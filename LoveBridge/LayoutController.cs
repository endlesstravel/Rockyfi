using Love;
using Rockyfi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveBridge
{
    public abstract class LayoutController : ElementController
    {
        public LayoutController()
        {
            shadowPlay = new ShadowPlay<Element>();

            var schema = DefineLayoutXml();
            if (schema == null)
                throw new System.Exception("DefineLayoutXml should return xml document string !");

            var initData = DefineInitialData();
            shadowPlay.Build(schema);
            shadowPlay.SetData(initData);
            bridge = new Bridge(shadowPlay, CreateElementCheck, SetRootParent);
            shadowPlay.SetBridge(bridge);

            Update();
        }

        public abstract string DefineLayoutXml();
        public abstract Dictionary<string, object> DefineInitialData();

        public virtual void SetRootParent(ElementController root)
        {
            root.OnSetParent(this);
        }

        /// <summary>
        ///  default create element method
        /// </summary>
        public abstract ElementController CreateElement(string tagName, Dictionary<string, object> attr);

        ShadowPlay<Element> shadowPlay;
        Bridge bridge;

        public void ReloadXml()
        {
            var schema = DefineLayoutXml();
            if (schema == null)
                throw new System.Exception("DefineLayoutXml should return xml document string !");
            var initData = DefineInitialData();
            shadowPlay.Build(schema);
            shadowPlay.IsDataDirty = true;
        }

        ElementController CreateElementCheck(string tagName, Dictionary<string, object> attr)
        {
            var ele = CreateElement(tagName, attr);
            if (ele == null)
                throw new System.Exception("CreateElement must return a new object!");
            return ele;
        }

        public void SetData(string key, object data)
        {
            if (shadowPlay != null)
            {
                shadowPlay.SetData(key, data);
            }
        }

        public object GetData(string key)
        {
            if (shadowPlay != null)
            {
                return shadowPlay.GetData(key);
            }
            return null;
        }

        public virtual void Update()
        {
            if (shadowPlay != null && bridge != null)
            {
                shadowPlay.Update();
                bridge.Update();
            }
            cachedList = ListNodes();
            cachedDeepMaskList = ListNodesByDeep();
            InternalUpdateInput();
            InternalUpdateAutoNavigation();
        }


        /// <summary>
        /// 排除自己
        /// </summary>
        protected List<DeepMaskBean> cachedDeepMaskList { get; private set; } = new List<DeepMaskBean>();


        /// <summary>
        /// 更新的列表，排除自己
        /// </summary>
        protected List<ElementController> cachedList { get; private set; } = new List<ElementController>();

        public string GetRealXmlString()
        {
            return shadowPlay.ToString();
        }

        public string GetAttrbuteXmlString()
        {
            return bridge.ToString();
        }

        /// <summary>
        /// <para>copy from    https://stackoverflow.com/a/21886340  </para>
        /// Comparer for comparing two keys, handling equality as beeing greater
        /// Use this Comparer e.g. with SortedLists or SortedDictionaries, that don't allow duplicate keys
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        public class DuplicateKeyComparer<TKey>
                        :
                     IComparer<TKey> where TKey : IComparable
        {
            #region IComparer<TKey> Members

            public int Compare(TKey x, TKey y)
            {
                int result = x.CompareTo(y);

                if (result == 0)
                    return 1;   // Handle equality as beeing greater
                else
                    return result;
            }

            #endregion
        }

        public class DeepMaskBean
        {
            public readonly int Deep;
            public readonly ElementController Ele;
            public readonly bool HasMask;
            public readonly RectangleF Mask;

            /// <summary>
            /// 是否在遮罩外
            /// </summary>
            public bool IsOutOfMask => HasMask && Ele.Rect.IntersectsWith(Mask) == false;

            public DeepMaskBean(int deep, ElementController ele, bool hasMask, RectangleF mask)
            {
                Deep = deep;
                Ele = ele;
                HasMask = hasMask;
                Mask = mask;
            }

            public override string ToString()
            {
                return base.ToString() + $", Deep:{Deep}, Ele:{Ele}";
            }
        }


        /// <summary>
        /// 获取所有节点
        /// </summary>
        /// <returns></returns>
        public List<ElementController> ListNodes()
        {
            // 根据深度排序其子节点
            Queue<ElementController> queue = new Queue<ElementController>();
            queue.Enqueue(this);

            List<ElementController> list = new List<ElementController>();


            while (queue.Count > 0)
            {
                var te = queue.Dequeue();
                if (te.Visible)
                {
                    if (te != this)
                    {
                        list.Add(te);
                    }

                    foreach (var child in te.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 根据深度排序其子节点
        /// </summary>
        /// <returns></returns>
        public List<DeepMaskBean> ListNodesByDeep()
        {
            // 根据深度排序其子节点
            Queue<DeepMaskBean> queue = new Queue<DeepMaskBean>();

            float maxSize = 999999;
            queue.Enqueue(new DeepMaskBean(0, this, MaskScissor, new RectangleF(
                -maxSize/2, -maxSize/2, maxSize, maxSize)));

            SortedList<int, DeepMaskBean> list = new SortedList<int, DeepMaskBean>(new DuplicateKeyComparer<int>());

            while (queue.Count > 0)
            {
                var te = queue.Dequeue();
                if (te.Ele.Visible)
                {
                    if (te.Ele != this)
                    {
                        list.Add(te.Deep, te);
                    }

                    foreach (var child in te.Ele.Children)
                    {
                        if (child.Visible)
                        {
                            bool hasMask = child.MaskScissor || te.HasMask;
                            var mask = child.MaskScissor ? RectangleF.Intersect(te.Mask, child.Rect) : te.Mask;
                            var tec = new DeepMaskBean(te.Deep + 1, child, hasMask, mask);
                            queue.Enqueue(tec);
                        }
                    }
                }
            }

            return list.Values.ToList();
        }

        public override void InternalUpdateInput()
        {
            var containMouse = CurrentMouseHover(Mouse.GetPosition());
            if (containMouse != null)
            {
                var ele = containMouse.Ele;

                // 更新 cover 操作
                ele.UpdateInputHoverVisible();


                { // 更新 cover 操作
                    var node = ele;
                    while (node != null)
                    {
                        node.InternalUpdateInputHover();
                        node = node.Parent;
                    }
                }


                { // 更新滚轮操作
                    var node = ele;
                    var sx = Mouse.GetScrollX();
                    var sy = Mouse.GetScrollY();
                    bool scrollUsed = false;
                    while (node != null)
                    {
                        node.InternalUpdateInputScroll(ref scrollUsed, sx, sy);
                        node = node.Parent;
                    }
                }
            }


            { // 每个元素每一帧都调用一次的方法
                foreach (var ele in cachedList)
                {
                    ele.InternalUpdateInput();
                }
            }
        }

        #region input



        /// <summary>
        /// 返回和指定点相交的元素，没有相交的节点时返回null
        /// </summary>
        /// <returns></returns>
        public DeepMaskBean CurrentMouseHover(Vector2 pos)
        {
            // 找到最顶部的和鼠标相遇的东西
            if (cachedDeepMaskList.Count > 0)
            {
                var topDeep = cachedDeepMaskList.Where(item => item.IsOutOfMask == false).Reverse().ToList();
                var containPosition = topDeep.FirstOrDefault(
                    item => item.HasMask ? RectangleF.Intersect(item.Mask, item.Ele.Rect).Contains(pos) : item.Ele.Rect.Contains(pos));

                return containPosition;
            }
            return null;
        }

        #endregion





        #region Navigation

        ElementController m_autoNavigationELEC = null;
        public ElementController AutoNavigationElement => m_autoNavigationELEC;

        /// <summary>
        /// 更新自动导航
        /// </summary>
        public virtual void InternalUpdateAutoNavigation()
        {
            var indicatorDir = Vector2.Zero;
            if (Keyboard.IsDown(KeyConstant.Left))
                indicatorDir += Vector2.Left;
            if (Keyboard.IsDown(KeyConstant.Right))
                indicatorDir += Vector2.Right;
            if (Keyboard.IsDown(KeyConstant.Up))
                indicatorDir += Vector2.Up;
            if (Keyboard.IsDown(KeyConstant.Down))
                indicatorDir += Vector2.Down;

            FindNearestPoint(indicatorDir);

            if (m_autoNavigationELEC != null)
            {
                m_autoNavigationELEC.UpdateInputAutoNavigation();
            }
        }

        int FloatCompare(float a, float b)
        {
            if (a > b)
                return 1;
            if (b > a)
                return -1;

            return 0;
        }

        public ElementController FindNearestPoint(IEnumerable<ElementController> deepMaskBeans, Vector2 p, Vector2 dir)
        {
            var li = deepMaskBeans
                .Select(item => Tuple.Create(item,
                            Vector2.Dot(item.Rect.Center() - p, dir) / ((item.Rect.Center() - p).Length() * dir.Length()), // 角度
                            Vector2.Distance(item.Rect.Center(), p) // 距离
                            ))
                .Where(item => item.Item2 > 0 && item.Item2 > 0.9f).ToList();

            li.Sort((a, b) =>
            {
                if (a.Item3 == b.Item3)
                {
                    return FloatCompare(a.Item2, b.Item2);
                }

                if (a.Item3 < b.Item3)
                    return -1;

                return 1;
            });

            foreach (var item in li)
            {
                Console.WriteLine($"{item}");
            }

            if (li.Count() > 0)
                return li.First().Item1;

            return null;
        }


        public void FindNearestPoint(Vector2 dir)
        {
            if (cachedList.Contains(m_autoNavigationELEC) == false)
            {
                m_autoNavigationELEC = null;
            }

            if (cachedList.Count > 0)
            {
                var initPos = m_autoNavigationELEC != null ? m_autoNavigationELEC.Rect.Center() : Vector2.Zero;
                m_autoNavigationELEC = FindNearestPoint(cachedList.Where(item => item.AutoNavigation).ToList(), initPos, dir);
            }
        }

        #endregion



    }
}

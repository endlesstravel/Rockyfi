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
        public LayoutController() : base(nameof(LayoutController) + ".root.object")
        {
            shadowPlay = new ShadowPlay<Element>();

            var schema = DefineLayoutDocument();
            if (schema == null)
                throw new System.Exception(nameof(DefineLayoutDocument) + " return an unexpected null!");

            var initData = DefineInitialData();
            shadowPlay.Build(schema);
            shadowPlay.SetData(initData);
            bridge = new Bridge(shadowPlay, CreateElementCheck, SetRootParent);
            shadowPlay.SetBridge(bridge);

            //Update(); // not call virtual method in ..
        }

        /// <summary>
        /// define the layout document
        /// </summary>
        /// <returns></returns>
        public abstract string DefineLayoutDocument();

        /// <summary>
        /// define the initial data
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, object> DefineInitialData();

        /// <summary>
        ///  default create element method
        /// </summary>
        public abstract ElementController CreateElement(string tagName, Dictionary<string, object> attr);

        public virtual void SetRootParent(ElementController root)
        {
            root.OnSetParent(this);
        }

        ShadowPlay<Element> shadowPlay;
        Bridge bridge;

        /// <summary>
        /// FIXME:
        /// reaload xml, recall the `DefineInitialData` method
        /// </summary>
        private void ResetLayout()
        {
            var schema = DefineLayoutDocument();
            var initData = DefineInitialData();
            if (schema == null)
                throw new System.Exception("DefineLayoutXml should return xml document string !");
            shadowPlay.Build(schema);
            shadowPlay.ResetBridgeRootElement();
            shadowPlay.SetData(initData);
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
            Stack<DeepMaskBean> stack = new Stack<DeepMaskBean>();
            float maxSize = 999999;
            stack.Push(new DeepMaskBean(0, this, MaskScissor, new RectangleF(
                -maxSize/2, -maxSize/2, maxSize, maxSize)));

            LinkedList<DeepMaskBean> lllist = new LinkedList<DeepMaskBean>();

            while (stack.Count > 0)
            {
                var te = stack.Pop();

                if (te.Ele.Visible)
                {
                    if (te.Ele != this) // 排除自己
                    {
                        //list.Add(te.Deep, te);
                        lllist.AddLast(te);
                    }

                    foreach (var child in te.Ele.Children.Reverse<ElementController>()) // Reverse for `Later drawings`
                    {
                        if (child.Visible)
                        {
                            bool hasMask = child.MaskScissor || te.HasMask;
                            var mask = child.MaskScissor ? RectangleF.Intersect(te.Mask, child.Rect) : te.Mask;
                            var tec = new DeepMaskBean(te.Deep + 1, child, hasMask, mask);

                            stack.Push(tec);
                        }
                    }
                }
            }

            return lllist.ToList();
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
            var hoverableList = cachedDeepMaskList.Where(item => item.Ele.Hoverable).ToList();
            if (hoverableList.Count > 0)
            {
                var topDeep = hoverableList.Where(item => item.IsOutOfMask == false).Reverse().ToList();
                var containPosition = topDeep.FirstOrDefault(
                    item => item.HasMask ? RectangleF.Intersect(item.Mask, item.Ele.Rect).Contains(pos) : item.Ele.Rect.Contains(pos));

                return containPosition;
            }
            return null;
        }

        #endregion





        #region Navigation

        ElementController m_autoNavigationELEC = null;
        ElementController m_lastAutoNavigationELEC = null;
        public ElementController AutoNavigationElement => m_autoNavigationELEC;

        public virtual void SetAutoNavigation(ElementController elc)
        {
            m_lastAutoNavigationELEC = m_autoNavigationELEC;
            m_autoNavigationELEC = elc;
            InternalUpdateAutoNavigationInoveEvent();
        }

        public virtual void ClearAutoNavigation() => SetAutoNavigation(null);

        public ElementController GetRootParent(ElementController element)
        {
            if (element == null || element.Parent == null)
                return null;

            var ele = element.Parent;
            while (ele.Parent != null)
            {
                ele = ele.Parent;
            }

            return ele;
        }

        /// <summary>
        /// UpdateInputAutoNavigationEnd / UpdateInputAutoNavigationBegin 事件
        /// </summary>
        public void InternalUpdateAutoNavigationInoveEvent()
        {
            if (m_lastAutoNavigationELEC != m_autoNavigationELEC)
            {
                if (m_lastAutoNavigationELEC != null)
                    m_lastAutoNavigationELEC.UpdateInputAutoNavigationEnd();
                if (m_autoNavigationELEC != null)
                    m_autoNavigationELEC.UpdateInputAutoNavigationBegin();
            }
        }

        public void CheckNodeVaild()
        {

            if (GetRootParent(m_autoNavigationELEC) != this)
                m_autoNavigationELEC = null;
            if (GetRootParent(m_lastAutoNavigationELEC) != this)
                m_lastAutoNavigationELEC = null;
        }

        /// <summary>
        /// 更新自动导航
        /// </summary>
        public virtual void InternalUpdateAutoNavigation()
        {
            CheckNodeVaild();

            m_lastAutoNavigationELEC = m_autoNavigationELEC;

            if (Keyboard.IsPressed(KeyConstant.Left) 
                || Keyboard.IsPressed(KeyConstant.Right)
                || Keyboard.IsPressed(KeyConstant.Up)
                || Keyboard.IsPressed(KeyConstant.Down)
                )
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
            }


            if (m_autoNavigationELEC != null) // stay ..
            {
                m_autoNavigationELEC.UpdateInputAutoNavigation();
            }

            InternalUpdateAutoNavigationInoveEvent();
            InternalAdjustOffsetToViewChild();
        }

        

        /// <summary>
        /// 更新父节点的滚动以适应其…………
        /// </summary>
        public virtual void InternalAdjustOffsetToViewChild()
        {
            if (m_autoNavigationELEC != null && m_lastAutoNavigationELEC != m_autoNavigationELEC) // 是否切换显示
            {
                var parent = m_autoNavigationELEC.Parent;

                if (parent != null && parent.HasScrollOffset) // 是否其父节点需要滚动
                {
                    var pr = parent.Rect;
                    var ar = m_autoNavigationELEC.Rect;

                    if (pr.Width > ar.Width && pr.Height > ar.Height) // 仅当大于时切换
                    {
                        // 检测是否超越边界
                        if ((pr.Left <= ar.Left && pr.Top <= ar.Top && pr.Right >= ar.Right && pr.Bottom >= ar.Bottom) == false)
                        {
                            var limitRect = pr.DefLeft(pr.Left + ar.Width / 2)
                                                .DefRight(pr.Right - ar.Width / 2)
                                                .DefTop(pr.Top + ar.Height / 2)
                                                .DefBottom(pr.Bottom - ar.Height / 2);


                            var r = ar;
                            r.Left = Mathf.Max(r.Left, pr.Left);
                            r.Top = Mathf.Max(r.Top, pr.Top);
                            r.Right = Mathf.Min(r.Right, pr.Right);
                            r.Bottom = Mathf.Min(r.Bottom, pr.Bottom);

                            var offset = ar.Location - r.Location;

                            // 设置父节点偏移
                            parent.SetScrollOffset(parent.ScrollOffset - offset);
                        }
                    }
                }
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
            for (float limit = 0.9f; limit > 0; limit -= 0.09f)
            {
                var ele = FindNearestPoint(deepMaskBeans, p, dir, limit);
                if (ele != null)
                    return ele;
            }

            return null;
        }

        public ElementController FindNearestPoint(IEnumerable<ElementController> deepMaskBeans, Vector2 p, Vector2 dir, float degreeLimit)
        {
            var li = deepMaskBeans
                .Select(item => Tuple.Create(item,
                            Vector2.Dot(item.Rect.Center() - p, dir) / ((item.Rect.Center() - p).Length() * dir.Length()), // 角度
                            Vector2.Distance(item.Rect.Center(), p) // 距离
                            ))
                .Where(item => item.Item2 > degreeLimit).ToList();

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

            //foreach (var item in li)
            //{
            //    Console.WriteLine($"FindNearestPoint -> {item}"); // debug for show
            //}

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
                var list = cachedList.Where(item => item.AutoNavigation).ToList();

                if (m_autoNavigationELEC == null)
                {
                    m_autoNavigationELEC = list.FirstOrDefault();
                }
                else // if (m_autoNavigationELEC != null)
                {
                    var t = FindNearestPoint(list, initPos, dir);
                    if (t != null)
                        m_autoNavigationELEC = t;   
                }

            }
        }

        #endregion

        #region draw area

        public override void Draw()
        {
            foreach (var dmb in cachedDeepMaskList)
            {
                //if (dmb.Ele != this) // cachedDeepMaskList 已经排除了自己
                {
                    if (dmb.HasMask)
                    {
                        var maskf = dmb.Mask;
                        Rectangle cr = new Rectangle(
                            Mathf.FloorToInt(maskf.X),
                            Mathf.FloorToInt(maskf.Y),
                            Mathf.CeilToInt(maskf.Width),
                            Mathf.CeilToInt(maskf.Height)
                            );
                        Graphics.SetScissor(cr);
                    }
                    dmb.Ele.Draw();
                    //Graphics.Print(dmb.Deep.ToString(), dmb.Ele.Rect.X, dmb.Ele.Rect.Y + Graphics.GetFont().GetHeight()); // debug for show

                    if (dmb.HasMask)
                    {
                        Graphics.SetScissor();
                    }
                }
            }


            //var containMouse = CurrentMouseHover(Mouse.GetPosition());
            //if (containMouse != null)
            //{
            //    Graphics.Print(containMouse.Ele.Text + containMouse.Ele.ToString(), 100, 100); // debug for show
            //}
        }

        #endregion


    }
}

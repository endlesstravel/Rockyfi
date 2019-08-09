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
        }

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
                    list.Add(te.Deep, te);

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
    }
}

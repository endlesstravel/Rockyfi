using System;
using System.Collections.Generic;
using System.Text;

namespace Rockyfi
{
    public partial class ShadowPlay
    {
        #region Draw Region

        public delegate void DrawNodeFunc(float x, float y, float width, float height, string text, Dictionary<string, object> attribute);

        /// <summary>
        /// Draw the three with the give draw function
        /// </summary>
        /// <param name="defaultDrawFunc">default draw function, draw the component when specify tage draw function not exists</param>
        public void DrawTraversely(DrawNodeFunc defaultDrawFunc = null)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                var contextAttr = GetNodeRuntimeAttribute(node);
                var drawFunc = themeDictionary.TryGetValue(contextAttr.template.TagName, out var specifyDrawFunc) ? specifyDrawFunc : defaultDrawFunc;

                if (drawFunc != null)
                {
                    var nc = node.Context; // protected node.Context

                    var attributes = new Dictionary<string, object>(contextAttr.attributes.Count
                        + contextAttr.template.attributes.Count);

                    foreach (var kv in contextAttr.template.attributes)
                        attributes[kv.Key] = kv.Value;
                    foreach (var kv in contextAttr.attributes)
                        attributes[kv.Key.TargetName] = kv.Value;

                    defaultDrawFunc(node.LayoutGetLeft(), node.LayoutGetTop(),
                        node.LayoutGetWidth(), node.LayoutGetHeight(),
                        contextAttr.textDataBindExpressCurrentValue,
                        attributes // contextAttr.attributes ???
                        );
                    if (!(nc == null && node.Context == null) && nc != node.Context)
                    {
                        throw new Exception("cant change node.Context !");
                    }
                    node.Context = nc; // protected node.Context
                }

                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        readonly Dictionary<string, DrawNodeFunc> themeDictionary = new Dictionary<string, DrawNodeFunc>();
        public void SetTheme(Dictionary<string, DrawNodeFunc> theme)
        {
            foreach (var kv in theme)
            {
                themeDictionary[kv.Key] = kv.Value;
            }
        }

        public abstract class Element<T> where T: Element<T>
        {
            public abstract void ChangeAttributes(Dictionary<string, object> attributes);
            public abstract void InsertChild(int index, T child);
            public abstract void RemoveAt(int index);
            public abstract void Remove(T child);
            public abstract void Destory(T element);
        }
        public abstract class ElementFactory<T> where T : Element<T>
        {
            public abstract T CreateElement(string tagName, Dictionary<string, object> attr);
        }

        /// <summary>
        /// Set element factory, null to unset.
        /// </summary>
        /// <param name="factory"></param>
        public void SetElementFactory(ElementFactory factory)
        {
            elementFactory = factory;
        }

        ElementFactory elementFactory;

        #endregion
    }
}

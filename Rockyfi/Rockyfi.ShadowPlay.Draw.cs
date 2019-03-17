using System;
using System.Collections.Generic;
using System.Text;

namespace Rockyfi
{
    public partial class ShadowPlay
    {
        #region Draw Region

        public delegate void DrawNodeFunc(float x, float y, float width, float height, string text, Dictionary<string, object> attribute);


        void DrawRecursive(float x, float y, Node node, DrawNodeFunc defaultDrawFunc, Dictionary<string, DrawNodeFunc> themeDictionary)
        {
            var contextAttr = GetNodeRuntimeAttribute(node);
            var drawFunc = themeDictionary != null
                && themeDictionary.TryGetValue(contextAttr.template.TagName, out var specifyDrawFunc)
                ? specifyDrawFunc : defaultDrawFunc;

            if (drawFunc != null)
            {
                var nc = node.Context; // protected node.Context

                var attributes = new Dictionary<string, object>(contextAttr.attributes.Count
                    + contextAttr.template.attributes.Count);

                foreach (var kv in contextAttr.template.attributes)
                    attributes[kv.Key] = kv.Value;
                foreach (var kv in contextAttr.attributes)
                    attributes[kv.Key.TargetName] = kv.Value;

                defaultDrawFunc(x + node.LayoutGetLeft(), y + node.LayoutGetTop(),
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
                DrawRecursive(x + node.LayoutGetLeft(), y + node.LayoutGetTop(), child, defaultDrawFunc, themeDictionary);
            }
        }

        /// <summary>
        /// Draw the three with the give draw function
        /// </summary>
        /// <param name="defaultDrawFunc">default draw function, draw the component when specify tage draw function not exists</param>
        public void DrawTraversely(float x, float y, DrawNodeFunc defaultDrawFunc, Dictionary<string, DrawNodeFunc> themeDictionary = null)
        {
            if (defaultDrawFunc == null && themeDictionary == null)
                throw new ArgumentNullException("defaultDrawFunc");

            DrawRecursive(x, y, root, defaultDrawFunc, themeDictionary);
        }

        public abstract class Bridge
        {
            public abstract BridgeElement CreateElement(string tagName, Dictionary<string, object> attr);
            public abstract void OnSetRoot(BridgeElement root);

            internal BridgeElement CreateElement(Node node, string tagName, Dictionary<string, object> attr)
            {
                var ele = CreateElement(tagName, attr);
                ele.node = node;
                return ele;
            }

        }

        /// <summary>
        /// Set element factory, null to unset.
        /// </summary>
        /// <param name="factory"></param>
        public void SetBridge(Bridge factory)
        {
            if (bridge != null)
            {
                throw new Exception("can't change element factory");
            }
            bridge = factory;

            // reset root
            root = Flex.CreateDefaultNode();
            var ra = CreateRuntimeNodeAttribute(root, templateRoot);

            // copy style
            ProcessStyleBind(root, GenerateContextStack());

            var eleRoot = bridge.CreateElement(root,
                templateRoot.TagName,
                new Dictionary<string, object>());
            ra.element = eleRoot;
            bridge.OnSetRoot(eleRoot);
        }

        Bridge bridge;

        public abstract class BridgeElement
        {
            public abstract void OnChangeAttributes(string key, object value);
            public abstract void OnChangeText(string text);
            public abstract void OnInsertChild(int index, BridgeElement child);
            public abstract void OnReplaceChild(BridgeElement oldChild, BridgeElement child);
            public abstract void OnAddChild(BridgeElement child);
            public abstract void OnRemoveAt(int index);
            public abstract void OnRemove(BridgeElement child);

            #region other properties

            /// <summary>
            /// Return binded text, otherwies return nul.
            /// </summary>
            /// <returns></returns>
            public string GetText()
            {
                return GetNodeRuntimeAttribute(node).textDataBindExpressCurrentValue;
            }
            #endregion

            #region Style
            internal Node node;
            public Overflow StyleGetOverflow()
            {
                return node.StyleGetOverflow();
            }

            public Display StyleGetDisplay()
            {
                return node.StyleGetDisplay();
            }

            // LayoutGetLeft gets left
            public float LayoutGetLeft()
            {
                return node.LayoutGetLeft();
            }

            // LayoutGetTop gets top
            public float LayoutGetTop()
            {
                return node.LayoutGetTop();
            }

            // LayoutGetRight gets right
            public float LayoutGetRight()
            {
                return node.LayoutGetRight();
            }

            // LayoutGetBottom gets bottom
            public float LayoutGetBottom()
            {
                return node.LayoutGetBottom();
            }

            // LayoutGetWidth gets width
            public float LayoutGetWidth()
            {
                return node.LayoutGetWidth();
            }

            // LayoutGetHeight gets height
            public float LayoutGetHeight()
            {
                return node.LayoutGetHeight();
            }

            // LayoutGetMargin gets margin
            public float LayoutGetMargin(Edge edge)
            {
                return node.LayoutGetMargin(edge);
            }

            // LayoutGetBorder gets border
            public float LayoutGetBorder(Edge edge)
            {
                return node.LayoutGetBorder(edge);
            }

            // LayoutGetPadding gets padding
            public float LayoutGetPadding(Edge edge)
            {
                return node.LayoutGetPadding(edge);
            }

            public bool LayoutGetHadOverflow()
            {
                return node.LayoutGetHadOverflow();
            }

            #endregion

        }

        #endregion
    }
}

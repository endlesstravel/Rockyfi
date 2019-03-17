using System;
using System.Collections.Generic;

namespace Rockyfi
{
    public class EmptyBridgeElement : BridgeElement<EmptyBridgeElement>
    {
        public override void OnAddChild(EmptyBridgeElement child)
        {
            throw new NotImplementedException();
        }

        public override void OnChangeAttributes(string key, object value)
        {
            throw new NotImplementedException();
        }

        public override void OnChangeText(string text)
        {
            throw new NotImplementedException();
        }

        public override void OnInsertChild(int index, EmptyBridgeElement child)
        {
            throw new NotImplementedException();
        }

        public override void OnRemove(EmptyBridgeElement child)
        {
            throw new NotImplementedException();
        }

        public override void OnRemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public override void OnReplaceChild(EmptyBridgeElement oldChild, EmptyBridgeElement child)
        {
            throw new NotImplementedException();
        }
    }

    public class ShasowPlaySimpleBridge : Bridge<EmptyBridgeElement>
    {
        public override EmptyBridgeElement CreateElement(string tagName, Dictionary<string, object> attr)
        {
            throw new NotImplementedException();
        }

        public override void OnSetRoot(EmptyBridgeElement root)
        {
            throw new NotImplementedException();
        }
    }

    public class ShadowPlaySimple : ShadowPlay<EmptyBridgeElement>
    {
    }

    public partial class ShadowPlay<T> where T: BridgeElement<T>
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
        public void Draw(float x, float y, DrawNodeFunc defaultDrawFunc, Dictionary<string, DrawNodeFunc> themeDictionary = null)
        {
            if (defaultDrawFunc == null && themeDictionary == null)
                throw new ArgumentNullException("defaultDrawFunc");

            DrawRecursive(x, y, root, defaultDrawFunc, themeDictionary);
        }

        /// <summary>
        /// Set element factory, null to unset.
        /// </summary>
        /// <param name="factory"></param>
        public void SetBridge(Bridge<T> factory)
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

        Bridge<T> bridge;

        #endregion
    }


    public abstract class Bridge<T> where T : BridgeElement<T>
    {
        public abstract T CreateElement(string tagName, Dictionary<string, object> attr);
        public abstract void OnSetRoot(T root);

        internal T CreateElement(Node node, string tagName, Dictionary<string, object> attr)
        {
            var ele = CreateElement(tagName, attr);
            ele.node = node;
            return ele;
        }

    }

    public abstract class BridgeElement<T> where T : BridgeElement<T>
    {
        public abstract void OnChangeAttributes(string key, object value);
        public abstract void OnChangeText(string text);
        public abstract void OnInsertChild(int index, T child);
        public abstract void OnReplaceChild(T oldChild, T child);
        public abstract void OnAddChild(T child);
        public abstract void OnRemoveAt(int index);
        public abstract void OnRemove(T child);

        #region other properties
        internal Node node;
        /// <summary>
        /// Return binded text, otherwies return nul.
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return ShadowPlay<T>.GetNodeRuntimeAttribute(node).textDataBindExpressCurrentValue;
        }
        #endregion

        #region Style
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
}

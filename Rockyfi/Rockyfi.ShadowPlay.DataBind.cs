using System;
using System.Collections;
using System.Collections.Generic;

namespace Rockyfi
{

    public partial class ShadowPlay
    {
        #region DataBind
        internal class TemplateNode
        {
            public TemplateNode(string tagName)
            {
                TagName = tagName;
            }

            public readonly string TagName;
            internal IfDataBindExpress ifExpress = null;
            internal ForDataBindExpress forExpress = null;
            internal TextDataBindExpress textDataBindExpress = null;
            internal readonly LinkedList<AttributeDataBindExpress> attributeDataBindExpressList = new LinkedList<AttributeDataBindExpress>();
            internal readonly Style nodeStyle = new Style();
            internal readonly Dictionary<string, string> attributes = new Dictionary<string, string>();
            internal readonly List<TemplateNode> Children = new List<TemplateNode>();
        }

        internal class TemplateChildGroup : IEnumerable<Node>
        {
            public readonly TemplateNode Template;
            public Node monoNode;
            public LinkedList<Node> NodeList = new LinkedList<Node>();

            public TemplateChildGroup(TemplateNode tnode)
            {
                Template = tnode;
            }

            public void Append(Node node)
            {
                if (NodeList != null)
                {
                    NodeList.AddLast(node);
                }
                else if (monoNode != null)
                {
                    NodeList = new LinkedList<Node>();
                    NodeList.AddLast(monoNode);
                    NodeList.AddLast(node);
                    monoNode = null;
                }
                else
                {
                    monoNode = node;
                }
            }

            public bool IsMono { get { return monoNode != null; } }
            public bool IsMultiply { get { return NodeList != null; } }



            public IEnumerator<Node> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }

        internal class RuntimeNodeAttribute
        {
            internal Node node;
            internal readonly TemplateNode template; // where node come from

            public RuntimeNodeAttribute(Node node, TemplateNode templateRendererNode)
            {
                this.node = node;
                this.template = templateRendererNode;
            }

            public bool IsDirty = false;
            internal object forExpressItemCurrentValue = null;
            internal string textDataBindExpressCurrentValue = null;
            internal readonly Dictionary<AttributeDataBindExpress, object> attributes = new Dictionary<AttributeDataBindExpress, object>();
            internal readonly LinkedList<TemplateChildGroup> Children = new LinkedList<TemplateChildGroup>();
        }

        RuntimeNodeAttribute CreateRuntimeNodeAttribute(Node node, TemplateNode templateRendererNode)
        {
            var ra = new RuntimeNodeAttribute(node, templateRendererNode);
            node.Context = ra;
            return ra;
        }

        internal static RuntimeNodeAttribute GetNodeRuntimeAttribute(Node node)
        {
            return node.Context as RuntimeNodeAttribute;
        }

        readonly Dictionary<string, object> runtimeContext = new Dictionary<string, object>();
        readonly LiteSet<string> targetProperties = new LiteSet<string>();
        public void SetProperties(IEnumerable<string> properties)
        {
            foreach (var props in properties)
            {
                this.targetProperties.Add(props);
            }
        }

        public bool ContainsProperties(string key)
        {
            return targetProperties.Contains(key);
        }

        ContextStack GenerateContextStack()
        {
            return new ContextStack(runtimeContext);
        }

        /// <summary>
        /// reset all data.
        /// </summary>
        /// <param name="contextDictionary"></param>
        public void ResetData(Dictionary<string, object> contextDictionary)
        {
            runtimeContext.Clear();
            if (contextDictionary != null)
            {
                foreach (var kv in contextDictionary)
                {
                    runtimeContext[kv.Key] = kv.Value;
                }
            }
        }

        /// <summary>
        /// update data in value, if the value different then before then re-renderer the tree
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void SetData(string key, object data)
        {
            if (key == null)
                return;

            if (runtimeContext.TryGetValue(key, out var oldData) && data != null && oldData != null)
            {
                if (data != null && data.Equals(oldData)
                    || oldData != null && oldData.Equals(data))
                    return;

            }

            runtimeContext[key] = data;
        }

        /// <summary>
        /// return null is no key on ...
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
        {
            return runtimeContext.TryGetValue(key, out var obj) ? obj : null;
        }

        #endregion
    }
}

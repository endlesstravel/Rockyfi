using System;
using System.Collections;
using System.Collections.Generic;

namespace Rockyfi
{
    public partial class ShadowPlay<T> where T: BridgeElement<T>
    {
        #region DataBind
        internal class TemplateNode
        {
            public TemplateNode(string tagName)
            {
                TagName = tagName;
            }

            public readonly string TagName;
            public readonly string Key;
            internal IfDataBindExpress ifExpress = null;
            internal ForDataBindExpress forExpress = null;
            internal TextDataBindExpress textDataBindExpress = null;
            internal readonly LinkedList<AttributeDataBindExpress> attributeDataBindExpressList = new LinkedList<AttributeDataBindExpress>();
            //internal readonly LinkedList<OnceDataBindExpress> onceExpressList = new LinkedList<OnceDataBindExpress>();
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

        internal class RuntimeAttribute
        {
            internal Node node;
            internal readonly TemplateNode template; // where node come from
            internal T element;

            public RuntimeAttribute(Node node, TemplateNode template)
            {
                this.node = node;
                this.template = template;
            }

            internal bool IsThunk = false;

            public bool IsDirty = false;
            internal object forExpressItemCurrentValue = null;
            internal string textDataBindExpressCurrentValue = null;
            //internal DictionarySet<OnceDataBindExpress> onceExecuteFunc = new DictionarySet<OnceDataBindExpress>(); // alread execute
            internal readonly Dictionary<AttributeDataBindExpress, object> attributes = new Dictionary<AttributeDataBindExpress, object>();
            internal Dictionary<string, object> StringAttr
            {
                get
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    foreach (var kv in attributes)
                    {
                        dic[kv.Key.TargetName] = kv.Value;
                    }
                    return dic;
                }
            }
            internal LinkedList<TemplateChildGroup> groupChildren = new LinkedList<TemplateChildGroup>();
            internal RuntimeAttribute Parent;
            internal List<RuntimeAttribute> Children = new List<RuntimeAttribute>();

            public delegate void AppendChildMethod(TemplateNode template, Node node);
            public void ResetChildren(Action<AppendChildMethod> builder)
            {
                TemplateNode currentTemplate = null;
                TemplateChildGroup currentGroup = null;
                builder((TemplateNode t, Node n) =>
                {
                    if (currentTemplate != t)
                    {
                        currentTemplate = t;
                        currentGroup = new TemplateChildGroup(t);
                        groupChildren.AddLast(currentGroup);
                    }
                    currentGroup.Append(n);

                    var ra = GetNodeRuntimeAttribute(n);
                    ra.Parent = this;
                    Children.Add(ra);
                });
            }
            public void AppendChild(RuntimeAttribute ra)
            {
                var currentGroup = groupChildren.Count > 0 ? groupChildren.Last.Value : null;
                var currentTemplate = groupChildren.Count > 0 ? groupChildren.Last.Value.Template : null;

                if (currentTemplate != ra.template)
                {
                    currentGroup = new TemplateChildGroup(ra.template);
                }

                currentGroup.Append(ra.node);

                ra.Parent = this;
                Children.Add(ra);
            }
        }

        RuntimeAttribute CreateRuntimeNodeAttribute(Node node, TemplateNode tnode)
        {
            var ra = new RuntimeAttribute(node, tnode);
            node.Context = ra;
            return ra;
        }

        internal static RuntimeAttribute GetNodeRuntimeAttribute(Node node)
        {
            return node.Context as RuntimeAttribute;
        }

        readonly Dictionary<string, object> runtimeContext = new Dictionary<string, object>();
        //readonly LiteSet<string> targetProperties = new LiteSet<string>();
        //public void SetProperties(IEnumerable<string> properties)
        //{
        //    foreach (var props in properties)
        //    {
        //        this.targetProperties.Add(props);
        //    }
        //}

        //public bool ContainsProperties(string key)
        //{
        //    return targetProperties.Contains(key);
        //}

        public ContextStack GenerateContextStack()
        {
            return new ContextStack(runtimeContext);
        }

        //public RuntimeContext GenerateRuntimeContext()
        //{
        //    return new RuntimeContext(runtimeContext);
        //}

        //public class RuntimeContext : Expr.IVariableHolder
        //{
        //    readonly Dictionary<string, object> dict = new Dictionary<string, object>();

        //    public RuntimeContext(Dictionary<string, object> dict)
        //    {
        //        this.dict = dict;
        //    }
        //    public bool Exists(string name)
        //    {
        //        return dict.ContainsKey(name);
        //    }

        //    public object GetVariable(string name)
        //    {
        //        return dict.TryGetValue(name, out var value) ? value : null;
        //    }
        //}

        public bool IsDataDirty { get; set; }

        /// <summary>
        /// reset all data.
        /// </summary>
        /// <param name="contextDictionary"></param>
        public void SetData(Dictionary<string, object> contextDictionary)
        {
            IsDataDirty = true;
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
            if (runtimeContext.TryGetValue(key, out var oldData) && data != null && oldData != null)
            {
                if (data != null && data.Equals(oldData) || oldData != null && oldData.Equals(data))
                {
                    return;
                }

            }

            IsDataDirty = true;
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

using System;
using System.Collections.Generic;
using System.Xml;

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

            internal TemplateNode Parent;
            internal IfDataBindExpress ifExpress = null;
            internal ForDataBindExpress forExpress = null;
            internal TextDataBindExpress textDataBindExpress = null;
            internal readonly LinkedList<AttributeDataBindExpress> attributeDataBindExpressList = new LinkedList<AttributeDataBindExpress>();
            internal readonly Style nodeStyle = new Style();
            internal readonly Dictionary<string, string> attributes = new Dictionary<string, string>();
            internal readonly List<TemplateNode> Children = new List<TemplateNode>();
        }

        class RuntimeNodeAttribute
        {
            internal Node node;
            internal readonly TemplateNode template; // where node come from
            internal CardData dataSource;
            internal CardData dataSourceParent;
            internal bool isListMember = false;
            internal int indexAtDataSourceParent;
            internal string nameAtDataSourceParent;

            public RuntimeNodeAttribute(Node node, TemplateNode templateRendererNode)
            {
                this.node = node;
                this.template = templateRendererNode;
            }

            public bool IsDirty = false;
            internal object forExpressItemCurrentValue = null;
            internal string textDataBindExpressCurrentValue = null;
            internal readonly Dictionary<AttributeDataBindExpress, object> attributes = new Dictionary<AttributeDataBindExpress, object>();

            internal class ChildGroup
            {
                public readonly TemplateNode Template;
                public readonly LinkedList<Node> NodeList = new LinkedList<Node>();

                public ChildGroup(TemplateNode tnode)
                {
                    Template = tnode;
                }
            }

            internal readonly LinkedList<ChildGroup> Children = new LinkedList<ChildGroup>();
        }

        RuntimeNodeAttribute CreateRuntimeNodeAttribute(Node node, TemplateNode templateRendererNode)
        {
            var ra = new RuntimeNodeAttribute(node, templateRendererNode);
            node.Context = ra;
            return ra;
        }

        RuntimeNodeAttribute GetNodeRuntimeAttribute(Node node)
        {
            return node.Context as RuntimeNodeAttribute;
        }

        interface IDataBindWatcher
        {
            void DataChanged(object newValue, object oldValue);
        }

        //class ExpressBind
        //{
        //    ExpressBind parent;
        //}

        class DataBind
        {
            //readonly Dictionary<string, ForDataBindExpress> forExpressBind = new Dictionary<string, ForDataBindExpress>();
            //readonly Dictionary<string, IfDataBindExpress> ifExpressBind = new Dictionary<string, IfDataBindExpress>();
            //readonly Dictionary<string, AttributeDataBindExpress> attrExpressBind = new Dictionary<string, AttributeDataBindExpress>();

            //readonly Dictionary<string, ExpressBind> expressBind = new Dictionary<string, ExpressBind>();

            //public void SetExpress(ForDataBindExpress x)
            //{

            //}

            readonly Dictionary<string, object> runtimeContext = new Dictionary<string, object>();
            readonly LiteSet<string> TopTargetKeys = new LiteSet<string>();

            public void ResetTopTargets()
            {
                TopTargetKeys.Clear();
            }

            public string GetTopTarget(string targetItem, TemplateNode templateNode)
            {
                string currentTarget = targetItem;
                var tnode = templateNode;
                while (tnode != null && !TopTargetKeys.Contains(currentTarget))
                {
                    if (tnode.forExpress != null)
                    {
                        currentTarget = tnode.forExpress.TargetKey;
                    }
                    tnode = tnode.Parent;
                }

                return currentTarget;
            }

            public bool TryAddTopTargetKey(string targetItem, TemplateNode templateNode)
            {
                var keyStr = GetTopTarget(targetItem, templateNode);
                if (keyStr != null)
                {
                    TopTargetKeys.Add(keyStr);
                    return true;
                }
                return false;
            }

            public ContextStack GenerateContextStack()
            {
                return new ContextStack(runtimeContext);
            }

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
        }


        /// <summary>
        /// update data in value, if the value different then before then re-renderer the tree
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void SetData(string key, object data)
        {
            dataBind.SetData(key, data);
        }

        /// <summary>
        /// return null is no key on ...
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
        {
            return dataBind.GetData(key);
        }


        //readonly Dictionary<string, LinkedList<RuntimeNodeAttribute>> textEffectBind = new Dictionary<string, LinkedList<RuntimeNodeAttribute>>();
        //readonly Dictionary<string, LinkedList<RuntimeNodeAttribute>> attributeEffectBind = new Dictionary<string, LinkedList<RuntimeNodeAttribute>>();
        //readonly Dictionary<string, LinkedList<RuntimeNodeAttribute>> ifExpressEffectBind = new Dictionary<string, LinkedList<RuntimeNodeAttribute>>();
        //readonly Dictionary<string, LinkedList<RuntimeNodeAttribute>> forExpressEffectBind = new Dictionary<string, LinkedList<RuntimeNodeAttribute>>();

        ///// <summary>
        ///// insert child
        ///// </summary>
        //void InsertChild(Node node, Node child, int idx)
        //{
        //    // TODO: data check
        //    Rockyfi.InsertChild(node, child, idx);
        //}

        //// delete
        //void RemoveChild(Node node, Node child)
        //{
        //    // TODO: data check
        //    Rockyfi.RemoveChild(node, child);
        //}

        ///// <summary>
        ///// update and re-render page node
        ///// </summary>
        //public void CommitDataChange()
        //{
        //    // 寻找那些最大的子树然后重新渲染
        //    // 广度优先遍历树
        //    List<Node> nodesNeedToReRender = new List<Node>();
        //    //Dictionary<Node, bool> nodeDirty = new Dictionary<Node, bool>(); // HashSet ???? not avaliable in .Net 2.0
        //    Queue<Node> queue = new Queue<Node>();
        //    queue.Enqueue(root);


        //    while (queue.Count > 0)
        //    {
        //        var node = queue.Dequeue();
        //        //if (nodeDirty.ContainsKey())
        //        //{

        //        //}
        //    }

        //    //foreach (var bind in effectBind.Values)
        //    //{
        //    //    if (bind.IsDirty)
        //    //    {
        //    //    }
        //    //}
        //}

        void BindAttributeExpress(AttributeDataBindExpress attributeDataBindExpress, Node node, ContextStackData contextStackData)
        {
            var ra = GetNodeRuntimeAttribute(node);
            ra.dataSource.AddDataChangedWatcher((string name, object newValue, object oldValue) =>
            {
                // change it style
                ProcessNodeStyle(node, name, newValue != null ? newValue.ToString() : "");
                ra.attributes[attributeDataBindExpress] = newValue;
            });
        }

        void BindTextExpressWithNode(TemplateNode tnode, TextDataBindExpress express, Node node, Node parentNode)
        {

        }

        #endregion
    }
}

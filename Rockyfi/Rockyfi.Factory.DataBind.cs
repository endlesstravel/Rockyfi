using System;
using System.Collections.Generic;
using System.Xml;

namespace Rockyfi
{
    public partial class LightCard
    {
        #region DataBind
        internal class TemplateNode
        {
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
            internal readonly TemplateNode templateRendererNode; // where node come from

            public RuntimeNodeAttribute(Node node, TemplateNode templateRendererNode)
            {
                this.node = node;
                this.templateRendererNode = templateRendererNode;
            }

            public bool IsDirty = false;
            internal object forExpressItemCurrentValue = null;
            internal string textDataBindExpressCurrentValue = null;
            internal readonly Dictionary<string, object> attributes = new Dictionary<string, object>();
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

        interface DataWatcher
        {
            void DataChanged(object newValue, object oldValue);
        }

        class ExpressBind
        {
            ExpressBind parent;

        }

        class DataBind
        {
            readonly Dictionary<string, ForDataBindExpress> forExpressBind = new Dictionary<string, ForDataBindExpress>();
            readonly Dictionary<string, IfDataBindExpress> ifExpressBind = new Dictionary<string, IfDataBindExpress>();
            readonly Dictionary<string, AttributeDataBindExpress> attrExpressBind = new Dictionary<string, AttributeDataBindExpress>();

            readonly Dictionary<string, ExpressBind> expressBind = new Dictionary<string, ExpressBind>();

            public void SetExpress(ForDataBindExpress x)
            {

            }

        }


        readonly Dictionary<string, ForDataBindExpress> topDomain = new Dictionary<string, ForDataBindExpress>();


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


        //void BindXMLNodeWithNode(XmlNode element, Node node, Node parentNode)
        //{
        //    AddRangeElementList();
        //}

        void BindIfExpressWithNode(TemplateNode tnode, IfDataBindExpress express, Node node, Node parentNode)
        {
            //if (express == null || node == null)
            //    return;

            //if (!express.IsEffectedByContext && express.TargetKeys != null)
            //{
            //    // TODO:
            //    //if (effectBind.TryGetValue(key, out DataBindContext bind))
            //    //{
            //    //    bind.Data = "";
            //    //}
            //}
        }

        void BindForExpressWithNode(TemplateNode tnode, ForDataBindExpress express, Node node, Node parentNode)
        {
            //if (express == null)
            //    return;

            //// TODO:
            //if (!forExpressEffectBind.TryGetValue(express.TargetKey, out var list))
            //{
            //    list = new LinkedList<RuntimeNodeAttribute>();
            //    forExpressEffectBind.Add(express.TargetKey, list);
            //}

            //var ca = GetNodeRuntimeAttribute(parentNode);
            //ca.forExpress = express;
            //list.AddLast(GetNodeRuntimeAttribute(node));
        }

        void BindAttributeExpressWithNode(TemplateNode tnode, AttributeDataBindExpress express, Node node, Node parentNode)
        {
            //if (express == null)
            //    return;

            //// TODO:
            //if (!attributeEffectBind.TryGetValue(express.TargetKey, out var list))
            //{
            //    list = new LinkedList<RuntimeNodeAttribute>();
            //    forExpressEffectBind.Add(express.TargetKey, list);
            //}


            //var ca = GetNodeRuntimeAttribute(node);
            //ca.attributeDataBindExpressList.AddLast(express);
            //list.AddLast(ca);
        }

        void BindTextExpressWithNode(TemplateNode tnode, TextDataBindExpress express, Node node, Node parentNode)
        {
            //if (express == null)
            //    return;

            //// TODO:

            //var ca = GetNodeRuntimeAttribute(node);
            //foreach (var tkey in express.TargetKeys)
            //{

            //    if (!attributeEffectBind.TryGetValue(tkey, out var list))
            //    {
            //        list = new LinkedList<RuntimeNodeAttribute>();
            //        forExpressEffectBind.Add(tkey, list);
            //    }

            //    list.AddLast(ca);
            //}
            //ca.textDataBindExpress = express;
        }

        ///// <summary>
        ///// update data in value
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="data"></param>
        //public void SetData(string key, object data)
        //{
        //    if (key == null)
        //        return;

        //    //LinkedList<RuntimeNodeAttribute> nodeAttributeList;
        //    //if (attributeEffectBind.TryGetValue(key, out nodeAttributeList))
        //    //{
        //    //    foreach (RuntimeNodeAttribute nodeAttribute in nodeAttributeList)
        //    //    {
        //    //        foreach (var attrExp in nodeAttribute.attributeDataBindExpressList)
        //    //        {
        //    //            if (key.Equals(attrExp.TargetKey))
        //    //            {
        //    //                object result = attrExp.TryEvaluate(contextStack, out object value) ? value : null;
        //    //                nodeAttribute.attributes[key] = result;
        //    //                ProcessNodeStyle(nodeAttribute.node, attrExp.TargetName, value != null ? value.ToString() : "");
        //    //                return;
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //else if (textEffectBind.TryGetValue(key, out nodeAttributeList))
        //    //{
        //    //    foreach (RuntimeNodeAttribute nodeAttribute in nodeAttributeList)
        //    //    {
        //    //        nodeAttribute.textDataBindExpressCurrentValue = nodeAttribute.textDataBindExpress.Evaluate(contextStack);
        //    //    }
        //    //}
        //    //else if (forExpressEffectBind.TryGetValue(key, out nodeAttributeList))
        //    //{
        //    //    foreach (RuntimeNodeAttribute childNodeAttribute in nodeAttributeList)
        //    //    {
        //    //        // remove old child
        //    //        var parentNode = childNodeAttribute.node.Parent;
        //    //        var parentNodeAttribute = GetNodeRuntimeAttribute(parentNode);

        //    //        foreach (var range in parentNodeAttribute.childernNodeElemetRangeList)
        //    //        {
        //    //            var testEle = range.element;
        //    //            foreach (var eledChildNode in range.nodes)
        //    //            {
        //    //                if (testEle == childNodeAttribute.element)
        //    //                {
        //    //                    parentNode.RemoveChild(eledChildNode);
        //    //                }
        //    //            }
        //    //        }

        //    //        // evaluate the value
        //    //        parentNodeAttribute.textDataBindExpressCurrentValue = parentNodeAttribute.textDataBindExpress.Evaluate(contextStack);


        //    //        // add to the ...

        //    //    }
        //    //}
        //}

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

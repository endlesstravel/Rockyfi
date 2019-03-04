using System;
using System.Collections.Generic;
using System.Xml;

namespace Rockyfi
{
    public partial class Factory
    {
        #region DataBind
        internal class TemplateRendererNode
        {
            internal Node node;
            internal IfDataBindExpress ifExpress = null;
            internal ForDataBindExpress forExpress = null;
            internal TextDataBindExpress textDataBindExpress = null;
            internal readonly LinkedList<AttributeDataBindExpress> attributeDataBindExpressList = new LinkedList<AttributeDataBindExpress>();
            internal readonly Style nodeStyle = new Style();
            internal readonly Dictionary<string, string> attributes = new Dictionary<string, string>();
            internal readonly List<TemplateRendererNode> Children = new List<TemplateRendererNode>();
        }

        class RuntimeNodeAttribute
        {
            internal readonly TemplateRendererNode templateRendererNode; // where node come from

            public RuntimeNodeAttribute(Node node, TemplateRendererNode templateRendererNode)
            {
                templateRendererNode.node = node;
                this.templateRendererNode = templateRendererNode;
            }

            internal IEnumerable<object> forExpressCurrentValue = null;
            internal string textDataBindExpressCurrentValue = null;
            internal readonly Dictionary<string, object> attributes = new Dictionary<string, object>();
        }

        RuntimeNodeAttribute CreateRuntimeNodeAttribute(Node node, TemplateRendererNode templateRendererNode)
        {
            var ra = new RuntimeNodeAttribute(node, templateRendererNode);
            node.Context = ra;
            return ra;
        }

        RuntimeNodeAttribute GetNodeRuntimeAttribute(Node node)
        {
            return node.Context as RuntimeNodeAttribute;
        }

        class DataBind
        {

        }
        readonly Dictionary<string, LinkedList<RuntimeNodeAttribute>> textEffectBind = new Dictionary<string, LinkedList<RuntimeNodeAttribute>>();
        readonly Dictionary<string, LinkedList<RuntimeNodeAttribute>> attributeEffectBind = new Dictionary<string, LinkedList<RuntimeNodeAttribute>>();
        readonly Dictionary<string, LinkedList<RuntimeNodeAttribute>> ifExpressEffectBind = new Dictionary<string, LinkedList<RuntimeNodeAttribute>>();
        readonly Dictionary<string, LinkedList<RuntimeNodeAttribute>> forExpressEffectBind = new Dictionary<string, LinkedList<RuntimeNodeAttribute>>();

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

        /// <summary>
        /// update and re-render page node
        /// </summary>
        public void CommitDataChange()
        {
            // 寻找那些最大的子树然后重新渲染
            // 广度优先遍历树
            List<Node> nodesNeedToReRender = new List<Node>();
            //Dictionary<Node, bool> nodeDirty = new Dictionary<Node, bool>(); // HashSet ???? not avaliable in .Net 2.0
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);


            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                //if (nodeDirty.ContainsKey())
                //{

                //}
            }

            //foreach (var bind in effectBind.Values)
            //{
            //    if (bind.IsDirty)
            //    {
            //    }
            //}
        }


        //void BindXMLNodeWithNode(XmlNode element, Node node, Node parentNode)
        //{
        //    AddRangeElementList();
        //}

        void BindIfExpressWithNode(TemplateRendererNode tnode, IfDataBindExpress express, Node node, Node parentNode)
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

        void BindForExpressWithNode(TemplateRendererNode tnode, ForDataBindExpress express, Node node, Node parentNode)
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

        void BindAttributeExpressWithNode(TemplateRendererNode tnode, AttributeDataBindExpress express, Node node, Node parentNode)
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

        void BindTextExpressWithNode(TemplateRendererNode tnode, TextDataBindExpress express, Node node, Node parentNode)
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

        /// <summary>
        /// update data in value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void SetData(string key, object data)
        {
            if (key == null)
                return;

            //LinkedList<RuntimeNodeAttribute> nodeAttributeList;
            //if (attributeEffectBind.TryGetValue(key, out nodeAttributeList))
            //{
            //    foreach (RuntimeNodeAttribute nodeAttribute in nodeAttributeList)
            //    {
            //        foreach (var attrExp in nodeAttribute.attributeDataBindExpressList)
            //        {
            //            if (key.Equals(attrExp.TargetKey))
            //            {
            //                object result = attrExp.TryEvaluate(contextStack, out object value) ? value : null;
            //                nodeAttribute.attributes[key] = result;
            //                ProcessNodeStyle(nodeAttribute.node, attrExp.TargetName, value != null ? value.ToString() : "");
            //                return;
            //            }
            //        }
            //    }
            //}
            //else if (textEffectBind.TryGetValue(key, out nodeAttributeList))
            //{
            //    foreach (RuntimeNodeAttribute nodeAttribute in nodeAttributeList)
            //    {
            //        nodeAttribute.textDataBindExpressCurrentValue = nodeAttribute.textDataBindExpress.Evaluate(contextStack);
            //    }
            //}
            //else if (forExpressEffectBind.TryGetValue(key, out nodeAttributeList))
            //{
            //    foreach (RuntimeNodeAttribute childNodeAttribute in nodeAttributeList)
            //    {
            //        // remove old child
            //        var parentNode = childNodeAttribute.node.Parent;
            //        var parentNodeAttribute = GetNodeRuntimeAttribute(parentNode);

            //        foreach (var range in parentNodeAttribute.childernNodeElemetRangeList)
            //        {
            //            var testEle = range.element;
            //            foreach (var eledChildNode in range.nodes)
            //            {
            //                if (testEle == childNodeAttribute.element)
            //                {
            //                    parentNode.RemoveChild(eledChildNode);
            //                }
            //            }
            //        }

            //        // evaluate the value
            //        parentNodeAttribute.textDataBindExpressCurrentValue = parentNodeAttribute.textDataBindExpress.Evaluate(contextStack);


            //        // add to the ...

            //    }
            //}
        }

        /// <summary>
        /// return null is no key on ...
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
        {
            //return effectBind.TryGetValue(key, out DataBindContext data) ? data.Data : null;
            return null;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Xml;

namespace Rockyfi
{
    public partial class Factory
    {
        #region DataBind
        public class NodeAttribute
        {
            internal Node parentNode;
            internal Node node;
            internal bool IsDirty = true; // is this node need to re-calculate/render ?
            internal IfDataBindExpress ifExpress = null;
            internal ForDataBindExpress forExpress = null;
            internal TextDataBindExpress textDataBindExpress = null;
            internal readonly LinkedList<AttributeDataBindExpress> attributeDataBindExpressList = new LinkedList<AttributeDataBindExpress>();
            internal readonly Dictionary<string, object> attributes = new Dictionary<string, object>();
            internal readonly List<Node> nodes = new List<Node>();
        }

        NodeAttribute GetNodeCustomAttribute(Node node)
        {
            NodeAttribute attr = node.Context as NodeAttribute;
            if (attr == null)
            {
                attr = new NodeAttribute();
                attr.node = node;
                node.Context = attr;
            }
            return attr;
        }

        class DataBind
        {

        }
        readonly Dictionary<string, NodeAttribute> effectBind = new Dictionary<string, NodeAttribute>();

        /// <summary>
        /// insert child
        /// </summary>
        public void InsertChild(Node node, Node child, int idx)
        {
            // TODO: data check
            Rockyfi.InsertChild(node, child, idx);
        }

        // delete
        public void RemoveChild(Node node, Node child)
        {
            // TODO: data check
            Rockyfi.RemoveChild(node, child);
        }

        /// <summary>
        /// update and re-render page node
        /// </summary>
        public void CommitDataChange()
        {
            // 寻找那些最大的子树然后重新渲染
            // 广度优先遍历树
            List<Node> nodesNeedToReRender = new List<Node>();
            Dictionary<Node, bool> nodeDirty = new Dictionary<Node, bool>(); // HashSet ???? not avaliable in .Net 2.0
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


        void BindIfExpressWithNode(XmlNode element, IfDataBindExpress express, Node node, Node parentNode)
        {
            if (express == null || node == null)
                return;

            if (!express.IsEffectedByContext && express.TargetKeys != null)
            {
                // TODO:
                //if (effectBind.TryGetValue(key, out DataBindContext bind))
                //{
                //    bind.Data = "";
                //}
            }
        }

        void BindForExpressWithNode(XmlNode element, ForDataBindExpress express, Node node, Node parentNode)
        {
            // TODO:
        }

        void BindAttributeExpressWithNode(XmlNode element, AttributeDataBindExpress express, Node node, Node parentNode)
        {
            // TODO:

        }

        /// <summary>
        /// update data in value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void SetData(string key, object data)
        {
            //if (effectBind.TryGetValue(key, out DataBindContext bind))
            //{
            //    bind.Data = data;
            //}
            //else // no need to update
            //{
            //    var newBind = new DataBind();
            //    newBind.Data = data;
            //    effectBind.Add(key, newBind);
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

using System;
using System.Collections.Generic;
using System.Xml;

namespace Rockyfi
{
    public partial class Factory
    {
        class VirtualNode
        {
            bool IsDirty = true; // is this node need to re-calculate/render ?
            IfDataBindExpress ifExpress = null;
            ForDataBindExpress forExpress = null;
        }


        #region DataBind
        public DataBindContext CreateDataContext(object obj)
        {
            var dbc = new DataBindContext();
            dbc.Data = obj;
            return dbc;
        }

        public class DataBindContext
        {
            object data;
            public object Data
            {
                get { return data; }

                set
                {
                    if (data != null)
                    {
                        if (!data.Equals(value))
                        {
                            IsDirty = true;
                        }
                    }
                    else
                    {
                        if (value != null)
                        {
                            this.IsDirty = true;
                        }
                    }

                    data = value;
                }
            }
            public bool IsDirty { internal get; set; }
            public bool GetAsBool() { return (bool)data; }
            public IEnumerable<object> GetAsEnumerable() { return data as IEnumerable<object>; }
            readonly internal List<Node> nodes = new List<Node>();
        }
        readonly Dictionary<string, DataBindContext> effectBind = new Dictionary<string, DataBindContext>();

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

            foreach (var bind in effectBind.Values)
            {
                if (bind.IsDirty)
                {
                }
            }
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
            if (effectBind.TryGetValue(key, out DataBindContext bind))
            {
                bind.Data = data;
            }
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
            return effectBind.TryGetValue(key, out DataBindContext data) ? data.Data : null;
        }

        #endregion
    }
}

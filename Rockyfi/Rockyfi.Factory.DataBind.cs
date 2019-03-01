using System;
using System.Collections.Generic;

namespace Rockyfi
{
    public partial class Factory
    {
        //class VirtualNode
        //{
        //    internal VirtualNode Parent = null;
        //    internal readonly List<VirtualNode> Childern = new List<VirtualNode>();
        //    internal List<Node> RealNodes = new List<Node>();

        //    internal bool IsDirty; // is this node need to re-calculate/render ?
        //    internal DataBindIfExpress ifExpress;
        //    internal DataBindForExpress forExpress;
        //}
        class VirtualNode
        {
            internal VirtualNode Parent = null;
            internal readonly List<VirtualNode> Childern = new List<VirtualNode>();
            internal List<Node> RealNodes = new List<Node>();

            internal bool IsDirty; // is this node need to re-calculate/render ?
            internal IfDataBindExpress ifExpress;
            internal ForDataBindExpress forExpress;
        }

        //const string AttributeKey = "";

        #region DataBind

        readonly Dictionary<string, ObjectDataBindExpress> dataBindObjectExpressList = new Dictionary<string, ObjectDataBindExpress>();
        readonly Dictionary<string, ForDataBindExpress> dataBindForExpressList = new Dictionary<string, ForDataBindExpress>();
        readonly Dictionary<string, IfDataBindExpress> dataBindIfExpressList = new Dictionary<string, IfDataBindExpress>();

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
        private static bool TryGetObjectPath(string[] objPath, int index, object input, out object obj)
        {
            obj = null;
            // current is final ?
            if (index == objPath.Length - 1)
            {
                obj = input;
                return input == null;
            }


            if (input == null)
            {
                return false;
            }

            // an map ?
            var nextKey = objPath[index + 1];
            var dictionary = input as Dictionary<string, object>;
            if (dictionary != null)
            {
                if(dictionary.TryGetValue(nextKey, out obj))
                {
                    return true;
                }
            }

            // reflection
            Type t = input.GetType();
            System.Reflection.PropertyInfo[] properties = t.GetProperties();
            System.Reflection.FieldInfo[] fieldInfos = t.GetFields();

            foreach (var p in properties)
            {
                if (p.Name == nextKey)
                {
                    var m = p.GetGetMethod();
                    if (m == null)
                    {
                        return false;
                    }
                    return TryGetObjectPath(objPath, index + 1, m.Invoke(input, null), out obj);
                }
            }
            foreach (var f in fieldInfos)
            {
                if (f.Name == nextKey)
                {
                    return TryGetObjectPath(objPath, index + 1, f.GetValue(input), out obj);
                }
            }

            return false;
        }
        static bool TryGetObject(ContextStack contextStack, string[] objPath, out object outObj)
        {
            outObj = null;
            if (objPath == null || objPath.Length == 0)
            {
                return false;
            }
            if (contextStack.TryGet(objPath[0], out var dataBind))
            {
                return TryGetObjectPath(objPath, 0, dataBind.Data, out outObj);
            }
            return false;
        }

        class ContextStack
        {
            // TODO : we can do more optimization here !
            // Sparse linked list access.
            // Inert stack ??
            // Artificial/Real statc
            class ContextRealNode
            {

            }


            readonly LinkedList<Dictionary<string, DataBindContext>> contextStack = new LinkedList<Dictionary<string, DataBindContext>>();
            public ContextStack(Dictionary<string, DataBindContext> topContext)
            {
                contextStack.AddLast(topContext);
            }

            public ContextStack()
            {
                contextStack.AddLast(new Dictionary<string, DataBindContext>());
            }


            public bool TryGet(string name, out DataBindContext bindContext)
            {
                return (bindContext = Get(name)) != null;
            }

            public DataBindContext Get(string name)
            {
                var node = contextStack.Last;
                while (node != null)
                {
                    var dictionary = node.Value;
                    if (dictionary.TryGetValue(name, out DataBindContext value))
                        return value;

                    node = node.Previous;
                }
                return null;
            }

            public void Set(string name, DataBindContext value)
            {
                contextStack.Last.Value["name"] = value;
            }

            public Dictionary<string, DataBindContext> LeaveScope()
            {
                var last = contextStack.Last != null ? contextStack.Last.Value : null;
                contextStack.RemoveLast();
                return last;
            }

            public void EnterScope()
            {
                contextStack.AddLast(new Dictionary<string, DataBindContext>());
            }
        }

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

        void BindExpressWithNode<TR>(IDataBindExpress<TR> express, Node node)
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

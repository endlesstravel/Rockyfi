using System;
using System.Collections.Generic;

namespace Rockyfi
{
    public partial class Factory
    {
        const string AttributeKey = "";

        #region DataCenter

        class DataBind
        {
            object data;

            public object Data
            {
                get
                {
                    return data;
                }

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

            public bool IsDirty
            {
                internal get;
                set;
            }

            internal List<Node> nodes;
        }
        readonly Dictionary<string, DataBind> effectBind = new Dictionary<string, DataBind>();
        internal bool TryGetObjectPath(string[] objPath, int index, object input, out object obj)
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
        internal bool TryGetObject(string[] objPath, out object obj)
        {
            obj = null;
            if (objPath == null || objPath.Length == 0)
            {
                return false;
            }
            if (effectBind.TryGetValue(objPath[0], out var dataBind))
            {
                return TryGetObjectPath(objPath, 0, dataBind.Data, out obj);
            }
            return false;
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
            foreach (var bind in effectBind.Values)
            {
                if (bind.IsDirty)
                {

                }
            }
        }

        /// <summary>
        /// update data in value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        void SetData(string key, object data)
        {
            if (effectBind.TryGetValue(key, out DataBind bind))
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
        object GetData(string key)
        {
            return effectBind.TryGetValue(key, out DataBind data) ? data.Data : null;
        }

        #endregion
    }
}

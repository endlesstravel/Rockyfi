using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Rockyfi
{
    public partial class Factory
    {
        const string AttributeKey = "";

        #region DataCenter

        // The Regex class itself is thread safe and immutable(read-only). That is, 
        // Regex objects can be created on any thread and shared between threads; 
        // matching methods can be called from any thread and never alter any global state.
        // However, result objects (Match and MatchCollection) returned by Regex should be used on a single thread..
        internal static Regex strRegex = new Regex(@"^'([^']*)'$");
        internal static Regex objRegex = new Regex(@"^(([a-zA-Z]\w*)(\.([a-zA-Z]\w*))*)$");
        internal static Regex ifRegex = new Regex(@"^([^=]+)\s*(==\s*([^=]+))?$");


        // xxx.yy.zz -> [xxx, yy, zz]
        static bool TryParseDotValue(string input, out string[] result)
        {
            if (objRegex.IsMatch(input))
            {
                result = input.Split('.');
                return true;
            }
            result = null;
            return false;
        }

        static bool TryParseStringValue(string input, out string result)
        {
            if (strRegex.IsMatch(input))
            {
                result = input.Substring(1, input.Length - 2);
                return true;
            }
            result = null;
            return false;
        }

        enum DataBindObjectType
        {
            Unknow,
            Integer,
            Float,
            String,
            ObjectSymbol,
        }

        /// <summary>
        /// 11  : int
        /// 11.11  : float
        /// 'hello ?': string
        /// true: boolean
        /// false: boolean
        /// xxx.bc.fd: object value
        /// </summary>
        class DataBindObjectExpress
        {
            string express;
            DataBindObjectType type = DataBindObjectType.Unknow;
            object value;
            object cachedValue;
            internal void UpdateExpress(string exp, Factory factory)
            {
                if (express == exp)
                    return;

                express = exp;
                if (int.TryParse(express, out int intResult))
                {
                    value = intResult;
                    type = DataBindObjectType.Integer;
                }
                else if (float.TryParse(express, out float floatResult))
                {
                    value = intResult;
                    type = DataBindObjectType.Float;
                }
                else if (TryParseDotValue(express, out string[] objResult))
                {
                    value = intResult;
                    factory.TryGetObject(objResult, out cachedValue);
                    type = DataBindObjectType.ObjectSymbol;
                }
                else if (TryParseStringValue(express, out string strResult))
                {
                    value = intResult;
                    type = DataBindObjectType.String;
                }
            }

        }

        /// <summary>
        /// ^\s*([\w\.]+)\s*(==\s*([\w\.]+))?\s*$
        /// ^\s*([\w\.]+)\s*(==\s*([\w\.]+))?\s*$
        /// case 1: 
        ///    bind:if="item"
        ///    string is null or empty ?
        ///    number is 0 ?
        /// case 2:
        /// </summary>
        class DataBindIfExpress
        {
            string express;
            bool isMono;
            DataBindObjectExpress leftExpress;
            DataBindObjectExpress RightExpress;

            void InitExpress(Factory factory)
            {
                Match match = ifRegex.Match(express);
                if(match.Success)
                {
                    isMono = match.Groups.Count == 2;
                    if (isMono)
                    {

                    }
                }
            }

            //bool Evaluate(Factory factory)
            //{
            //    ifRegex.Match(express);
            //}
        }
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

        internal void ResoloveObjectValue(string express)
        {

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

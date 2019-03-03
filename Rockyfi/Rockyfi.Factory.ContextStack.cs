using System;
using System.Collections.Generic;

namespace Rockyfi
{
    public partial class Factory
    {
        internal class ContextStack
        {
            // TODO : we can do more optimization here !
            // Sparse linked list access.
            // Inert stack ??
            // Artificial/Real statc
            class ContextRealNode
            {

            }

            readonly LinkedList<Dictionary<string, object>> contextStack = new LinkedList<Dictionary<string, object>>();
            public ContextStack(Dictionary<string, object> topContext)
            {
                contextStack.AddLast(topContext != null ? topContext : new Dictionary<string, object>());
            }

            public bool TryGetFromPath(string[] objPath, out object outObj)
            {
                outObj = null;
                if (objPath == null || objPath.Length == 0)
                {
                    return false;
                }
                if (TryGet(objPath[0], out var bindContext))
                {
                    return TryGetObjectPath(objPath, 0, bindContext, out outObj);
                }
                return false;
            }

            public bool TryGet(string name, out object bindContext)
            {
                return (bindContext = Get(name)) != null;
            }

            public object Get(string name)
            {
                var node = contextStack.Last;
                while (node != null)
                {
                    var dictionary = node.Value;
                    if (dictionary.TryGetValue(name, out object value))
                        return value;

                    node = node.Previous;
                }
                return null;
            }

            public void Set(string name, object value)
            {
                contextStack.Last.Value[name] = value;
            }

            public Dictionary<string, object> LeaveScope()
            {
                var last = contextStack.Last != null ? contextStack.Last.Value : null;
                contextStack.RemoveLast();
                return last;
            }

            public void EnterScope()
            {
                contextStack.AddLast(new Dictionary<string, object>());
            }

            static bool TryGetObjectPath(string[] objPath, int index, object input, out object obj)
            {
                obj = null;
                // current is final ?
                if (index == objPath.Length - 1)
                {
                    obj = input;
                    return input != null;
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
                    if (dictionary.TryGetValue(nextKey, out obj))
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
        }

    }
}

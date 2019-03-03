using System;
using System.Collections.Generic;
using System.Text;

namespace Rockyfi
{
    public partial class Factory
    {
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

            public bool TryGetFromPath(string[] objPath, out object outObj)
            {
                outObj = null;
                if (objPath == null || objPath.Length == 0)
                {
                    return false;
                }
                if (TryGet(objPath[0], out var dataBind))
                {
                    return TryGetObjectPath(objPath, 0, dataBind.Data, out outObj);
                }
                return false;
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
                contextStack.Last.Value[name] = value;
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

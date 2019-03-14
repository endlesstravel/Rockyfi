using System;
using System.Collections.Generic;
using System.Text;

namespace Rockyfi
{
    class LiteSet<T>
    {
        readonly Dictionary<T, bool> dict = new Dictionary<T, bool>();

        public void Add(T item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }
            dict[item] = true;
        }
        public void Clear()
        {
            dict.Clear();
        }
        public bool Contains(T item)
        {
            return dict.ContainsKey(item);
        }
        public bool Remove(T item)
        {
            return dict.Remove(item);
        }
        public int Count
        {
            get { return dict.Count; }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Rockyfi
{
    internal class CardData
    {
        public class ListData
        {
            internal readonly CardData cardData;
            public ListData(CardData cardData)
            {
                this.cardData = cardData;
            }

            readonly List<object> listData = new List<object>();

            public void Set(int index, bool value)
            {
                object oldData = listData[index];
                listData[index] = value;
                cardData.ListDataChangedEvent?.Invoke(index, value, oldData);
            }
            public void Set(int index, int value)
            {
                object oldData = listData[index];
                listData[index] = value;
                cardData.ListDataChangedEvent?.Invoke(index, value, oldData);
            }
            public void Set(int index, float value)
            {
                object oldData = listData[index];
                listData[index] = value;
                cardData.ListDataChangedEvent?.Invoke(index, value, oldData);
            }
            public void Set(int index, double value)
            {
                object oldData = listData[index];
                listData[index] = value;
                cardData.ListDataChangedEvent?.Invoke(index, value, oldData);
            }
            public void Set(int index, string value)
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                object oldData = listData[index];
                listData[index] = value;
                cardData.ListDataChangedEvent?.Invoke(index, value, oldData);
            }
            public void Set(int index, CardData value)
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                object oldData = listData[index];
                listData[index] = value;
                cardData.ListDataChangedEvent?.Invoke(index, value, oldData);
            }
            public void Add(bool value)
            {
                listData.Add(value);
                cardData.ListDataInsertEvent?.Invoke(listData.Count, value);
            }
            public void Add(int value)
            {
                listData.Add(value);
                cardData.ListDataInsertEvent?.Invoke(listData.Count, value);
            }
            public void Add(float value)
            {
                listData.Add(value);
                cardData.ListDataInsertEvent?.Invoke(listData.Count, value);
            }
            public void Add(double value)
            {
                listData.Add(value);
                cardData.ListDataInsertEvent?.Invoke(listData.Count, value);
            }
            public void Add(string value)
            {
                listData.Add(value);
                cardData.ListDataInsertEvent?.Invoke(listData.Count, value);
            }
            public void Add(CardData value)
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                listData.Add(value);
                cardData.ListDataInsertEvent?.Invoke(listData.Count, value);
            }


            public void Insert(int index, bool value)
            {
                listData.Insert(index, value);
                cardData.ListDataInsertEvent?.Invoke(index, value);
            }
            public void Insert(int index, int value)
            {
                listData.Insert(index, value);
                cardData.ListDataInsertEvent?.Invoke(index, value);
            }
            public void Insert(int index, float value)
            {
                listData.Insert(index, value);
                cardData.ListDataInsertEvent?.Invoke(index, value);
            }
            public void Insert(int index, double value)
            {
                listData.Insert(index, value);
                cardData.ListDataInsertEvent?.Invoke(index, value);
            }
            public void Insert(int index, string value)
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                listData.Insert(index, value);
                cardData.ListDataInsertEvent?.Invoke(index, value);
            }

            public void Insert(int index, CardData value)
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                listData.Insert(index, value);
                cardData.ListDataInsertEvent?.Invoke(index, value);
            }

            public bool Remove(object key)
            {
                var index = listData.IndexOf(key);
                if (index != -1)
                {
                    listData.RemoveAt(index);
                    cardData.ListDataRemoveEvent(index);
                    return true;
                }

                return false;
            }

            public void RemoveAt(int index)
            {
                listData.RemoveAt(index);
                cardData.ListDataRemoveEvent(index);
            }

            public void Clear()
            {
                listData.Clear();
                cardData.ListDataClearEvent?.Invoke();
            }

            #region read only properties
            public bool TryGetValue(int index, out bool value)
            {
                value = false;
                if (index < 0 || listData.Count <= index)
                    return false;
                var obj = listData[index];
                if (obj is bool)
                {
                    value = (bool)obj;
                    return true;
                }

                return false;
            }

            public bool TryGetValue(int index, out int value)
            {
                value = 0;
                if (index < 0 || listData.Count <= index)
                    return false;

                var obj = listData[index];
                if (obj is int)
                {
                    value = (int)obj;
                    return true;
                }

                return false;
            }


            public bool TryGetValue(int index, out float value)
            {
                value = 0;
                if (index < 0 || listData.Count <= index)
                    return false;
                var obj = listData[index];
                if (obj is float)
                {
                    value = (float)obj;
                    return true;
                }

                return false;
            }
            public bool TryGetValue(int index, out double value)
            {
                value = 0;
                if (index < 0 || listData.Count <= index)
                    return false;
                var obj = listData[index];
                if (obj is double)
                {
                    value = (double)obj;
                    return true;
                }

                return false;
            }

            public bool TryGetValue(int index, out string value)
            {
                value = null;
                if (index < 0 || listData.Count <= index)
                    return false;
                var obj = listData[index];
                if (obj is string)
                {
                    value = (string)obj;
                    return true;
                }

                return false;
            }

            public bool TryGetValue(int index, out CardData value)
            {
                value = null;
                if (index < 0 || listData.Count <= index)
                    return false;


                var obj = listData[index];
                if (obj is CardData)
                {
                    value = (CardData)obj;
                    return true;
                }

                return false;
            }

            public bool Contains(object value)
            {
                return listData.Contains(value);
            }

            public int IndexOf(object value)
            {
                return listData.IndexOf(value);
            }

            public int Count { get { return listData.Count; } }
            #endregion
        }
        readonly ListData List;
        readonly Dictionary<string, object> dataDict = new Dictionary<string, object>();

        public CardData()
        {
            List = new ListData(this);
        }

        public void AddDataChangedWatcher(DataChanged dataChanged)
        {
            DataChangedEvent += dataChanged;
        }

        public delegate void DataChanged(string name, object newValue, object oldValue);
        public delegate void ListDataChanged(int index, object newValue, object oldValue);
        public delegate void ListDataInsert(int index, object value);
        public delegate void ListDataRemove(int index);
        public delegate void ListDataClear();

        internal event DataChanged DataChangedEvent;
        internal event ListDataChanged ListDataChangedEvent;
        internal event ListDataInsert ListDataInsertEvent;
        internal event ListDataRemove ListDataRemoveEvent;
        internal event ListDataClear ListDataClearEvent;

        public void Set(string key, int value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            dataDict.TryGetValue(key, out var oldValue);
            dataDict[key] = value;
            DataChangedEvent?.Invoke(key, value, oldValue);
        }
        public void Set(string key, float value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            dataDict.TryGetValue(key, out var oldValue);
            dataDict[key] = value;
            DataChangedEvent?.Invoke(key, value, oldValue);
        }
        public void Set(string key, double value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            dataDict.TryGetValue(key, out var oldValue);
            dataDict[key] = value;
            DataChangedEvent?.Invoke(key, value, oldValue);
        }
        public void Set(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            dataDict.TryGetValue(key, out var oldValue);
            dataDict[key] = value;
            DataChangedEvent?.Invoke(key, value, oldValue);
        }
        public void Set(string key, CardData value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            dataDict.TryGetValue(key, out var oldValue);
            dataDict[key] = value;
            DataChangedEvent?.Invoke(key, value, oldValue);
        }

        public bool TryGetValue(string key, out int value)
        {
            if (dataDict.TryGetValue(key, out var obj) && obj is int)
            {
                value = (int)obj;
                return true;
            }

            value = 0;
            return false;
        }

        public bool TryGetValue(string key, out float value)
        {
            if (dataDict.TryGetValue(key, out var obj) && obj is float)
            {
                value = (float)obj;
                return true;
            }

            value = 0;
            return false;
        }

        public bool TryGetValue(string key, out double value)
        {
            if (dataDict.TryGetValue(key, out var obj) && obj is double)
            {
                value = (double)obj;
                return true;
            }

            value = 0;
            return false;
        }

        public bool TryGetValue(string key, out string value)
        {
            if (dataDict.TryGetValue(key, out var obj) && obj is string)
            {
                value = (string)obj;
                return true;
            }

            value = null;
            return false;
        }

        public bool TryGetValue(string key, out CardData value)
        {
            if (dataDict.TryGetValue(key, out var obj) && obj is CardData)
            {
                value = (CardData)obj;
                return true;
            }

            value = null;
            return false;
        }

        public bool TryGetValue(string key, out object value)
        {
            if (dataDict.TryGetValue(key, out var obj))
            {
                value = obj;
                return true;
            }

            value = null;
            return false;
        }

        public bool Remove(string key)
        {
            if (dataDict.TryGetValue(key, out var oldValue) && dataDict.Remove(key))
            {
                DataChangedEvent?.Invoke(key, null, oldValue);
                return true;
            }
            return false;
        }

        public Dictionary<string, object>.Enumerator GetEnumerator()
        {
            return dataDict.GetEnumerator();
        }

        public int Count { get { return dataDict.Count; } }
    }
}

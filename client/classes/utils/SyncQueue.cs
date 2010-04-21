using System;
using System.Collections.Generic;

namespace com.jds.GUpdater.classes.utils
{
    public class SyncQueue<T> : IEnumerable<T>
    {
        private readonly List<T> _list = new List<T>();
        private static readonly object _lock = new object();

        public void AddLast(T item)
        {
            lock (_lock)
            {
                _list.Add((item));  
            }   
        }

        /*public object Next
        {
            get
            {
                lock (_lock)
                {

                    if (_list.Count == 0)
                    {
                        return null;
                    }
                    LinkedListNode<T> value = _list.First;
                    _list.Remove(value);
                    return value.Value;
                }
            }
        }*/

        public void Remove(T val)
        {
            lock (_lock)
            {
                _list.Remove(val);
            }
        }

        public T[] ToArray
        {
            get
            {
                lock (_lock)
                {
                    return _list.ToArray();
                }
            }
        }

        public int Count
        {
            get
            {
             //   lock (_lock)
                {
                    return _list.Count;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return _list.GetEnumerator();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            lock (_lock)
            {
                return _list.GetEnumerator();
            }
        }
    }

}
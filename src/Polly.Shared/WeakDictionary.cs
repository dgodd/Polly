using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Polly.Shared
{
    public class WeakDictionary<K, V> where V : class
    {
        private HashSet<KeyValuePair<K, WeakReference<V>>> list;

        public WeakDictionary()
        {
            list = new HashSet<KeyValuePair<K, WeakReference<V>>>();
        }

        public void Add(K k, V v)
        {
            list.Add(new KeyValuePair<K, WeakReference<V>>(k, new WeakReference<V>(v)));
        }

        public IEnumerable<KeyValuePair<K, V>> All()
        {
            var cloneList = new HashSet<KeyValuePair<K, WeakReference<V>>>(list);
            foreach (var pair in cloneList)
            {
                V value;
                if (pair.Value.TryGetTarget(out value))
                {
                    yield return new KeyValuePair<K,V>(pair.Key, value);
                }
                else
                {
                    list.Remove(pair);
                }
            }
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Polly.Shared
{
    public class WeakDictionary<K, V>
    {
        private HashSet<KeyValuePair<K, WeakReference>> list;

        public WeakDictionary()
        {
            list = new HashSet<KeyValuePair<K, WeakReference>>();
        }

        public void Add(K k, V v)
        {
            list.Add(new KeyValuePair<K, WeakReference>(k, new WeakReference(v)));
        }

        public IEnumerable<KeyValuePair<K, V>> All()
        {
            var cloneList = new HashSet<KeyValuePair<K, WeakReference>>(list);
            foreach (var pair in cloneList)
            {
                object value = pair.Value.Target;
                if (value != null)
                {
                    yield return new KeyValuePair<K,V>(pair.Key, (V)value);
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

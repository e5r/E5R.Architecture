// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace E5R.Architecture.Core
{
    public class Parameters : IDictionary<string, object>
    {
        private readonly Dictionary<string, List<object>> _parameters;

        public Parameters()
        {
            _parameters = new Dictionary<string, List<object>>();
        }

        public object this[string key]
        {
            get
            {
                TryGetValue(key, out object value);
                return value;
            }
            set => Add(key, value);
        }

        public ICollection<string> Keys => _parameters.Keys;

        public ICollection<object> Values => _parameters.Values.SelectMany(m => m).ToList();

        public int Count => _parameters.Count;

        public bool IsReadOnly => false;

        public void Add(string key, object value)
            => Add(new KeyValuePair<string, object>(key, value));

        public void Add(KeyValuePair<string, object> item)
        {
            List<object> values;

            if (!_parameters.TryGetValue(item.Key, out values))
            {
                values = new List<object>();
                _parameters.Add(item.Key, values);
            }

            values.Add(item.Value);
        }

        public void Clear() => _parameters.Clear();

        public bool Contains(KeyValuePair<string, object> item)
        {
            if (_parameters.TryGetValue(item.Key, out List<object> values))
            {
                return values.Contains(item);
            }

            return false;
        }

        public bool ContainsKey(string key) => _parameters.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            if (arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            for (var currentIndex = 0; currentIndex < array.Length; currentIndex++)
            {
                Add(array[currentIndex]);
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            => _parameters
            .Select(s => new KeyValuePair<string, object>(s.Key, s.Value))
            .GetEnumerator();

        public bool Remove(string key) => _parameters.Remove(key);

        public bool Remove(KeyValuePair<string, object> item)
        {
            if (_parameters.TryGetValue(item.Key, out List<object> values))
            {
                return values.Remove(item);
            }

            return false;
        }

        public bool TryGetValue(string key, out object value)
        {
            if (_parameters.TryGetValue(key, out List<object> allValues))
            {
                value = allValues;
                return true;
            }

            value = null;

            return false;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            if (_parameters.TryGetValue(key, out List<object> allValues))
            {
                var firstItem = allValues.FirstOrDefault();

                value = (T)firstItem;

                return true;
            }

            value = default(T);

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => _parameters.GetEnumerator();
    }
}

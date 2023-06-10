using System;
using System.Collections.Generic;
using System.Linq;

namespace Kiwi.Utilities.Extensions
{
    public static class DictionaryExtensions
    {
        public static int RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            return dictionary
                .Where(predicate)
                .Select(pair => pair.Key)
                .ToList()
                .Count(dictionary.Remove);
        }
    }
}
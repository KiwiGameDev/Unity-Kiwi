using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Kiwi.Utilities.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Jon Skeet's excellent reimplementation of LINQ Count.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <param name="source">The source IEnumerable.</param>
        /// <returns>The number of items in the source.</returns>
        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            Assert.IsNotNull(source, "source cannot be null");

            // Optimization for ICollection<T>
            if (source is ICollection<TSource> genericCollection)
                return genericCollection.Count;

            // Optimization for ICollection
            if (source is ICollection nonGenericCollection)
                return nonGenericCollection.Count;

            // Do it the slow way - and make sure we overflow appropriately
            checked
            {
                int count = 0;
                using IEnumerator<TSource> iterator = source.GetEnumerator();

                while (iterator.MoveNext())
                {
                    count++;
                }

                return count;
            }
        }

        public static TItem MinBy<TItem>(this IEnumerable<TItem> items, [NotNull] Func<TItem, float> valueSelector)
        {
            float minItemValue = float.MaxValue;
            TItem minItem = default;

            foreach (TItem currentItem in items)
            {
                float currentItemValue = valueSelector(currentItem);

                if (currentItemValue < minItemValue)
                {
                    minItem = currentItem;
                    minItemValue = currentItemValue;
                }
            }

            return minItem;
        }

        public static TItem MinBy<TItem>(this IEnumerable<TItem> items, [NotNull] Func<TItem, int> valueSelector)
        {
            int minItemValue = int.MaxValue;
            TItem minItem = default;

            foreach (TItem currentItem in items)
            {
                int currentItemValue = valueSelector(minItem);

                if (currentItemValue < minItemValue)
                {
                    minItem = currentItem;
                    minItemValue = currentItemValue;
                }
            }

            return minItem;
        }
    }
}
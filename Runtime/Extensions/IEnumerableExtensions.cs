using System.Collections.Generic;
using System.Collections;
using UnityEngine.Assertions;

namespace Kiwi.Extensions
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
                using var iterator = source.GetEnumerator();

                while (iterator.MoveNext())
                {
                    count++;
                }

                return count;
            }
        }
    }
}
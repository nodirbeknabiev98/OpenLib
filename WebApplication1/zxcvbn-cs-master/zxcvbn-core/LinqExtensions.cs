﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Zxcvbn
{
    /// <summary>
    /// Useful shared Linq extensions
    /// </summary>
    internal static class LinqExtensions
    {
        /// <summary>
        /// Used to group elements by a key function, but only where elements are adjacent
        /// </summary>
        /// <param name="keySelector">Function used to choose the key for grouping</param>
        /// <param name="source">THe enumerable being grouped</param>
        /// <returns>An enumerable of <see cref="AdjacentGrouping{TKey, TElement}"/> </returns>
        /// <typeparam name="TKey">Type of key value used for grouping</typeparam>
        /// <typeparam name="TSource">Type of elements that are grouped</typeparam>
        public static IEnumerable<AdjacentGrouping<TKey, TSource>> GroupAdjacent<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var prevKey = default(TKey);
            var prevStartIndex = 0;
            var prevInit = false;
            var itemsList = new List<TSource>();

            var i = 0;
            foreach (var item in source)
            {
                var key = keySelector(item);
                if (prevInit)
                {
                    if (!prevKey.Equals(key))
                    {
                        yield return new AdjacentGrouping<TKey, TSource>(key, itemsList, prevStartIndex, i - 1);

                        prevKey = key;
                        itemsList = new List<TSource>
                        {
                            item
                        };
                        prevStartIndex = i;
                    }
                    else
                    {
                        itemsList.Add(item);
                    }
                }
                else
                {
                    prevKey = key;
                    itemsList.Add(item);
                    prevInit = true;
                }

                i++;
            }

            if (prevInit) yield return new AdjacentGrouping<TKey, TSource>(prevKey, itemsList, prevStartIndex, i - 1);
        }

        /// <inheritdoc />
        /// <summary>
        /// A single grouping from the GroupAdjacent function, includes start and end indexes for the grouping in addition to standard IGrouping bits
        /// </summary>
        /// <typeparam name="TElement">Type of grouped elements</typeparam>
        /// <typeparam name="TKey">Type of key used for grouping</typeparam>
        public class AdjacentGrouping<TKey, TElement> : IGrouping<TKey, TElement>
        {
            private readonly IEnumerable<TElement> _mGroupItems;

            internal AdjacentGrouping(TKey key, IEnumerable<TElement> groupItems, int startIndex, int endIndex)
            {
                Key = key;
                StartIndex = startIndex;
                EndIndex = endIndex;
                _mGroupItems = groupItems;
            }

            /// <summary>
            /// The end index in the enumerable for this group (i.e. the index of the last element)
            /// </summary>
            public int EndIndex
            {
                get;
            }

            /// <inheritdoc />
            /// <summary>
            /// The key value for this grouping
            /// </summary>
            public TKey Key
            {
                get;
            }

            /// <summary>
            /// The start index in the source enumerable for this group (i.e. index of first element)
            /// </summary>
            public int StartIndex
            {
                get;
            }

            IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
            {
                return _mGroupItems.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _mGroupItems.GetEnumerator();
            }
        }
    }
}
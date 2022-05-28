using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlgoSdk.LowLevel
{
    public struct ListSlice<T, TList>
        : IReadOnlyList<T>
        where TList : IReadOnlyList<T>
    {
        readonly TList list;
        readonly int start;
        readonly int count;

        public ListSlice(TList list, int start, int count)
        {
            this.list = list;
            this.start = start;
            this.count = count;
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return list[start + index];
            }
        }

        public int Count => count;

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T, ListSlice<T, TList>>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T[] ToArray()
        {
            var result = new T[Count];
            for (var i = 0; i < Count; i++)
                result[i] = this[i];
            return result;
        }
    }
}

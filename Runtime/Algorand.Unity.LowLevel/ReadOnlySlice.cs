using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algorand.Unity.LowLevel
{
    public struct ReadOnlySlice<T, TList>
        : IReadOnlyList<T>
        where TList : IReadOnlyList<T>
    {
        private readonly TList list;
        private readonly int start;
        private readonly int count;

        public ReadOnlySlice(TList list, int start, int count)
        {
            this.list = list;
            this.start = start;
            this.count = count;
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return list[start + index];
            }
        }

        public int Count => count;

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T, ReadOnlySlice<T, TList>>(this);
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

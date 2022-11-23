using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;

namespace Algorand.Unity.LowLevel
{
    public struct NativeIndexer<T>
        : IReadOnlyList<T>
        , INativeDisposable
    {
        private readonly T[] array;
        private NativeList<int> indices;

        public NativeIndexer(
            T[] array,
            Allocator allocator
        )
        {
            this.array = array;
            this.indices = new NativeList<int>(array.Length, allocator);
        }

        public T this[int index] => array[indices[index]];

        public int Count => indices.Length;

        public void Add(int index)
        {
            indices.Add(index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T, NativeIndexer<T>>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            indices.Dispose();
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return indices.Dispose(inputDeps);
        }

        public ReadOnlySlice<T, NativeIndexer<T>> Slice(int start, int count = -1)
        {
            if (start < 0 || start >= Count)
                throw new ArgumentOutOfRangeException(nameof(start));

            if (count < 0)
                count = Count - start;
            else if (start + count >= Count)
                throw new ArgumentOutOfRangeException(nameof(count));

            return new ReadOnlySlice<T, NativeIndexer<T>>(this, start, count);
        }
    }
}

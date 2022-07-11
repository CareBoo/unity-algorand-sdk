using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk.LowLevel
{
    public struct NativeOrderedSet<T>
        : INativeDisposable
        , IReadOnlyList<T>
        where T : unmanaged, IEquatable<T>
    {
        NativeParallelHashSet<T> set;
        NativeList<T> list;

        public NativeOrderedSet(int initialCapacity, Allocator allocator)
        {
            this.set = new NativeParallelHashSet<T>(initialCapacity, allocator);
            this.list = new NativeList<T>(initialCapacity, allocator);
        }

        public T this[int index] { get => list[index]; }

        public int Capacity { get => list.Capacity; set => list.Capacity = value; }

        public bool IsEmpty => list.IsEmpty;

        public int Length { get => list.Length; }

        public int Count => Length;

        public bool Add(T element)
        {
            if (set.Add(element))
            {
                list.Add(element);
                return true;
            }
            return false;
        }

        public bool TryPop(out T element)
        {
            if (Length == 0)
            {
                element = default;
                return false;
            }

            var last = Length - 1;
            element = list[last];
            list.RemoveAt(last);
            set.Remove(element);
            return true;
        }

        public void RemoveAt(int index)
        {
            var element = this[index];
            list.RemoveAt(index);
            set.Remove(element);
        }

        public bool Contains(T element)
        {
            return set.Contains(element);
        }

        public int AddIndexOf(T element)
        {
            if (Add(element))
            {
                return Length - 1;
            }
            for (var i = 0; i < Length; i++)
                if (this[i].Equals(element))
                    return i;

            throw new System.InvalidOperationException("NativeOrderedSet is in invalid state. Set contains an element that does not exist in the list.");
        }

        public int IndexOf(T element)
        {
            if (!set.Contains(element))
                return -1;

            for (var i = 0; i < Length; i++)
                if (this[i].Equals(element))
                    return i;

            throw new System.InvalidOperationException("NativeOrderedSet is in invalid state. Set contains an element that does not exist in the list.");
        }

        public void Clear()
        {
            set.Clear();
            list.Clear();
        }

        public ReadOnlySlice<T, NativeOrderedSet<T>> Slice(int start, int count)
        {
            if (start > Count)
                throw new System.ArgumentOutOfRangeException(nameof(start));
            if (start + count > Count)
                throw new System.ArgumentOutOfRangeException(nameof(count));
            return new ReadOnlySlice<T, NativeOrderedSet<T>>(this, start, count);
        }

        public ReadOnlySlice<T, NativeOrderedSet<T>> Slice(int start) => Slice(start, Count - start);

        public T[] ToArray()
        {
            return list.ToArray();
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(
                set.Dispose(inputDeps),
                list.Dispose(inputDeps)
                );
        }

        public void Dispose()
        {
            set.Dispose();
            list.Dispose();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}

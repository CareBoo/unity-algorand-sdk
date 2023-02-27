using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Algorand.Unity.LowLevel
{
    public struct NativeOrderedSet<T>
        : INativeDisposable
        , IReadOnlyList<T>
        where T : unmanaged, IEquatable<T>
    {
        private NativeParallelHashSet<T> set;
        private NativeList<T> list;

        public NativeOrderedSet(int initialCapacity, Allocator allocator)
        {
            set = new NativeParallelHashSet<T>(initialCapacity, allocator);
            list = new NativeList<T>(initialCapacity, allocator);
        }

        public ref T this[int index] => ref list.ElementAt(index);
        
        T IReadOnlyList<T>.this[int index] => this[index];

        public int Capacity { get => list.Capacity; set => list.Capacity = value; }

        public bool IsEmpty => list.IsEmpty;

        public int Length => list.Length;

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

            throw new InvalidOperationException("NativeOrderedSet is in invalid state. Set contains an element that does not exist in the list.");
        }

        public int IndexOf(T element)
        {
            if (!set.Contains(element))
                return -1;

            for (var i = 0; i < Length; i++)
                if (this[i].Equals(element))
                    return i;

            throw new InvalidOperationException("NativeOrderedSet is in invalid state. Set contains an element that does not exist in the list.");
        }

        public void Clear()
        {
            set.Clear();
            list.Clear();
        }

        public ReadOnlySlice<T, NativeOrderedSet<T>> Slice(int start, int count)
        {
            if (start > Count)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (start + count > Count)
                throw new ArgumentOutOfRangeException(nameof(count));
            return new ReadOnlySlice<T, NativeOrderedSet<T>>(this, start, count);
        }

        public ReadOnlySlice<T, NativeOrderedSet<T>> Slice(int start) => Slice(start, Count - start);

        public T[] ToArray()
        {
            return list.AsArray().ToArray();
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

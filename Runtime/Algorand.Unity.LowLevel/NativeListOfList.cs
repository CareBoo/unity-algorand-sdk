using Unity.Collections;
using Unity.Jobs;

namespace Algorand.Unity.LowLevel
{
    public struct NativeListOfList<T>
        : INativeDisposable
        where T : unmanaged
    {
        private NativeList<T> list;
        private NativeList<int> indices;

        public NativeListOfList(Allocator allocator)
        {
            list = new NativeList<T>(allocator);
            indices = new NativeList<int>(allocator);
        }

        public NativeSlice<T> this[int index]
        {
            get
            {
                GetRange(index, out var start, out var count);
                return new NativeSlice<T>(list, start, count);
            }
        }

        public int Length => indices.Length;

        public bool IsCreated => list.IsCreated && indices.IsCreated;

        public bool IsEmpty => indices.IsEmpty;

        public void Add<TList>(TList val)
            where TList : IIndexable<T>
        {
            var start = list.Length;
            var count = val.Length;
            indices.Add(start);
            list.Length += count;
            for (var i = 0; i < count; i++)
                list[start + i] = val.ElementAt(i);
        }

        public void AddArray<TArray>(TArray arr)
            where TArray : IContiguousArray<T>
        {
            var start = list.Length;
            var count = arr.Length;
            indices.Add(start);
            list.Length += count;
            for (var i = 0; i < count; i++)
                list[start + i] = arr[i];
        }

        public void RemoveAt(int index)
        {
            GetRange(index, out var start, out var count);
            indices.RemoveAt(index);
            list.RemoveRange(start, count);
        }

        public void GetRange(int index, out int start, out int count)
        {
            start = indices[index];
            var next = index + 1;
            var end = next >= indices.Length ? list.Length : indices[next];
            count = end - start;
        }

        public T[][] ToArray()
        {
            var result = new T[Length][];
            for (var i = 0; i < Length; i++)
                result[i] = this[i].ToArray();

            return result;
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(
                list.Dispose(inputDeps),
                indices.Dispose(inputDeps)
            );
        }

        public void Dispose()
        {
            list.Dispose();
            indices.Dispose();
        }
    }
}

using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Algorand.Unity.MessagePack
{
    public partial struct MessagePackWriter
        : INativeDisposable
    {
        private NativeList<byte> data;

        public MessagePackWriter(Allocator allocator)
        {
            this.data = new NativeList<byte>(allocator);
        }

        public NativeList<byte> Data => data;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return data.IsCreated
                ? data.Dispose(inputDeps)
                : inputDeps;
        }

        public void Dispose()
        {
            if (data.IsCreated)
                data.Dispose();
        }

        public void WriteNil()
        {
            data.Add(MessagePackCode.Nil);
        }

        public unsafe void WriteRaw(NativeArray<byte> bytes)
        {
            data.AddRange(bytes.GetUnsafePtr(), bytes.Length);
        }
    }
}

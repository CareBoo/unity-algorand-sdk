using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackWriter
    {
        NativeList<byte> data;

        public MessagePackWriter(NativeList<byte> data)
        {
            this.data = data;
        }

        public void WriteNil()
        {
            data.Add(MessagePackCode.Nil);
        }

        public unsafe void WriteRaw(NativeArray<byte>.ReadOnly bytes)
        {
            data.AddRange(bytes.GetUnsafeReadOnlyPtr(), bytes.Length);
        }
    }
}

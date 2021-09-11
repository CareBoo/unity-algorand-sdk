using Unity.Collections;

namespace AlgoSdk.MessagePack
{
    public ref struct MessagePackReader
    {
        NativeArray<byte>.ReadOnly data;
        int offset;

        public MessagePackReader(NativeArray<byte>.ReadOnly data)
        {
            this.data = data;
            this.offset = 0;
        }
    }
}

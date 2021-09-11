using Unity.Collections;

namespace AlgoSdk.MessagePack
{
    public ref struct MessagePackWriter
    {
        NativeList<byte> data;

        public MessagePackWriter(NativeList<byte> data)
        {
            this.data = data;
        }
    }
}

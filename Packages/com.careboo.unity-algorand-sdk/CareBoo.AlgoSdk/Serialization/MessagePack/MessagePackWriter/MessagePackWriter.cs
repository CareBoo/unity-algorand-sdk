using Unity.Collections;

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
    }
}

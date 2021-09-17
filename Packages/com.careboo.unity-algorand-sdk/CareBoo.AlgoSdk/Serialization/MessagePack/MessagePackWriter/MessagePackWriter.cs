using System;
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

        public void WriteMapHeader(int length)
        {
            throw new NotImplementedException();
        }

        public void WriteString<T>(in T fs) where T : struct, INativeList<byte>, IUTF8Bytes
        {
            throw new NotImplementedException();
        }

        public void WriteNil()
        {
            data.Add(MessagePackCode.Nil);
        }
    }
}

using System;
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

        public void WriteMapHeader(int length)
        {
            throw new NotImplementedException();
        }

        public void WriteString<T>(T fs) where T : struct, INativeList<byte>, IUTF8Bytes
        {
            throw new NotImplementedException();
        }
    }
}

using System;
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

        public int ReadMapHeader()
        {
            throw new NotImplementedException();
        }

        public void ReadString<T>(ref T fs) where T : struct, INativeList<byte>, IUTF8Bytes
        {
            throw new NotImplementedException();
        }
    }
}

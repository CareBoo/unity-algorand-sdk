using System;

namespace AlgoSdk.MessagePack
{
    public partial struct MessagePackWriter
    {
        public void WriteBytesHeader(int length)
        {
            if (length <= byte.MaxValue)
            {
                data.Add(MessagePackCode.Bin8);
                data.Add((byte)length);
            }
            else if (length <= UInt16.MaxValue)
            {
                data.Add(MessagePackCode.Bin16);
                WriteBigEndian((ushort)length);
            }
            else
            {
                data.Add(MessagePackCode.Bin32);
                WriteBigEndian((uint)length);
            }
        }

        public unsafe void WriteBytes(void* buffer, int length)
        {
            WriteBytesHeader(length);
            data.AddRange(buffer, length);
        }

        public unsafe void WriteBytes(byte[] arr)
        {
            fixed (byte* buffer = &arr[0])
            {
                WriteBytes(buffer, arr.Length);
            }
        }
    }
}

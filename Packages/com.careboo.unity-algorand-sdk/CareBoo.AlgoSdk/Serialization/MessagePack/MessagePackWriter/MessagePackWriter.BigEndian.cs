using System;

namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackWriter
    {
        public void WriteBigEndian(short value) => WriteBigEndian(unchecked((ushort)value));

        public void WriteBigEndian(int value) => WriteBigEndian(unchecked((uint)value));

        public void WriteBigEndian(long value) => WriteBigEndian(unchecked((ulong)value));

        public void WriteBigEndian(ushort value)
        {
            unchecked
            {
                for (var i = 8; i >= 0; i -= 8)
                    data.Add((byte)(value >> i));
            }
        }

        public void WriteBigEndian(uint value)
        {
            unchecked
            {
                for (var i = 24; i >= 0; i -= 8)
                    data.Add((byte)(value >> i));
            }
        }

        public void WriteBigEndian(ulong value)
        {
            unchecked
            {
                for (var i = 56; i >= 0; i -= 8)
                    data.Add((byte)(value >> i));
            }
        }
    }
}

using Unity.Collections;

namespace Algorand.Unity.MessagePack
{
    public partial struct MessagePackWriter
    {
        public void WriteBigEndian(short value) => WriteBigEndian(unchecked((ushort)value));

        public void WriteBigEndian(int value) => WriteBigEndian(unchecked((uint)value));

        public void WriteBigEndian(long value) => WriteBigEndian(unchecked((ulong)value));

        public void WriteBigEndian(ushort value)
        {
            var offset = data.Length;
            data.Length += 2;
            value.CopyToNativeBytesBigEndian(ref data, offset);
        }

        public void WriteBigEndian(uint value)
        {
            var offset = data.Length;
            data.Length += 4;
            value.CopyToNativeBytesBigEndian(ref data, offset);
        }

        public void WriteBigEndian(ulong value)
        {
            var offset = data.Length;
            data.Length += 8;
            value.CopyToNativeBytesBigEndian(ref data, offset);
        }
    }
}

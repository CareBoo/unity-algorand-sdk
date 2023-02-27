using System;

namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public bool TryReadBigEndian(out short value)
        {
            if (!TryRead(out value))
                return false;
            if (BitConverter.IsLittleEndian)
                value = BinaryUtils.ReverseEndianness(value);
            return true;
        }

        public bool TryReadBigEndian(out ushort value)
        {
            if (!TryRead(out value))
                return false;
            if (BitConverter.IsLittleEndian)
                value = BinaryUtils.ReverseEndianness(value);
            return true;
        }

        public bool TryReadBigEndian(out int value)
        {
            if (!TryRead(out value))
                return false;
            if (BitConverter.IsLittleEndian)
                value = BinaryUtils.ReverseEndianness(value);
            return true;
        }

        public bool TryReadBigEndian(out uint value)
        {
            if (!TryRead(out value))
                return false;
            if (BitConverter.IsLittleEndian)
                value = BinaryUtils.ReverseEndianness(value);
            return true;
        }

        public bool TryReadBigEndian(out long value)
        {
            if (!TryRead(out value))
                return false;
            if (BitConverter.IsLittleEndian)
                value = BinaryUtils.ReverseEndianness(value);
            return true;
        }

        public bool TryReadBigEndian(out ulong value)
        {
            if (!TryRead(out value))
                return false;
            if (BitConverter.IsLittleEndian)
                value = BinaryUtils.ReverseEndianness(value);
            return true;
        }
    }
}

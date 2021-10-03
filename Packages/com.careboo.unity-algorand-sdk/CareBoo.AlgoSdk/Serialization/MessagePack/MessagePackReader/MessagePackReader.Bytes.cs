using Unity.Collections;

namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public NativeSlice<byte> ReadBytes()
        {
            if (TryReadBytes(out NativeSlice<byte> bytes))
                return bytes;
            throw InsufficientBuffer();
        }

        public bool TryReadBytes(out NativeSlice<byte> bytes)
        {
            bytes = default;
            var resetOffset = offset;
            if (!TryGetBytesLength(out int length))
                return false;

            if (offset + length >= data.Length)
            {
                offset = resetOffset;
                return false;
            }
            bytes = data.AsNativeSlice(offset, length);
            offset += length;
            return true;
        }

        bool TryGetBytesLength(out int length)
        {
            if (!TryRead(out byte code))
            {
                length = 0;
                return false;
            }

            switch (code)
            {
                case MessagePackCode.Bin8:
                    if (TryRead(out byte byteLength))
                    {
                        length = byteLength;
                        return true;
                    }

                    break;
                case MessagePackCode.Bin16:
                    if (TryReadBigEndian(out short shortLength))
                    {
                        length = unchecked((ushort)shortLength);
                        return true;
                    }

                    break;
                case MessagePackCode.Bin32:
                    return TryReadBigEndian(out length);
                default:
                    throw InvalidCode(code);
            }

            length = 0;
            return false;
        }

        bool TryReadRawNextBinary(NativeList<byte> bytes)
        {
            if (!TryPeek(out var code))
                return false;

            var resetOffset = offset;
            if (!TryGetBytesLength(out var length))
            {
                offset = resetOffset;
                return false;
            }
            bytes.Add(code);
            if (!TryAdvance(length, bytes))
            {
                offset = resetOffset;
                return false;
            }
            return true;
        }
    }
}

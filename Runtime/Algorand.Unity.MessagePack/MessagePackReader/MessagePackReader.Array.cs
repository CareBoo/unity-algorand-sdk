using Unity.Collections;

namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public int ReadArrayHeader()
        {
            if (TryReadArrayHeader(out int count)) return count;
            throw InsufficientBuffer();
        }

        public bool TryReadArrayHeader(out int count)
        {
            count = -1;
            if (!TryRead(out byte code)) return false;

            switch (code)
            {
                case MessagePackCode.Array16:
                    if (!TryReadBigEndian(out short shortValue))
                        return false;
                    count = unchecked((ushort)shortValue);
                    break;
                case MessagePackCode.Array32:
                    return TryReadBigEndian(out count);
                default:
                    if (code >= MessagePackCode.MinFixArray && code <= MessagePackCode.MaxFixArray)
                    {
                        count = (byte)(code & 0xF);
                        break;
                    }
                    throw InvalidCode(code);
            }
            return true;
        }

        private bool TryReadRawNextArray(NativeList<byte> bytes)
        {
            if (!TryPeek(out var code))
                return false;

            var resetOffset = offset;
            if (!TryReadArrayHeader(out var length))
            {
                offset = resetOffset;
                return false;
            }
            bytes.Add(code);
            for (var i = 0; i < length; i++)
            {
                if (!TryReadRaw(bytes))
                {
                    offset = resetOffset;
                    return false;
                }
            }
            return true;
        }

        private bool TrySkipNextArray()
        {
            return TryReadArrayHeader(out var count) && TrySkip(count);
        }
    }
}

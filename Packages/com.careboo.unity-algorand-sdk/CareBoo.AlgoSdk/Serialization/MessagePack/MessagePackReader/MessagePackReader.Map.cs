namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public int ReadMapHeader()
        {
            if (TryReadMapHeader(out int count)) return count;
            throw InsufficientBuffer();
        }

        public bool TryReadMapHeader(out int count)
        {
            count = -1;
            if (!TryRead(out byte code)) return false;

            switch (code)
            {
                case MessagePackCode.Map16:
                    if (!TryReadBigEndian(out short shortValue))
                        return false;
                    count = unchecked((ushort)shortValue);
                    break;
                case MessagePackCode.Map32:
                    return TryReadBigEndian(out count);
                default:
                    if (code >= MessagePackCode.MinFixMap && code <= MessagePackCode.MaxFixMap)
                    {
                        count = (byte)(code & 0xF);
                        break;
                    }
                    throw InvalidCode(code);
            }
            return true;
        }

        bool TrySkipNextMap()
        {
            return TryReadMapHeader(out var count) && TrySkip(count * 2);
        }
    }
}

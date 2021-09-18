using System;

namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public short ReadInt16()
        {
            throw new NotImplementedException();
        }

        public int ReadInt32()
        {
            throw new NotImplementedException();
        }

        public long ReadInt64()
        {
            throw new NotImplementedException();
        }

        public ushort ReadUInt16()
        {
            throw new NotImplementedException();
        }

        public uint ReadUInt32()
        {
            throw new NotImplementedException();
        }

        public ulong ReadUInt64()
        {
            if (!TryRead(out byte code))
                throw InsufficientBuffer();

            switch (code)
            {
                case MessagePackCode.UInt8:
                    if (TryRead(out byte byteResult))
                        return checked((ulong)byteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int8:
                    if (TryRead(out sbyte sbyteResult))
                        return checked((ulong)sbyteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt16:
                    if (TryReadBigEndian(out ushort ushortResult))
                        return checked((ulong)ushortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int16:
                    if (TryReadBigEndian(out short shortResult))
                        return checked((ulong)shortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt32:
                    if (TryReadBigEndian(out uint uintResult))
                        return checked((ulong)uintResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int32:
                    if (TryReadBigEndian(out int intResult))
                        return checked((ulong)intResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt64:
                    if (TryReadBigEndian(out ulong ulongResult))
                        return checked((ulong)ulongResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int64:
                    if (TryReadBigEndian(out long longResult))
                        return checked((ulong)longResult);
                    throw InsufficientBuffer();
                default:
                    if (code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt)
                    {
                        return checked((ulong)unchecked((sbyte)code));
                    }

                    if (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt)
                    {
                        return (ulong)code;
                    }

                    throw InvalidCode(code);
            }
        }
    }
}

using System;

namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public sbyte ReadInt8()
        {
            if (!TryRead(out byte code))
                throw InsufficientBuffer();

            switch (code)
            {
                case MessagePackCode.UInt8:
                    if (TryRead(out byte byteResult))
                        return checked((sbyte)byteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int8:
                    if (TryRead(out sbyte sbyteResult))
                        return checked((sbyte)sbyteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt16:
                    if (TryReadBigEndian(out ushort ushortResult))
                        return checked((sbyte)ushortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int16:
                    if (TryReadBigEndian(out short shortResult))
                        return checked((sbyte)shortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt32:
                    if (TryReadBigEndian(out uint uintResult))
                        return checked((sbyte)uintResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int32:
                    if (TryReadBigEndian(out int intResult))
                        return checked((sbyte)intResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt64:
                    if (TryReadBigEndian(out ulong ulongResult))
                        return checked((sbyte)ulongResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int64:
                    if (TryReadBigEndian(out long longResult))
                        return checked((sbyte)longResult);
                    throw InsufficientBuffer();
                default:
                    if (code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt)
                    {
                        return checked((sbyte)unchecked((sbyte)code));
                    }

                    if (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt)
                    {
                        return (sbyte)code;
                    }

                    throw InvalidCode(code);
            }
        }

        public short ReadInt16()
        {
            if (!TryRead(out byte code))
                throw InsufficientBuffer();

            switch (code)
            {
                case MessagePackCode.UInt8:
                    if (TryRead(out byte byteResult))
                        return checked((short)byteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int8:
                    if (TryRead(out sbyte sbyteResult))
                        return checked((short)sbyteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt16:
                    if (TryReadBigEndian(out ushort ushortResult))
                        return checked((short)ushortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int16:
                    if (TryReadBigEndian(out short shortResult))
                        return checked((short)shortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt32:
                    if (TryReadBigEndian(out uint uintResult))
                        return checked((short)uintResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int32:
                    if (TryReadBigEndian(out int intResult))
                        return checked((short)intResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt64:
                    if (TryReadBigEndian(out ulong ulongResult))
                        return checked((short)ulongResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int64:
                    if (TryReadBigEndian(out long longResult))
                        return checked((short)longResult);
                    throw InsufficientBuffer();
                default:
                    if (code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt)
                    {
                        return checked((short)unchecked((sbyte)code));
                    }

                    if (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt)
                    {
                        return (short)code;
                    }

                    throw InvalidCode(code);
            }
        }

        public int ReadInt32()
        {
            if (!TryRead(out byte code))
                throw InsufficientBuffer();

            switch (code)
            {
                case MessagePackCode.UInt8:
                    if (TryRead(out byte byteResult))
                        return checked((int)byteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int8:
                    if (TryRead(out sbyte sbyteResult))
                        return checked((int)sbyteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt16:
                    if (TryReadBigEndian(out ushort ushortResult))
                        return checked((int)ushortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int16:
                    if (TryReadBigEndian(out short shortResult))
                        return checked((int)shortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt32:
                    if (TryReadBigEndian(out uint uintResult))
                        return checked((int)uintResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int32:
                    if (TryReadBigEndian(out int intResult))
                        return checked((int)intResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt64:
                    if (TryReadBigEndian(out ulong ulongResult))
                        return checked((int)ulongResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int64:
                    if (TryReadBigEndian(out long longResult))
                        return checked((int)longResult);
                    throw InsufficientBuffer();
                default:
                    if (code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt)
                    {
                        return checked((int)unchecked((sbyte)code));
                    }

                    if (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt)
                    {
                        return (int)code;
                    }

                    throw InvalidCode(code);
            }
        }

        public long ReadInt64()
        {
            if (!TryRead(out byte code))
                throw InsufficientBuffer();

            switch (code)
            {
                case MessagePackCode.UInt8:
                    if (TryRead(out byte byteResult))
                        return checked((ushort)byteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int8:
                    if (TryRead(out sbyte sbyteResult))
                        return checked((ushort)sbyteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt16:
                    if (TryReadBigEndian(out ushort ushortResult))
                        return checked((ushort)ushortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int16:
                    if (TryReadBigEndian(out short shortResult))
                        return checked((ushort)shortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt32:
                    if (TryReadBigEndian(out uint uintResult))
                        return checked((ushort)uintResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int32:
                    if (TryReadBigEndian(out int intResult))
                        return checked((ushort)intResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt64:
                    if (TryReadBigEndian(out ulong ulongResult))
                        return checked((ushort)ulongResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int64:
                    if (TryReadBigEndian(out long longResult))
                        return checked((ushort)longResult);
                    throw InsufficientBuffer();
                default:
                    if (code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt)
                    {
                        return checked((ushort)unchecked((sbyte)code));
                    }

                    if (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt)
                    {
                        return (ushort)code;
                    }

                    throw InvalidCode(code);
            }
        }

        public byte ReadUInt8()
        {
            if (!TryRead(out byte code))
                throw InsufficientBuffer();

            switch (code)
            {
                case MessagePackCode.UInt8:
                    if (TryRead(out byte byteResult))
                        return checked((byte)byteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int8:
                    if (TryRead(out sbyte sbyteResult))
                        return checked((byte)sbyteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt16:
                    if (TryReadBigEndian(out ushort ushortResult))
                        return checked((byte)ushortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int16:
                    if (TryReadBigEndian(out short shortResult))
                        return checked((byte)shortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt32:
                    if (TryReadBigEndian(out uint uintResult))
                        return checked((byte)uintResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int32:
                    if (TryReadBigEndian(out int intResult))
                        return checked((byte)intResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt64:
                    if (TryReadBigEndian(out ulong ulongResult))
                        return checked((byte)ulongResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int64:
                    if (TryReadBigEndian(out long longResult))
                        return checked((byte)longResult);
                    throw InsufficientBuffer();
                default:
                    if (code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt)
                    {
                        return checked((byte)unchecked((sbyte)code));
                    }

                    if (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt)
                    {
                        return (byte)code;
                    }

                    throw InvalidCode(code);
            }
        }

        public ushort ReadUInt16()
        {
            if (!TryRead(out byte code))
                throw InsufficientBuffer();

            switch (code)
            {
                case MessagePackCode.UInt8:
                    if (TryRead(out byte byteResult))
                        return checked((ushort)byteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int8:
                    if (TryRead(out sbyte sbyteResult))
                        return checked((ushort)sbyteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt16:
                    if (TryReadBigEndian(out ushort ushortResult))
                        return checked((ushort)ushortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int16:
                    if (TryReadBigEndian(out short shortResult))
                        return checked((ushort)shortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt32:
                    if (TryReadBigEndian(out uint uintResult))
                        return checked((ushort)uintResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int32:
                    if (TryReadBigEndian(out int intResult))
                        return checked((ushort)intResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt64:
                    if (TryReadBigEndian(out ulong ulongResult))
                        return checked((ushort)ulongResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int64:
                    if (TryReadBigEndian(out long longResult))
                        return checked((ushort)longResult);
                    throw InsufficientBuffer();
                default:
                    if (code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt)
                    {
                        return checked((ushort)unchecked((sbyte)code));
                    }

                    if (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt)
                    {
                        return (ushort)code;
                    }

                    throw InvalidCode(code);
            }
        }

        public uint ReadUInt32()
        {
            if (!TryRead(out byte code))
                throw InsufficientBuffer();

            switch (code)
            {
                case MessagePackCode.UInt8:
                    if (TryRead(out byte byteResult))
                        return checked((uint)byteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int8:
                    if (TryRead(out sbyte sbyteResult))
                        return checked((uint)sbyteResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt16:
                    if (TryReadBigEndian(out ushort ushortResult))
                        return checked((uint)ushortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int16:
                    if (TryReadBigEndian(out short shortResult))
                        return checked((uint)shortResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt32:
                    if (TryReadBigEndian(out uint uintResult))
                        return checked((uint)uintResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int32:
                    if (TryReadBigEndian(out int intResult))
                        return checked((uint)intResult);
                    throw InsufficientBuffer();
                case MessagePackCode.UInt64:
                    if (TryReadBigEndian(out ulong ulongResult))
                        return checked((uint)ulongResult);
                    throw InsufficientBuffer();
                case MessagePackCode.Int64:
                    if (TryReadBigEndian(out long longResult))
                        return checked((uint)longResult);
                    throw InsufficientBuffer();
                default:
                    if (code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt)
                    {
                        return checked((uint)unchecked((sbyte)code));
                    }

                    if (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt)
                    {
                        return (uint)code;
                    }

                    throw InvalidCode(code);
            }
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

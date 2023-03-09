using Unity.Collections;

namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public void ReadString(out string s)
        {
            var text = new NativeText(Allocator.Temp);
            try
            {
                ReadString(ref text);
                s = text.ToString();
            }
            finally
            {
                text.Dispose();
            }
        }

        public void ReadString<T>(ref T fs)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            if (!TryReadString(ref fs))
                throw InsufficientBuffer();
        }

        public bool TryReadString<T>(ref T fs)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            fs.Length = 0;
            if (TryReadNil())
                return true;

            var resetOffset = offset;
            if (!TryGetStringLengthInBytes(out var length))
                return false;

            for (var i = 0; i < length; i++)
            {
                if (!TryRead(out byte b))
                {
                    offset = resetOffset;
                    return false;
                }
                var error = fs.AppendRawByte(b);
                if (error != FormatError.None)
                {
                    offset = resetOffset;
                    return false;
                }
            }
            return true;
        }

        public void ReadString(ref NativeText fs)
        {
            if (!TryReadString(ref fs))
                throw InsufficientBuffer();
        }

        public bool TryReadString(ref NativeText fs)
        {
            fs.Length = 0;
            if (TryReadNil())
                return true;

            var resetOffset = offset;
            if (!TryGetStringLengthInBytes(out var length))
                return false;

            for (var i = 0; i < length; i++)
            {
                if (!TryRead(out byte b))
                {
                    offset = resetOffset;
                    return false;
                }
                var error = fs.AppendRawByte(b);
                if (error != FormatError.None)
                {
                    offset = resetOffset;
                    return false;
                }
            }
            return true;
        }

        private bool TryGetStringLengthInBytes(out int length)
        {
            if (!TryRead(out byte code))
            {
                length = default;
                return false;
            }

            if (code >= MessagePackCode.MinFixStr && code <= MessagePackCode.MaxFixStr)
            {
                length = code & 0x1F;
                return true;
            }

            return TryGetStringLengthInBytes(code, out length);
        }

        private bool TryGetStringLengthInBytes(byte code, out int length)
        {
            length = default;
            switch (code)
            {
                case MessagePackCode.Str8:
                    if (TryRead(out byte byteValue))
                    {
                        length = byteValue;
                        return true;
                    }

                    break;
                case MessagePackCode.Str16:
                    if (TryReadBigEndian(out short shortValue))
                    {
                        length = unchecked((ushort)shortValue);
                        return true;
                    }

                    break;
                case MessagePackCode.Str32:
                    if (TryReadBigEndian(out int intValue))
                    {
                        length = intValue;
                        return true;
                    }

                    break;
                default:
                    if (code >= MessagePackCode.MinFixStr && code <= MessagePackCode.MaxFixStr)
                    {
                        length = code & 0x1F;
                        return true;
                    }

                    throw InvalidCode(code);
            }
            return false;
        }

        private bool TryReadRawNextString(NativeList<byte> bytes)
        {
            if (!TryPeek(out var code))
                return false;

            var resetOffset = offset;
            if (!TryGetStringLengthInBytes(out var length))
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

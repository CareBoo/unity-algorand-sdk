using Unity.Collections;

namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public bool TryReadExtensionFormatHeader(out ExtensionHeader header)
        {
            header = default;
            if (!TryRead(out byte code))
                return false;

            uint length;
            switch (code)
            {
                case MessagePackCode.FixExt1:
                    length = 1;
                    break;
                case MessagePackCode.FixExt2:
                    length = 2;
                    break;
                case MessagePackCode.FixExt4:
                    length = 4;
                    break;
                case MessagePackCode.FixExt8:
                    length = 8;
                    break;
                case MessagePackCode.FixExt16:
                    length = 16;
                    break;
                case MessagePackCode.Ext8:
                    if (!TryRead(out byte byteLength))
                    {
                        return false;
                    }

                    length = byteLength;
                    break;
                case MessagePackCode.Ext16:
                    if (!TryReadBigEndian(out short shortLength))
                    {
                        return false;
                    }

                    length = unchecked((ushort)shortLength);
                    break;
                case MessagePackCode.Ext32:
                    if (!TryReadBigEndian(out int intLength))
                    {
                        return false;
                    }

                    length = unchecked((uint)intLength);
                    break;
                default:
                    throw InvalidCode(code);
            }

            if (!TryRead(out byte typeCode))
                return false;

            header = new ExtensionHeader(unchecked((sbyte)typeCode), length);
            return true;
        }

        private bool TryReadRawNextExtension(NativeList<byte> bytes)
        {
            if (!TryPeek(out byte code))
                return false;

            var resetOffset = offset;
            if (!TryReadExtensionFormatHeader(out var header))
            {
                offset = resetOffset;
                return false;
            }
            bytes.Add(code);
            bytes.Add((byte)header.TypeCode);
            if (!TryAdvance((int)header.Length, bytes))
            {
                offset = resetOffset;
                return false;
            }

            return true;
        }
    }
}

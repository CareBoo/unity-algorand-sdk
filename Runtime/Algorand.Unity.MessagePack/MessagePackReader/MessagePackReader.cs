using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        private NativeArray<byte> data;
        private int offset;

        public MessagePackReader(NativeArray<byte> data)
        {
            this.data = data;
            this.offset = 0;
        }

        public int Position
        {
            get => offset;
            set => offset = value;
        }

        public byte Peek()
        {
            if (TryPeek(out byte code))
                return code;
            throw InsufficientBuffer();
        }

        public byte ReadByte()
        {
            if (TryRead(out byte code))
                return code;
            throw InsufficientBuffer();
        }

        public sbyte ReadSByte()
        {
            if (TryRead(out sbyte code))
                return code;
            throw InsufficientBuffer();
        }

        public void Skip()
        {
            if (!TrySkip())
                throw InsufficientBuffer();
        }

        public void ReadRaw(NativeList<byte> bytes)
        {
            if (!TryReadRaw(bytes))
                throw InsufficientBuffer();
        }

        private bool TryRead<T>(out T value) where T : struct
        {
            value = default;
            var size = UnsafeUtility.SizeOf<T>();
            var next = offset + size;
            if (next - 1 >= data.Length) return false;
            unsafe
            {
                value = UnsafeUtility.ReadArrayElementWithStride<T>(data.GetUnsafeReadOnlyPtr(), offset, 1);
            }
            offset = next;
            return true;
        }

        public bool TryRead(out byte code)
        {
            code = default;
            var reset = offset;
            if (offset >= data.Length)
                return false;
            code = data[offset++];
            return true;
        }

        public bool TryRead(out sbyte value)
        {
            if (TryRead(out byte byteVal))
            {
                value = unchecked((sbyte)byteVal);
                return true;
            }
            value = default;
            return false;
        }

        public bool TryPeek(out byte code)
        {
            code = default;
            if (offset >= data.Length)
                return false;
            code = data[offset];
            return true;
        }

        public bool TryReadRaw(NativeList<byte> bytes)
        {
            if (!TryPeek(out byte code)) return false;

            switch (code)
            {
                case MessagePackCode.Nil:
                case MessagePackCode.True:
                case MessagePackCode.False:
                    return TryAdvance(1, bytes);
                case MessagePackCode.Int8:
                case MessagePackCode.UInt8:
                    return TryAdvance(2, bytes);
                case MessagePackCode.Int16:
                case MessagePackCode.UInt16:
                    return TryAdvance(3, bytes);
                case MessagePackCode.Int32:
                case MessagePackCode.UInt32:
                case MessagePackCode.Float32:
                    return TryAdvance(5, bytes);
                case MessagePackCode.Int64:
                case MessagePackCode.UInt64:
                case MessagePackCode.Float64:
                    return TryAdvance(9, bytes);
                case MessagePackCode.Map16:
                case MessagePackCode.Map32:
                    return TryReadRawNextMap(bytes);
                case MessagePackCode.Array16:
                case MessagePackCode.Array32:
                    return TryReadRawNextArray(bytes);
                case MessagePackCode.Str8:
                case MessagePackCode.Str16:
                case MessagePackCode.Str32:
                    return TryReadRawNextString(bytes);
                case MessagePackCode.Bin8:
                case MessagePackCode.Bin16:
                case MessagePackCode.Bin32:
                    return TryReadRawNextBinary(bytes);
                case MessagePackCode.FixExt1:
                case MessagePackCode.FixExt2:
                case MessagePackCode.FixExt4:
                case MessagePackCode.FixExt8:
                case MessagePackCode.FixExt16:
                case MessagePackCode.Ext8:
                case MessagePackCode.Ext16:
                case MessagePackCode.Ext32:
                    return TryReadRawNextExtension(bytes);
                default:
                    if ((code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt) ||
                        (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt))
                    {
                        return TryAdvance(1, bytes);
                    }

                    if (code >= MessagePackCode.MinFixMap && code <= MessagePackCode.MaxFixMap)
                    {
                        return TryReadRawNextMap(bytes);
                    }

                    if (code >= MessagePackCode.MinFixArray && code <= MessagePackCode.MaxFixArray)
                    {
                        return TryReadRawNextArray(bytes);
                    }

                    if (code >= MessagePackCode.MinFixStr && code <= MessagePackCode.MaxFixStr)
                    {
                        return TryReadRawNextString(bytes);
                    }
                    throw new NotSupportedException($"msgpack code not supported: {code}");
            }
        }

        public bool TrySkip()
        {
            if (!TryPeek(out byte code)) return false;

            switch (code)
            {
                case MessagePackCode.Nil:
                case MessagePackCode.True:
                case MessagePackCode.False:
                    return TryAdvance(1);
                case MessagePackCode.Int8:
                case MessagePackCode.UInt8:
                    return TryAdvance(2);
                case MessagePackCode.Int16:
                case MessagePackCode.UInt16:
                    return TryAdvance(3);
                case MessagePackCode.Int32:
                case MessagePackCode.UInt32:
                case MessagePackCode.Float32:
                    return TryAdvance(5);
                case MessagePackCode.Int64:
                case MessagePackCode.UInt64:
                case MessagePackCode.Float64:
                    return TryAdvance(9);
                case MessagePackCode.Map16:
                case MessagePackCode.Map32:
                    return TrySkipNextMap();
                case MessagePackCode.Array16:
                case MessagePackCode.Array32:
                    return TrySkipNextArray();
                case MessagePackCode.Str8:
                case MessagePackCode.Str16:
                case MessagePackCode.Str32:
                    return TryGetStringLengthInBytes(out int length) && TryAdvance(length);
                case MessagePackCode.Bin8:
                case MessagePackCode.Bin16:
                case MessagePackCode.Bin32:
                    return TryGetBytesLength(out length) && TryAdvance(length);
                case MessagePackCode.FixExt1:
                case MessagePackCode.FixExt2:
                case MessagePackCode.FixExt4:
                case MessagePackCode.FixExt8:
                case MessagePackCode.FixExt16:
                case MessagePackCode.Ext8:
                case MessagePackCode.Ext16:
                case MessagePackCode.Ext32:
                    return TryReadExtensionFormatHeader(out ExtensionHeader header) && TryAdvance((int)header.Length);
                default:
                    if ((code >= MessagePackCode.MinNegativeFixInt && code <= MessagePackCode.MaxNegativeFixInt) ||
                        (code >= MessagePackCode.MinFixInt && code <= MessagePackCode.MaxFixInt))
                    {
                        return TryAdvance(1);
                    }

                    if (code >= MessagePackCode.MinFixMap && code <= MessagePackCode.MaxFixMap)
                    {
                        return TrySkipNextMap();
                    }

                    if (code >= MessagePackCode.MinFixArray && code <= MessagePackCode.MaxFixArray)
                    {
                        return TrySkipNextArray();
                    }

                    if (code >= MessagePackCode.MinFixStr && code <= MessagePackCode.MaxFixStr)
                    {
                        return TryGetStringLengthInBytes(out length) && TryAdvance(length);
                    }
                    throw new NotSupportedException($"msgpack code not supported: {code}");
            }
        }

        private bool TrySkip(int count)
        {
            for (var i = 0; i < count; i++)
                if (!TrySkip())
                    return false;
            return true;
        }

        private bool TryAdvance(int x)
        {
            var result = offset + x;
            if (result > data.Length) return false;
            offset = result;
            return true;
        }

        private bool TryAdvance(int x, NativeList<byte> buffer)
        {
            if (!TryAdvance(x)) return false;
            unsafe
            {
                buffer.AddRange(GetCurrentUnsafePtr() - x, x);
            }
            return true;
        }

        private unsafe byte* GetCurrentUnsafePtr()
        {
            return (byte*)data.GetUnsafeReadOnlyPtr() + offset;
        }
    }
}

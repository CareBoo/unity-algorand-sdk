using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackReader
    {
        NativeArray<byte>.ReadOnly data;
        int offset;

        public MessagePackReader(NativeArray<byte>.ReadOnly data)
        {
            this.data = data;
            this.offset = 0;
        }

        public byte Peek()
        {
            return data[offset];
        }

        public byte Read()
        {
            return data[offset++];
        }

        bool TryRead<T>(out T value) where T : struct
        {
            value = default;
            var size = UnsafeUtility.SizeOf<T>();
            var next = offset + size;
            if (next >= data.Length) return false;
            unsafe
            {
                value = UnsafeUtility.ReadArrayElement<T>(data.GetUnsafeReadOnlyPtr(), offset);
            }
            offset = next;
            return true;
        }

        public bool TryRead(out byte code)
        {
            code = default;
            var next = offset + 1;
            if (next >= data.Length)
                return false;
            code = data[next];
            offset = next;
            return true;
        }

        public bool TrySkip()
        {
            if (offset >= data.Length) return false;

            byte code = Peek();
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
                        return this.TryGetStringLengthInBytes(out length) && TryAdvance(length);
                    }
                    throw new NotSupportedException($"msgpack code not supported: {code}");
            }
        }

        bool TrySkip(int count)
        {
            for (var i = 0; i < count; i++)
                if (!TrySkip())
                    return false;
            return true;
        }

        bool TryAdvance(int x)
        {
            var result = offset + x;
            if (result >= data.Length) return false;
            offset = result;
            return true;
        }
    }
}

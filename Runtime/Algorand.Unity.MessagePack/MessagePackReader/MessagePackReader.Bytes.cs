using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public NativeSlice<byte> ReadBytes()
        {
            if (TryReadBytes(out NativeSlice<byte> bytes))
                return bytes;
            throw InsufficientBuffer();
        }

        public T ReadBytes<T>()
            where T : unmanaged, IByteArray
        {
            T result = default;
            unsafe
            {
                UnsafeReadBytes(result.GetUnsafePtr(), result.Length);
            }
            return result;
        }

        public unsafe void UnsafeReadBytes(void* ptr, int length)
        {
            var resetOffset = offset;
            if (!TryGetBytesLength(out int msgLength))
                throw InsufficientBuffer();
            if (msgLength > length)
            {
                offset = resetOffset;
                throw InsufficientBuffer();
            }
            UnsafeUtility.MemCpy(ptr, GetCurrentUnsafePtr(), msgLength);
            offset += msgLength;
        }

        public bool TryReadBytes(out NativeSlice<byte> bytes)
        {
            bytes = default;
            var resetOffset = offset;
            if (!TryGetBytesLength(out int length))
            {
                offset = resetOffset;
                return false;
            }

            if (offset + length >= data.Length)
            {
                offset = resetOffset;
                return false;
            }
            bytes = new NativeSlice<byte>(data, offset, length);
            offset += length;
            return true;
        }

        private bool TryGetBytesLength(out int length)
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
            }

            length = 0;
            return false;
        }

        private bool TryReadRawNextBinary(NativeList<byte> bytes)
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

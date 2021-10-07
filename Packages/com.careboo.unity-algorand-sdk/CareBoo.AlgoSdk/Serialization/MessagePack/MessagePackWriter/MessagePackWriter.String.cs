using Unity.Collections;

namespace AlgoSdk.MessagePack
{
    public partial struct MessagePackWriter
    {
        public unsafe void WriteString<T>(in T fs)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            var chars = fs.GetUnsafePtr();
            var byteCount = fs.Length;
            if (byteCount <= MessagePackRange.MaxFixStringLength)
            {
                data.Add((byte)(MessagePackCode.MinFixStr | byteCount));
                data.AddRange(chars, byteCount);
            }
            else if (byteCount <= byte.MaxValue)
            {
                data.Add(MessagePackCode.Str8);
                data.Add(unchecked((byte)byteCount));
                data.AddRange(chars, byteCount);
            }
            else if (byteCount <= ushort.MaxValue)
            {
                data.Add(MessagePackCode.Str16);
                WriteBigEndian((ushort)byteCount);
                data.AddRange(chars, byteCount);
            }
            else
            {
                data.Add(MessagePackCode.Str32);
                WriteBigEndian((ushort)byteCount);
                data.AddRange(chars, byteCount);
            }
        }
    }
}

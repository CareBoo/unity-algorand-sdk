using Unity.Collections;

namespace Algorand.Unity.MessagePack
{
    public partial struct MessagePackWriter
    {
        public void WriteStringHeader(int length)
        {
            if (length <= MessagePackRange.MaxFixStringLength)
            {
                data.Add((byte)(MessagePackCode.MinFixStr | length));
            }
            else if (length <= byte.MaxValue)
            {
                data.Add(MessagePackCode.Str8);
                data.Add(unchecked((byte)length));
            }
            else if (length <= ushort.MaxValue)
            {
                data.Add(MessagePackCode.Str16);
                WriteBigEndian((ushort)length);
            }
            else
            {
                data.Add(MessagePackCode.Str32);
                WriteBigEndian((uint)length);
            }
        }

        public void WriteString(string s)
        {
            using var text = new NativeText(s, Allocator.Temp);
            WriteString(text);
        }

        public unsafe void WriteString<T>(T fs)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            var ptr = fs.GetUnsafePtr();
            var length = fs.Length;
            WriteStringHeader(length);
            data.AddRange(ptr, length);
        }
    }
}

namespace Algorand.Unity.MessagePack
{
    public partial struct MessagePackWriter
    {
        public void WriteArrayHeader(int count) => WriteArrayHeader((uint)count);

        public void WriteArrayHeader(uint count)
        {
            if (count <= MessagePackRange.MaxFixArrayCount)
            {
                data.Add((byte)(MessagePackCode.MinFixArray | count));
            }
            else if (count <= ushort.MaxValue)
            {
                data.Add(MessagePackCode.Array16);
                WriteBigEndian((ushort)count);
            }
            else
            {
                data.Add(MessagePackCode.Array32);
                WriteBigEndian(count);
            }
        }
    }
}

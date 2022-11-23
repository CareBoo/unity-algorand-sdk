namespace Algorand.Unity.MessagePack
{
    public partial struct MessagePackWriter
    {
        public void WriteMapHeader(int length) => WriteMapHeader((uint)length);

        public void WriteMapHeader(uint length)
        {
            if (length <= MessagePackRange.MaxFixMapCount)
            {
                data.Add((byte)(MessagePackCode.MinFixMap | length));
            }
            else if (length <= ushort.MaxValue)
            {
                data.Add(MessagePackCode.Map16);
                WriteBigEndian((ushort)length);
            }
            else
            {
                data.Add(MessagePackCode.Map32);
                WriteBigEndian(length);
            }
        }
    }
}

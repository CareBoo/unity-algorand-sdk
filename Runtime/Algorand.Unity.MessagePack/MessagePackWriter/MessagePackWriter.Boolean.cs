namespace Algorand.Unity.MessagePack
{
    public partial struct MessagePackWriter
    {
        public void Write(bool value)
        {
            data.Add(value ? MessagePackCode.True : MessagePackCode.False);
        }
    }
}

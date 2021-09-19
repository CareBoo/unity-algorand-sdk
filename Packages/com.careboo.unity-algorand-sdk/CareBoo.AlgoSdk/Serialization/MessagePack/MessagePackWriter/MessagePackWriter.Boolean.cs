namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackWriter
    {
        public void Write(bool value)
        {
            data.Add(value ? MessagePackCode.True : MessagePackCode.False);
        }
    }
}

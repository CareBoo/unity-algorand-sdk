namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public bool ReadBool()
        {
            var code = ReadByte();
            return code switch
            {
                MessagePackCode.True => true,
                MessagePackCode.False => false,
                _ => throw InvalidCode(code)
            };
        }
    }
}

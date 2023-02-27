namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        public bool TryReadNil()
        {
            if (Peek() != MessagePackCode.Nil)
                return false;
            offset++;
            return true;
        }
    }
}

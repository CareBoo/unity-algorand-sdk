namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(AlgoApiObject))]
    public struct AlgoApiObject
    {
        public byte[] MessagePack;
        public string Json;
        public bool IsMessagePack => MessagePack != null;
        public bool IsJson => Json != null;
    }
}

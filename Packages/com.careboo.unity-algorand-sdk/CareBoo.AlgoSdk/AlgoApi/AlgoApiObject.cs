using System;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(AlgoApiObjectFormatter))]
    public struct AlgoApiObject
        : IEquatable<AlgoApiObject>
    {
        public byte[] MessagePack;
        public string Json;
        public bool IsMessagePack => MessagePack != null;
        public bool IsJson => Json != null;

        public bool Equals(AlgoApiObject other)
        {
            return ArrayComparer.Equals(MessagePack, other.MessagePack)
                && StringComparer.Equals(Json, other.Json)
                ;
        }
    }
}

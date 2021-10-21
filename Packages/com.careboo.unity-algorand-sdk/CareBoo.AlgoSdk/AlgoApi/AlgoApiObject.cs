using System;

namespace AlgoSdk
{
    /// <summary>
    /// A non-deserialized object from an algorand service.
    /// </summary>
    [AlgoApiFormatter(typeof(AlgoApiObjectFormatter))]
    public struct AlgoApiObject
        : IEquatable<AlgoApiObject>
    {
        /// <summary>
        /// Raw message pack if the response had <see cref="ContentType.MessagePack"/>
        /// </summary>
        public byte[] MessagePack;

        /// <summary>
        /// Raw json if the response had <see cref="ContentType.Json"/>
        /// </summary>
        public string Json;

        /// <summary>
        /// <see cref="true"/> if this is a messagepack object.
        /// </summary>
        public bool IsMessagePack => MessagePack != null;

        /// <summary>
        /// <see cref="true"/> if this is a json object.
        /// </summary>
        public bool IsJson => Json != null;

        public bool Equals(AlgoApiObject other)
        {
            return ArrayComparer.Equals(MessagePack, other.MessagePack)
                && StringComparer.Equals(Json, other.Json)
                ;
        }
    }
}

using System;
using System.Text;
using Algorand.Unity.Collections;
using Unity.Collections;

namespace Algorand.Unity
{
    /// <summary>
    /// A non-deserialized object from an algorand service.
    /// </summary>
    [AlgoApiFormatter(typeof(AlgoApiObjectFormatter))]
    public partial struct AlgoApiObject
        : IEquatable<AlgoApiObject>
    {
        /// <summary>
        /// Raw message pack if the response had <see cref="ContentType.MessagePack"/>
        /// </summary>
        public byte[] MessagePack => IsMessagePack ? data : null;

        /// <summary>
        /// Raw json if the response had <see cref="ContentType.Json"/> encoded as UTF8.
        /// </summary>
        public byte[] Json => IsJson ? data : null;

        /// <summary>
        /// <see cref="true"/> if this is a messagepack object.
        /// </summary>
        public bool IsMessagePack => contentType == ContentType.MessagePack;

        /// <summary>
        /// <see cref="true"/> if this is a json object.
        /// </summary>
        public bool IsJson => contentType == ContentType.Json;

        private readonly byte[] data;

        private readonly ContentType contentType;

        public AlgoApiObject(byte[] data, ContentType contentType)
        {
            this.data = data;
            this.contentType = contentType;
        }

        public bool Equals(AlgoApiObject other)
        {
            return ArrayComparer.Equals(data, other.data)
                && contentType.Equals(other.contentType)
                ;
        }

        public static implicit operator AlgoApiObject(string json)
        {
            return new AlgoApiObject(Encoding.UTF8.GetBytes(json), ContentType.Json);
        }

        public static implicit operator AlgoApiObject(NativeText json)
        {
            return new AlgoApiObject(json.AsArray().ToArray(), ContentType.Json);
        }

        public static implicit operator AlgoApiObject(byte[] msgpack)
        {
            return new AlgoApiObject(msgpack, ContentType.MessagePack);
        }

        public static implicit operator AlgoApiObject(NativeArray<byte> msgpack)
        {
            return new AlgoApiObject(msgpack.ToArray(), ContentType.MessagePack);
        }

        public static implicit operator AlgoApiObject(NativeList<byte> msgpack)
        {
            return new AlgoApiObject(msgpack.AsArray().ToArray(), ContentType.MessagePack);
        }
    }
}

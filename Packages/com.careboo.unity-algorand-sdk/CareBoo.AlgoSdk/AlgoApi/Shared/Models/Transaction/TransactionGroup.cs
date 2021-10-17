using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionGroup
        : IEquatable<TransactionGroup>
    {
        public const int MaxSize = 16;

        public static readonly byte[] IdPrefix = Encoding.UTF8.GetBytes("TG");

        [AlgoApiField("txlist", "txlist")]
        public Sha512_256_Hash[] Txns;


        public bool Equals(TransactionGroup other)
        {
            return ArrayComparer.Equals(Txns, other.Txns);
        }

        public Sha512_256_Hash GetId()
        {
            using var msgpack = AlgoApiSerializer.SerializeMessagePack(this, Allocator.Temp);
            var data = new NativeByteArray(IdPrefix.Length + msgpack.Length, Allocator.Temp);
            try
            {
                data.CopyFrom(IdPrefix, 0);
                data.CopyFrom(msgpack.AsArray(), IdPrefix.Length);
                return Sha512.Hash256Truncated(data);
            }
            finally
            {
                data.Dispose();
            }
        }
    }
}

using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// A group of transactions used to generate a group id for atomic transactions.
    /// </summary>
    [AlgoApiObject]
    public struct TransactionGroup
        : IEquatable<TransactionGroup>
    {
        /// <summary>
        /// Max number of allowed transactions in an atomic transaction.
        /// </summary>
        public const int MaxSize = 16;

        /// <summary>
        /// The prefix to use when converting this group of transactions to bytes.
        /// </summary>
        public static readonly byte[] IdPrefix = Encoding.UTF8.GetBytes("TG");

        /// <summary>
        /// The list of transaction ids belonging to this group.
        /// </summary>
        [AlgoApiField("txlist", "txlist")]
        public TransactionId[] Txns;


        public bool Equals(TransactionGroup other)
        {
            return ArrayComparer.Equals(Txns, other.Txns);
        }

        /// <summary>
        /// Hash the transaction ids contained in this group.
        /// </summary>
        /// <returns>A <see cref="TransactionId"/></returns>
        public TransactionId GetId()
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

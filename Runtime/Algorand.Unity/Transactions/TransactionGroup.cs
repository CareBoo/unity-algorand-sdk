using System;
using System.Text;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// A group of transactions used to generate a group id for atomic transactions.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    [Obsolete("Use Transaction.Atomic instead to build and sign transaction groups.")]
    public partial struct TransactionGroup
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
        /// Generate a TransactionGroup with the given transactions that can be used to generate a GroupId.
        /// </summary>
        /// <param name="txns">The transactions to use.</param>
        /// <typeparam name="T">The type of the transactions.</typeparam>
        public static TransactionGroup Of<T>(params T[] txns) where T : ITransaction, IEquatable<T>
        {
            if (txns == null || txns.Length == 0)
                throw new ArgumentException("Cannot get the group id of 0 transactions", nameof(txns));
            if (txns.Length > TransactionGroup.MaxSize)
                throw new ArgumentException($"Cannot get the group id of a group of more than {TransactionGroup.MaxSize} transactions", nameof(txns));

            var txnIds = new TransactionId[txns.Length];
            for (var i = 0; i < txns.Length; i++)
            {
                txns[i].Group = default;
                txnIds[i] = txns[i].GetId();
            }
            return new TransactionGroup { Txns = txnIds };
        }

        /// <summary>
        /// Get a transaction group using the transaction ids given.
        /// </summary>
        /// <param name="txns">The transaction ids of the transactions in the group.</param>
        /// <returns>A transaction group that can be used to generate a group id.</returns>
        public static TransactionGroup Of(params TransactionId[] txns)
        {
            if (txns == null || txns.Length == 0)
                throw new ArgumentException("Cannot get the group id of 0 transactions", nameof(txns));
            if (txns.Length > TransactionGroup.MaxSize)
                throw new ArgumentException($"Cannot get the group id of a group of more than {TransactionGroup.MaxSize} transactions", nameof(txns));

            return new TransactionGroup { Txns = txns };
        }

        /// <summary>
        /// The list of transaction ids belonging to this group.
        /// </summary>
        [AlgoApiField("txlist")]
        [Tooltip("The list of transaction ids belonging to this group.")]
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

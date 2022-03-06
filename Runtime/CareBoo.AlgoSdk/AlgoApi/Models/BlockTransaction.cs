using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// A transaction found in a <see cref="BlockResponse"/> from <see cref="IAlgodClient.GetBlock"/>.
    /// </summary>
    [AlgoApiObject]
    public partial struct BlockTransaction
        : IEquatable<BlockTransaction>
    {
        /// <summary>
        /// The transaction.
        /// </summary>
        [AlgoApiField("txn", "txn")]
        [Tooltip("The transaction")]
        public Transaction Transaction;

        /// <summary>
        /// A crypto sig if the transaction was signed by one.
        /// </summary>
        [AlgoApiField("sig", "sig")]
        public Sig Sig
        {
            get => Transaction.Signature.Sig;
            set => Transaction.Signature.Sig = value;
        }

        /// <summary>
        /// A multisig if the transaction was signed by one.
        /// </summary>
        [AlgoApiField("msig", "msig")]
        public Multisig Multisig
        {
            get => Transaction.Signature.Multisig;
            set => Transaction.Signature.Multisig = value;
        }

        /// <summary>
        /// A logic sig if the transaction was signed by one.
        /// </summary>
        [AlgoApiField("lsig", "lsig")]
        public LogicSig LogicSig
        {
            get => Transaction.Signature.LogicSig;
            set => Transaction.Signature.LogicSig = value;
        }

        [AlgoApiField("hgi", "hgi")]
        public Optional<bool> Hgi;

        [AlgoApiField("rr", "rr")]
        public ulong Rr;

        [AlgoApiField("rs", "rs")]
        public ulong Rs;

        public bool Equals(BlockTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }

        public static implicit operator Transaction(BlockTransaction blockTxn)
        {
            return blockTxn.Transaction;
        }
    }
}

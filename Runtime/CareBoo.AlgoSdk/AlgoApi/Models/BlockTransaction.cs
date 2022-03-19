using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// A transaction found in a <see cref="BlockResponse"/> from <see cref="IAlgodClient.GetBlock"/>.
    /// </summary>
    /// <remarks>
    /// This is a `SignedTxnInBlock` from the algorand go project.
    /// </remarks>
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

        /// <summary>
        /// 
        /// </summary>
        [AlgoApiField("hgi", "hgi")]
        public Optional<bool> Hgi;

        /// <summary>
        /// Receiver Rewards
        /// </summary>
        [AlgoApiField("rr", "rr")]
        public ulong ReceiverRewards;

        /// <summary>
        /// Sender rewards
        /// </summary>
        [AlgoApiField("rs", "rs")]
        public ulong SenderRewards;

        /// <summary>
        /// Close Rewards
        /// </summary>
        [AlgoApiField("rc", "rc")]
        public ulong CloseRewards;

        [AlgoApiField("dt", "dt")]
        public EvalDelta EvalDelta;

        public bool Equals(BlockTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }

        public static implicit operator Transaction(BlockTransaction blockTxn)
        {
            return blockTxn.Transaction;
        }

        [Obsolete("Replaced with SenderRewards")]
        public ulong Rs { get => SenderRewards; set => SenderRewards = value; }

        [Obsolete("Replaced with ReceiverRewards")]
        public ulong Rr { get => ReceiverRewards; set => ReceiverRewards = value; }
    }
}

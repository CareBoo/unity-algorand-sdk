using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    public interface IPaymentTxn : ITransaction
    {
        /// <summary>
        /// The address of the account that receives the <see cref="Amount"/>.
        /// </summary>
        Address Receiver { get; set; }

        /// <summary>
        /// The total amount to be sent in microAlgos.
        /// </summary>
        MicroAlgos Amount { get; set; }

        /// <summary>
        /// When set, it indicates that the transaction is requesting that the <see cref="ITransaction.Sender"/> account should be closed, and all remaining funds, after the <see cref="ITransaction.Fee"/> and <see cref="Amount"/> are paid, be transferred to this address.
        /// </summary>
        Address CloseRemainderTo { get; set; }
    }

    public partial struct Transaction : IPaymentTxn
    {
        [AlgoApiField(null, "rcv")]
        public Address Receiver
        {
            get => paymentParams.Receiver;
            set => paymentParams.Receiver = value;
        }

        [AlgoApiField(null, "amt")]
        public MicroAlgos Amount
        {
            get => paymentParams.Amount;
            set => paymentParams.Amount = value;
        }

        [AlgoApiField(null, "close")]
        public Address CloseRemainderTo
        {
            get => paymentParams.CloseRemainderTo;
            set => paymentParams.CloseRemainderTo = value;
        }

        /// <summary>
        /// Create a <see cref="PaymentTxn"/>
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="receiver">The address of the account that receives the amount.</param>
        /// <param name="amount">The total amount to be sent in microAlgos.</param>
        /// <param name="closeRemainderTo">When set, it indicates that the transaction is requesting that the Sender account should be closed, and all remaining funds, after the fee and amount are paid, be transferred to this address.</param>
        /// <returns>A <see cref="PaymentTxn"/></returns>
        public static PaymentTxn Payment(
            Address sender,
            TransactionParams txnParams,
            Address receiver,
            MicroAlgos amount,
            Address closeRemainderTo = default
        )
        {
            var txn = new PaymentTxn
            {
                header = new TransactionHeader(sender, TransactionType.Payment, txnParams),
                Receiver = receiver,
                Amount = amount,
                CloseRemainderTo = closeRemainderTo
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }
    }

    [AlgoApiObject]
    [Serializable]
    public partial struct PaymentTxn
        : IPaymentTxn
        , IEquatable<PaymentTxn>
    {
        [SerializeField]
        internal TransactionHeader header;

        [SerializeField]
        Params @params;

        [AlgoApiField("fee", "fee")]
        public MicroAlgos Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        [AlgoApiField("first-valid", "fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        [AlgoApiField("last-valid", "lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        [AlgoApiField("sender", "snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        [AlgoApiField("tx-type", "type")]
        public TransactionType TransactionType
        {
            get => TransactionType.Payment;
            internal set => header.TransactionType = TransactionType.Payment;
        }

        [AlgoApiField("genesis-id", "gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        [AlgoApiField("group", "grp")]
        public TransactionId Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        [AlgoApiField("lease", "lx")]
        public TransactionId Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        [AlgoApiField("note", "note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        [AlgoApiField("rekey-to", "rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        [AlgoApiField(null, "rcv")]
        public Address Receiver
        {
            get => @params.Receiver;
            set => @params.Receiver = value;
        }

        [AlgoApiField(null, "amt")]
        public MicroAlgos Amount
        {
            get => @params.Amount;
            set => @params.Amount = value;
        }

        [AlgoApiField(null, "close")]
        public Address CloseRemainderTo
        {
            get => @params.CloseRemainderTo;
            set => @params.CloseRemainderTo = value;
        }

        [AlgoApiField("close-amount", "close-amount", readOnly: true)]
        public ulong CloseAmount
        {
            get => @params.CloseAmount;
            set => @params.CloseAmount = value;
        }

        public void CopyTo(ref Transaction transaction)
        {
            transaction.Header = header;
            transaction.PaymentParams = @params;
        }

        public void CopyFrom(Transaction transaction)
        {
            header = transaction.Header;
            @params = transaction.PaymentParams;
        }

        public bool Equals(PaymentTxn other)
        {
            return header.Equals(other.header)
                && @params.Equals(other.@params)
                ;
        }

        [AlgoApiObject]
        [Serializable]
        public partial struct Params
            : IEquatable<Params>
        {
            [AlgoApiField("receiver", "rcv")]
            [Tooltip("The address of the account that receives the Amount.")]
            public Address Receiver;

            [AlgoApiField("amount", "amt")]
            [Tooltip("The total amount to be sent in microAlgos.")]
            public MicroAlgos Amount;

            [AlgoApiField("close-remainder-to", "close")]
            [Tooltip("When set, it indicates that the transaction is requesting that the Sender account should be closed, and all remaining funds, after the Fee and Amount are paid, be transferred to this address.")]
            public Address CloseRemainderTo;

            [AlgoApiField("close-amount", "close-amount")]
            [NonSerialized]
            public ulong CloseAmount;

            public bool Equals(Params other)
            {
                return Receiver.Equals(other.Receiver)
                    && Amount.Equals(other.Amount)
                    && CloseRemainderTo.Equals(other.CloseRemainderTo)
                    && CloseAmount.Equals(other.CloseAmount)
                    ;
            }
        }
    }
}

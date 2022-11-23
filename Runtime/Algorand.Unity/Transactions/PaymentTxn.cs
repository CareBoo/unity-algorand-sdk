using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
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
        /// <inheritdoc />
        [AlgoApiField("rcv")]
        public Address Receiver
        {
            get => paymentParams.Receiver;
            set => paymentParams.Receiver = value;
        }

        /// <inheritdoc />
        [AlgoApiField("amt")]
        public MicroAlgos Amount
        {
            get => paymentParams.Amount;
            set => paymentParams.Amount = value;
        }

        /// <inheritdoc />
        [AlgoApiField("close")]
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

        [SerializeField] private Params @params;

        /// <inheritdoc />
        [AlgoApiField("fee")]
        public MicroAlgos Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        /// <inheritdoc />
        [AlgoApiField("fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        /// <inheritdoc />
        [AlgoApiField("type")]
        public TransactionType TransactionType
        {
            get => TransactionType.Payment;
            internal set => header.TransactionType = TransactionType.Payment;
        }

        /// <inheritdoc />
        [AlgoApiField("gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        /// <inheritdoc />
        [AlgoApiField("grp")]
        public TransactionId Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lx")]
        public TransactionId Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        /// <inheritdoc />
        [AlgoApiField("note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rcv")]
        public Address Receiver
        {
            get => @params.Receiver;
            set => @params.Receiver = value;
        }

        /// <inheritdoc />
        [AlgoApiField("amt")]
        public MicroAlgos Amount
        {
            get => @params.Amount;
            set => @params.Amount = value;
        }

        /// <inheritdoc />
        [AlgoApiField("close")]
        public Address CloseRemainderTo
        {
            get => @params.CloseRemainderTo;
            set => @params.CloseRemainderTo = value;
        }

        /// <inheritdoc />
        public void CopyTo(ref Transaction transaction)
        {
            transaction.Header = header;
            transaction.PaymentParams = @params;
        }

        /// <inheritdoc />
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
            [AlgoApiField("rcv")]
            [Tooltip("The address of the account that receives the Amount.")]
            public Address Receiver;

            [AlgoApiField("amt")]
            [Tooltip("The total amount to be sent in microAlgos.")]
            public MicroAlgos Amount;

            [AlgoApiField("close")]
            [Tooltip("When set, it indicates that the transaction is requesting that the Sender account should be closed, and all remaining funds, after the Fee and Amount are paid, be transferred to this address.")]
            public Address CloseRemainderTo;

            public bool Equals(Params other)
            {
                return Receiver.Equals(other.Receiver)
                    && Amount.Equals(other.Amount)
                    && CloseRemainderTo.Equals(other.CloseRemainderTo)
                    ;
            }
        }
    }
}

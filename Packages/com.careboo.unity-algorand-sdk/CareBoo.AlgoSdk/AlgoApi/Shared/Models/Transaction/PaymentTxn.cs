using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public partial struct Transaction
    {
        [AlgoApiField(null, "rcv")]
        public Address Receiver
        {
            get => PaymentParams.Receiver;
            set => PaymentParams.Receiver = value;
        }

        [AlgoApiField(null, "amt")]
        public ulong Amount
        {
            get => PaymentParams.Amount;
            set => PaymentParams.Amount = value;
        }

        [AlgoApiField(null, "close")]
        public Address CloseRemainderTo
        {
            get => PaymentParams.CloseRemainderTo;
            set => PaymentParams.CloseRemainderTo = value;
        }

        public static PaymentTxn Payment(
            Address sender,
            TransactionParams txnParams,
            Address receiver,
            ulong amount
        )
        {
            var txn = new PaymentTxn
            {
                header = new TransactionHeader(sender, TransactionType.Payment, txnParams),
                Receiver = receiver,
                Amount = amount
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }
    }

    [AlgoApiObject]
    public struct PaymentTxn
        : ITransaction
        , IEquatable<PaymentTxn>
    {
        internal TransactionHeader header;

        Params @params;

        [AlgoApiField("fee", "fee")]
        public ulong Fee
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
        public Sha512_256_Hash Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        [AlgoApiField("lease", "lx")]
        public Sha512_256_Hash Lease
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
        public ulong Amount
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
            transaction.HeaderParams = header;
            transaction.PaymentParams = @params;
        }

        public void CopyFrom(Transaction transaction)
        {
            header = transaction.HeaderParams;
            @params = transaction.PaymentParams;
        }

        public bool Equals(PaymentTxn other)
        {
            return header.Equals(other.header)
                && @params.Equals(other.@params)
                ;
        }

        [AlgoApiObject]
        public struct Params
            : IEquatable<Params>
        {
            [AlgoApiField("receiver", "rcv")]
            public Address Receiver;

            [AlgoApiField("amount", "amt")]
            public ulong Amount;

            [AlgoApiField("close-remainder-to", "close")]
            public Address CloseRemainderTo;

            [AlgoApiField("close-amount", "close-amount")]
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

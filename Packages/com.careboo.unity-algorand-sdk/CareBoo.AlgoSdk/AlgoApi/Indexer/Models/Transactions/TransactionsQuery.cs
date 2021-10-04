using System;
using Unity.Collections;

namespace AlgoSdk
{
    public struct TransactionsQuery
        : IEquatable<TransactionsQuery>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("address-role", null)]
        public AddressRole AddressRole;

        [AlgoApiField("after-time", null)]
        public DateTime AfterTime;

        [AlgoApiField("application-id", null)]
        public ulong ApplicationId;

        [AlgoApiField("asset-id", null)]
        public ulong AssetId;

        [AlgoApiField("before-time", null)]
        public DateTime BeforeTime;

        [AlgoApiField("currency-greater-than", null)]
        public ulong CurrencyGreaterThan;

        [AlgoApiField("currency-less-than", null)]
        public ulong CurrencyLessThan;

        [AlgoApiField("exclude-close-to", null)]
        public Optional<bool> ExcludeCloseTo;

        [AlgoApiField("limit", null)]
        public ulong Limit;

        [AlgoApiField("max-round", null)]
        public ulong MaxRound;

        [AlgoApiField("min-round", null)]
        public ulong MinRound;

        [AlgoApiField("next", null)]
        public FixedString128Bytes Next;

        [AlgoApiField("note-prefix", null)]
        public string NotePrefix;

        [AlgoApiField("rekey-to", null)]
        public Optional<bool> RekeyTo;

        [AlgoApiField("sig-type", null)]
        public SignatureType SigType;

        [AlgoApiField("tx-type", null)]
        public TransactionType TxType;

        [AlgoApiField("txid", null)]
        public TransactionId TxId;

        public bool Equals(TransactionsQuery other)
        {
            return Address.Equals(other.Address)
                && AddressRole.Equals(other.AddressRole)
                && AfterTime.Equals(other.AfterTime)
                && ApplicationId.Equals(other.ApplicationId)
                && AssetId.Equals(other.AssetId)
                && BeforeTime.Equals(other.BeforeTime)
                && CurrencyGreaterThan.Equals(other.CurrencyGreaterThan)
                && CurrencyLessThan.Equals(other.CurrencyLessThan)
                && ExcludeCloseTo.Equals(other.ExcludeCloseTo)
                && Limit.Equals(other.Limit)
                && MaxRound.Equals(other.MaxRound)
                && MinRound.Equals(other.MinRound)
                && Next.Equals(other.Next)
                && NotePrefix.Equals(other.NotePrefix)
                && RekeyTo.Equals(other.RekeyTo)
                && SigType.Equals(other.SigType)
                && TxType.Equals(other.TxType)
                && TxId.Equals(other.TxId)
                ;
        }
    }
}

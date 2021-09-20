using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct RawTransaction
        : IEquatable<RawTransaction>
    {
        [AlgoApiKey("votekey")]
        public Address VotePk;
        [AlgoApiKey("selkey")]
        public VrfPubkey SelectionPk;
        [AlgoApiKey("votefst")]
        public ulong VoteFirst;
        [AlgoApiKey("votelst")]
        public ulong VoteLast;
        [AlgoApiKey("votekd")]
        public ulong VoteKeyDilution;
        [AlgoApiKey("nonpart")]
        public Optional<bool> NonParticipation;
        [AlgoApiKey("apid")]
        public ulong ApplicationId;
        [AlgoApiKey("apan")]
        public ulong OnComplete;
        [AlgoApiKey("apat")]
        public Address[] Accounts;
        [AlgoApiKey("apap")]
        public byte[] ApprovalProgram;
        [AlgoApiKey("apaa")]
        public byte[] AppArguments;
        [AlgoApiKey("apsu")]
        public byte[] ClearStateProgram;
        [AlgoApiKey("apfa")]
        public Address[] ForeignApps;
        [AlgoApiKey("apas")]
        public Address[] ForeignAssets;
        [AlgoApiKey("apgs")]
        public StateSchema GlobalStateSchema;
        [AlgoApiKey("apls")]
        public StateSchema LocalStateSchema;
        [AlgoApiKey("apep")]
        public StateSchema ExtraProgramPages;

        public Transaction.Header Header;
        public Transaction.Payment.Params PaymentParams;
        public Transaction.AssetConfiguration.Params AssetConfigurationParams;
        public Transaction.AssetTransfer.Params AssetTransferParams;
        public Transaction.AssetFreeze.Params AssetFreezeParams;

        public static bool operator ==(in RawTransaction x, in RawTransaction y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(in RawTransaction x, in RawTransaction y)
        {
            return !x.Equals(y);
        }

        public bool Equals(RawTransaction other)
        {
            return Header.Equals(other.Header)
                && TransactionType switch
                {
                    TransactionType.Payment => PaymentParams.Equals(other.PaymentParams),
                    TransactionType.AssetConfiguration => AssetConfigurationParams.Equals(other.AssetConfigurationParams),
                    _ => true
                };
        }

        public override bool Equals(object obj)
        {
            if (obj is RawTransaction other)
                return Equals(other);
            return false;
        }

        public override int GetHashCode() => Header.GetHashCode();
    }
}

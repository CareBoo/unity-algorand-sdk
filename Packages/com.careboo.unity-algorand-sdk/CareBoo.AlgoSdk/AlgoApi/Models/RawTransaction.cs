using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct RawTransaction
        : IEquatable<RawTransaction>
        , IMessagePackObject
    {
        [AlgoApiKey("fee")]
        public ulong Fee;
        [AlgoApiKey("fv")]
        public ulong FirstValidRound;
        [AlgoApiKey("gh")]
        public GenesisHash GenesisHash;
        [AlgoApiKey("lv")]
        public ulong LastValidRound;
        [AlgoApiKey("snd")]
        public Address Sender;
        [AlgoApiKey("type")]
        public TransactionType TransactionType;
        [AlgoApiKey("gen")]
        public FixedString32Bytes GenesisId;
        [AlgoApiKey("grp")]
        public Address Group;
        [AlgoApiKey("lx")]
        public Address Lease;
        [AlgoApiKey("note")]
        public byte[] Note;
        [AlgoApiKey("rekey")]
        public Address RekeyTo;
        [AlgoApiKey("rcv")]
        public Address Receiver;
        [AlgoApiKey("amt")]
        public ulong Amount;
        [AlgoApiKey("close")]
        public Address CloseRemainderTo;
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
        [AlgoApiKey("caid")]
        public ulong ConfigAsset;
        [AlgoApiKey("apar")]
        public AssetParams AssetParams;
        [AlgoApiKey("xaid")]
        public ulong XferAsset;
        [AlgoApiKey("aamt")]
        public ulong AssetAmount;
        [AlgoApiKey("asnd")]
        public Address AssetSender;
        [AlgoApiKey("arcv")]
        public Address AssetReceiver;
        [AlgoApiKey("aclose")]
        public Address AssetCloseTo;
        [AlgoApiKey("fadd")]
        public Address FreezeAccount;
        [AlgoApiKey("faid")]
        public ulong FreezeAsset;
        [AlgoApiKey("afrz")]
        public Optional<bool> AssetFrozen;
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
            return this.Equals(ref other);
        }

        public override bool Equals(object obj)
        {
            if (obj is RawTransaction other)
                return Equals(other);
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Fee.GetHashCode();
                hash = hash * 31 + FirstValidRound.GetHashCode();
                return hash;
            }
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        private static readonly Field<RawTransaction>.Map rawTransactionFields = new Field<RawTransaction>.Map()
            .Assign("aamt", (ref RawTransaction r) => ref r.AssetAmount)
            .Assign("aclose", (ref RawTransaction r) => ref r.AssetCloseTo)
            .Assign("afrz", (ref RawTransaction r) => ref r.AssetFrozen)
            .Assign("amt", (ref RawTransaction r) => ref r.Amount)
            .Assign("apaa", (ref RawTransaction r) => ref r.AppArguments, ArrayComparer<byte>.Instance)
            .Assign("apan", (ref RawTransaction r) => ref r.OnComplete)
            .Assign("apap", (ref RawTransaction r) => ref r.ApprovalProgram, ArrayComparer<byte>.Instance)
            .Assign("apar", (ref RawTransaction r) => ref r.AssetParams)
            .Assign("apas", (ref RawTransaction r) => ref r.ForeignAssets, ArrayComparer<Address>.Instance)
            .Assign("apat", (ref RawTransaction r) => ref r.Accounts, ArrayComparer<Address>.Instance)
            .Assign("apep", (ref RawTransaction r) => ref r.ExtraProgramPages)
            .Assign("apfa", (ref RawTransaction r) => ref r.ForeignApps, ArrayComparer<Address>.Instance)
            .Assign("apgs", (ref RawTransaction r) => ref r.GlobalStateSchema)
            .Assign("apid", (ref RawTransaction r) => ref r.ApplicationId)
            .Assign("apls", (ref RawTransaction r) => ref r.LocalStateSchema)
            .Assign("apsu", (ref RawTransaction r) => ref r.ClearStateProgram, ArrayComparer<byte>.Instance)
            .Assign("arcv", (ref RawTransaction r) => ref r.AssetReceiver)
            .Assign("asnd", (ref RawTransaction r) => ref r.AssetSender)
            .Assign("caid", (ref RawTransaction r) => ref r.ConfigAsset)
            .Assign("close", (ref RawTransaction r) => ref r.CloseRemainderTo)
            .Assign("fadd", (ref RawTransaction r) => ref r.FreezeAccount)
            .Assign("faid", (ref RawTransaction r) => ref r.FreezeAsset)
            .Assign("fee", (ref RawTransaction r) => ref r.Fee)
            .Assign("fv", (ref RawTransaction r) => ref r.FirstValidRound)
            .Assign("gen", (ref RawTransaction r) => ref r.GenesisId)
            .Assign("gh", (ref RawTransaction r) => ref r.GenesisHash)
            .Assign("grp", (ref RawTransaction r) => ref r.Group)
            .Assign("lv", (ref RawTransaction r) => ref r.LastValidRound)
            .Assign("lx", (ref RawTransaction r) => ref r.Lease)
            .Assign("nonpart", (ref RawTransaction r) => ref r.NonParticipation)
            .Assign("note", (ref RawTransaction r) => ref r.Note, ArrayComparer<byte>.Instance)
            .Assign("rcv", (ref RawTransaction r) => ref r.Receiver)
            .Assign("rekey", (ref RawTransaction r) => ref r.RekeyTo)
            .Assign("selkey", (ref RawTransaction r) => ref r.SelectionPk)
            .Assign("snd", (ref RawTransaction r) => ref r.Sender)
            .Assign("type", (ref RawTransaction r) => ref r.TransactionType, TransactionTypeComparer.Instance)
            .Assign("votefst", (ref RawTransaction r) => ref r.VoteFirst)
            .Assign("votekd", (ref RawTransaction r) => ref r.VoteKeyDilution)
            .Assign("votekey", (ref RawTransaction r) => ref r.VotePk)
            .Assign("votelst", (ref RawTransaction r) => ref r.VoteLast)
            .Assign("xaid", (ref RawTransaction r) => ref r.XferAsset)
            ;
    }
}

using System;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct RawTransaction
        : IEquatable<RawTransaction>
        , IMessagePackObject
    {
        public ulong Fee;
        public ulong FirstValidRound;
        public Sha512_256_Hash GenesisHash;
        public ulong LastValidRound;
        public Address Sender;
        public TransactionType TransactionType;
        public FixedString32 GenesisId;
        public Address Group;
        public Address Lease;
        public NativeText Note;
        public Address RekeyTo;
        public Address Receiver;
        public ulong Amount;
        public Address CloseRemainderTo;
        public Ed25519.PublicKey VotePk;
        public VrfPubkey SelectionPk;
        public ulong VoteFirst;
        public ulong VoteLast;
        public ulong VoteKeyDilution;
        public Optional<bool> NonParticipation;
        public ulong ConfigAsset;
        public AssetParams AssetParams;
        public ulong XferAsset;
        public ulong AssetAmount;
        public Address AssetSender;
        public Address AssetReceiver;
        public Address AssetCloseTo;
        public Address FreezeAccount;
        public ulong FreezeAsset;
        public Optional<bool> AssetFrozen;
        public ulong ApplicationId;
        public ulong OnComplete;
        public NativeList<Address> Accounts;
        public NativeArray<byte> ApprovalProgram;
        public NativeArray<byte> AppArguments;
        public NativeArray<byte> ClearStateProgram;
        public NativeList<Address> ForeignApps;
        public NativeList<Address> ForeignAssets;
        public StateSchema GlobalStateSchema;
        public StateSchema LocalStateSchema;
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
            .Assign("apaa", (ref RawTransaction r) => ref r.AppArguments, default(NativeArrayComparer<byte>))
            .Assign("apan", (ref RawTransaction r) => ref r.OnComplete)
            .Assign("apap", (ref RawTransaction r) => ref r.ApprovalProgram, default(NativeArrayComparer<byte>))
            .Assign("apar", (ref RawTransaction r) => ref r.AssetParams)
            .Assign("apas", (ref RawTransaction r) => ref r.ForeignAssets, default(NativeListComparer<Address>))
            .Assign("apat", (ref RawTransaction r) => ref r.Accounts, default(NativeListComparer<Address>))
            .Assign("apep", (ref RawTransaction r) => ref r.ExtraProgramPages)
            .Assign("apfa", (ref RawTransaction r) => ref r.ForeignApps, default(NativeListComparer<Address>))
            .Assign("apgs", (ref RawTransaction r) => ref r.GlobalStateSchema)
            .Assign("apid", (ref RawTransaction r) => ref r.ApplicationId)
            .Assign("apls", (ref RawTransaction r) => ref r.LocalStateSchema)
            .Assign("apsu", (ref RawTransaction r) => ref r.ClearStateProgram, default(NativeArrayComparer<byte>))
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
            .Assign("note", (ref RawTransaction r) => ref r.Note, default(NativeTextComparer))
            .Assign("rcv", (ref RawTransaction r) => ref r.Receiver)
            .Assign("rekey", (ref RawTransaction r) => ref r.RekeyTo)
            .Assign("selkey", (ref RawTransaction r) => ref r.SelectionPk)
            .Assign("snd", (ref RawTransaction r) => ref r.Sender)
            .Assign("type", (ref RawTransaction r) => ref r.TransactionType, default(TransactionTypeComparer))
            .Assign("votefst", (ref RawTransaction r) => ref r.VoteFirst)
            .Assign("votekd", (ref RawTransaction r) => ref r.VoteKeyDilution)
            .Assign("votekey", (ref RawTransaction r) => ref r.VotePk)
            .Assign("votelst", (ref RawTransaction r) => ref r.VoteLast)
            .Assign("xaid", (ref RawTransaction r) => ref r.XferAsset)
            ;
    }
}

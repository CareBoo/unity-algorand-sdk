using System;
using System.Collections.Generic;
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
        private static readonly SortedDictionary<FixedString32, Field<RawTransaction>> rawTransactionFields = new SortedDictionary<FixedString32, Field<RawTransaction>>()
        {
            {"aamt", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetAmount)},
            {"aclose", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetCloseTo)},
            {"afrz", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetFrozen)},
            {"amt", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.Amount)},
            {"apaa", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AppArguments, default(NativeArrayComparer<byte>))},
            {"apan", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.OnComplete)},
            {"apap", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.ApprovalProgram, default(NativeArrayComparer<byte>))},
            {"apar", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetParams)},
            {"apas", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.ForeignAssets, default(NativeListComparer<Address>))},
            {"apat", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.Accounts, default(NativeListComparer<Address>))},
            {"apep", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.ExtraProgramPages)},
            {"apfa", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.ForeignApps, default(NativeListComparer<Address>))},
            {"apgs", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.GlobalStateSchema)},
            {"apid", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.ApplicationId)},
            {"apls", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.LocalStateSchema)},
            {"apsu", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.ClearStateProgram, default(NativeArrayComparer<byte>))},
            {"arcv", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetReceiver)},
            {"asnd", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetSender)},
            {"caid", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.ConfigAsset)},
            {"close", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.CloseRemainderTo)},
            {"fadd", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.FreezeAccount)},
            {"faid", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.FreezeAsset)},
            {"fee", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.Fee)},
            {"fv", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.FirstValidRound)},
            {"gen", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.GenesisId)},
            {"gh", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.GenesisHash)},
            {"grp", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.Group)},
            {"lv", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.LastValidRound)},
            {"lx", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.Lease)},
            {"nonpart", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.NonParticipation)},
            {"note", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.Note, default(NativeTextComparer))},
            {"rcv", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.Receiver)},
            {"rekey", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.RekeyTo)},
            {"selkey", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.SelectionPk)},
            {"snd", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.Sender)},
            {"type", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.TransactionType, default(TransactionTypeComparer))},
            {"votefst", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.VoteFirst)},
            {"votekd", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.VoteKeyDilution)},
            {"votekey", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.VotePk)},
            {"votelst", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.VoteLast)},
            {"xaid", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.XferAsset)},
        };
    }
}

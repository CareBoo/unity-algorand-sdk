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
        public Field<ulong> Fee;
        public Field<ulong> FirstValidRound;
        public Field<Sha512_256_Hash> GenesisHash;
        public Field<ulong> LastValidRound;
        public Field<Address> Sender;
        public Field<TransactionType> TransactionType;
        public Field<FixedString32> GenesisId;
        public Field<Address> Group;
        public Field<Address> Lease;
        public Field<NativeText> Note;
        public Field<Address> RekeyTo;
        public Field<Address> Receiver;
        public Field<ulong> Amount;
        public Field<Address> CloseRemainderTo;
        public Field<Ed25519.PublicKey> VotePk;
        public Field<VrfPubkey> SelectionPk;
        public Field<ulong> VoteFirst;
        public Field<ulong> VoteLast;
        public Field<ulong> VoteKeyDilution;
        public Field<Optional<bool>> NonParticipation;
        public Field<ulong> ConfigAsset;
        public Field<AssetParams> AssetParams;
        public Field<ulong> XferAsset;
        public Field<ulong> AssetAmount;
        public Field<Address> AssetSender;
        public Field<Address> AssetReceiver;
        public Field<Address> AssetCloseTo;
        public Field<Address> FreezeAccount;
        public Field<ulong> FreezeAsset;
        public Field<Optional<bool>> AssetFrozen;
        public Field<ulong> ApplicationId;
        public Field<ulong> OnComplete;
        public Field<NativeList<Address>> Accounts;
        public Field<NativeArray<byte>> ApprovalProgram;
        public Field<NativeArray<byte>> AppArguments;
        public Field<NativeArray<byte>> ClearStateProgram;
        public Field<NativeList<Address>> ForeignApps;
        public Field<NativeList<Address>> ForeignAssets;
        public Field<StateSchema> GlobalStateSchema;
        public Field<StateSchema> LocalStateSchema;
        public Field<StateSchema> ExtraProgramPages;

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
        private static readonly SortedDictionary<FixedString32, FieldFor<RawTransaction>> rawTransactionFields = new SortedDictionary<FixedString32, FieldFor<RawTransaction>>()
        {
            {"aamt", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetAmount)},
            {"aclose", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetCloseTo)},
            {"afrz", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetFrozen)},
            {"amt", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.Amount)},
            {"apaa", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.AppArguments, default(NativeArrayComparer<byte>))},
            {"apan", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.OnComplete)},
            {"apap", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.ApprovalProgram, default(NativeArrayComparer<byte>))},
            {"apar", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetParams)},
            {"apas", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.ForeignAssets, default(NativeListComparer<Address>))},
            {"apat", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.Accounts, default(NativeListComparer<Address>))},
            {"apep", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.ExtraProgramPages)},
            {"apfa", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.ForeignApps, default(NativeListComparer<Address>))},
            {"apgs", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.GlobalStateSchema)},
            {"apid", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.ApplicationId)},
            {"apls", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.LocalStateSchema)},
            {"apsu", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.ClearStateProgram, default(NativeArrayComparer<byte>))},
            {"arcv", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetReceiver)},
            {"asnd", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetSender)},
            {"caid", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.ConfigAsset)},
            {"close", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.CloseRemainderTo)},
            {"fadd", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.FreezeAccount)},
            {"faid", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.FreezeAsset)},
            {"fee", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.Fee)},
            {"fv", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.FirstValidRound)},
            {"gen", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.GenesisId)},
            {"gh", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.GenesisHash)},
            {"grp", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.Group)},
            {"lv", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.LastValidRound)},
            {"lx", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.Lease)},
            {"nonpart", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.NonParticipation)},
            {"note", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.Note, default(NativeTextComparer))},
            {"rcv", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.Receiver)},
            {"rekey", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.RekeyTo)},
            {"selkey", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.SelectionPk)},
            {"snd", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.Sender)},
            {"type", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.TransactionType, default(TransactionTypeComparer))},
            {"votefst", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.VoteFirst)},
            {"votekd", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.VoteKeyDilution)},
            {"votekey", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.VotePk)},
            {"votelst", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.VoteLast)},
            {"xaid", FieldFor<RawTransaction>.Assign((ref RawTransaction r) => ref r.XferAsset)},
        };
    }
}

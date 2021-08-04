using System;
using System.Collections.Generic;
using System.Linq;
using AlgoSdk.Crypto;
using MessagePack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.MsgPack
{
    [MessagePackObject]
    public struct RawTransaction
        : IEquatable<RawTransaction>
        , IMessagePackType<RawTransaction>
    {
        public Prop<ulong> Fee;
        public Prop<ulong> FirstValidRound;
        public Prop<Sha512_256_Hash> GenesisHash;
        public Prop<ulong> LastValidRound;
        public Prop<Address> Sender;
        public Prop<TransactionType> TransactionType;
        public Prop<FixedString32> GenesisId;
        public Prop<Address> Group;
        public Prop<Address> Lease;
        public Prop<NativeText> Note;
        public Prop<Address> RekeyTo;
        public Prop<Address> Receiver;
        public Prop<ulong> Amount;
        public Prop<Address> CloseRemainderTo;
        public Prop<Ed25519.PublicKey> VotePk;
        public Prop<VrfPubkey> SelectionPk;
        public Prop<ulong> VoteFirst;
        public Prop<ulong> VoteLast;
        public Prop<ulong> VoteKeyDilution;
        public Prop<NativeReference<bool>> NonParticipation;
        public Prop<ulong> ConfigAsset;
        public Prop<AssetParams> AssetParams;
        public Prop<ulong> XferAsset;
        public Prop<ulong> AssetAmount;
        public Prop<Address> AssetSender;
        public Prop<Address> AssetReceiver;
        public Prop<Address> AssetCloseTo;
        public Prop<Address> FreezeAccount;
        public Prop<ulong> FreezeAsset;
        public Prop<NativeReference<bool>> AssetFrozen;
        public Prop<ulong> ApplicationId;
        public Prop<ulong> OnComplete;
        public Prop<NativeList<Address>> Accounts;
        public Prop<NativeArray<byte>> ApprovalProgram;
        public Prop<NativeArray<byte>> AppArguments;
        public Prop<NativeArray<byte>> ClearStateProgram;
        public Prop<NativeList<Address>> ForeignApps;
        public Prop<NativeList<Address>> ForeignAssets;
        public Prop<StateSchema> GlobalStateSchema;
        public Prop<StateSchema> LocalStateSchema;
        public Prop<StateSchema> ExtraProgramPages;

        private static readonly FixedString32[] orderedFieldNames;

        private static readonly Dictionary<FixedString32, Field<RawTransaction>> fields;

        static RawTransaction()
        {
            fields = new Dictionary<FixedString32, Field<RawTransaction>>()
            {
                {"aamt", Field<RawTransaction>.Assign((ref RawTransaction r) => r.AssetAmount)},
                {"aclose", Field<RawTransaction>.Assign((ref RawTransaction r) => r.AssetCloseTo)},
                {"afrz", Field<RawTransaction>.Assign((ref RawTransaction r) => r.AssetFrozen)},
                {"amt", Field<RawTransaction>.Assign((ref RawTransaction r) => r.Amount)},
                {"apaa", Field<RawTransaction>.Assign((ref RawTransaction r) => r.AppArguments)},
                {"apan", Field<RawTransaction>.Assign((ref RawTransaction r) => r.OnComplete)},
                {"apap", Field<RawTransaction>.Assign((ref RawTransaction r) => r.ApprovalProgram)},
                {"apar", Field<RawTransaction>.Assign((ref RawTransaction r) => r.AssetParams)},
                {"apas", Field<RawTransaction>.Assign((ref RawTransaction r) => r.ForeignAssets, default(NativeListComparer<Address>))},
                {"apat", Field<RawTransaction>.Assign((ref RawTransaction r) => r.Accounts, default(NativeListComparer<Address>))},
                {"apep", Field<RawTransaction>.Assign((ref RawTransaction r) => r.ExtraProgramPages)},
                {"apfa", Field<RawTransaction>.Assign((ref RawTransaction r) => r.ForeignApps, default(NativeListComparer<Address>))},
                {"apgs", Field<RawTransaction>.Assign((ref RawTransaction r) => r.GlobalStateSchema)},
                {"apid", Field<RawTransaction>.Assign((ref RawTransaction r) => r.ApplicationId)},
                {"apls", Field<RawTransaction>.Assign((ref RawTransaction r) => r.LocalStateSchema)},
                {"apsu", Field<RawTransaction>.Assign((ref RawTransaction r) => r.ClearStateProgram)},
                {"arcv", Field<RawTransaction>.Assign((ref RawTransaction r) => r.AssetReceiver)},
                {"asnd", Field<RawTransaction>.Assign((ref RawTransaction r) => r.AssetSender)},
                {"caid", Field<RawTransaction>.Assign((ref RawTransaction r) => r.ConfigAsset)},
                {"close", Field<RawTransaction>.Assign((ref RawTransaction r) => r.CloseRemainderTo)},
                {"fadd", Field<RawTransaction>.Assign((ref RawTransaction r) => r.FreezeAccount)},
                {"faid", Field<RawTransaction>.Assign((ref RawTransaction r) => r.FreezeAsset)},
                {"fee", Field<RawTransaction>.Assign((ref RawTransaction r) => r.Fee)},
                {"fv", Field<RawTransaction>.Assign((ref RawTransaction r) => r.FirstValidRound)},
                {"gen", Field<RawTransaction>.Assign((ref RawTransaction r) => r.GenesisId)},
                {"gh", Field<RawTransaction>.Assign((ref RawTransaction r) => r.GenesisHash)},
                {"grp", Field<RawTransaction>.Assign((ref RawTransaction r) => r.Group)},
                {"lv", Field<RawTransaction>.Assign((ref RawTransaction r) => r.LastValidRound)},
                {"lx", Field<RawTransaction>.Assign((ref RawTransaction r) => r.Lease)},
                {"nonpart", Field<RawTransaction>.Assign((ref RawTransaction r) => r.NonParticipation)},
                {"note", Field<RawTransaction>.Assign((ref RawTransaction r) => r.Note, default(NativeTextComparer))},
                {"rcv", Field<RawTransaction>.Assign((ref RawTransaction r) => r.Receiver)},
                {"rekey", Field<RawTransaction>.Assign((ref RawTransaction r) => r.RekeyTo)},
                {"selkey", Field<RawTransaction>.Assign((ref RawTransaction r) => r.SelectionPk)},
                {"snd", Field<RawTransaction>.Assign((ref RawTransaction r) => r.Sender)},
                {"type", Field<RawTransaction>.Assign((ref RawTransaction r) => r.TransactionType, default(TransactionTypeComparer))},
                {"votefst", Field<RawTransaction>.Assign((ref RawTransaction r) => r.VoteFirst)},
                {"votekd", Field<RawTransaction>.Assign((ref RawTransaction r) => r.VoteKeyDilution)},
                {"votekey", Field<RawTransaction>.Assign((ref RawTransaction r) => r.VotePk)},
                {"votelst", Field<RawTransaction>.Assign((ref RawTransaction r) => r.VoteLast)},
                {"xaid", Field<RawTransaction>.Assign((ref RawTransaction r) => r.XferAsset)},
            };

            orderedFieldNames = fields.Keys.ToArray();
            Array.Sort(orderedFieldNames);
        }

        public static bool operator ==(in RawTransaction x, in RawTransaction y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(in RawTransaction x, in RawTransaction y)
        {
            return !x.Equals(y);
        }

        public Dictionary<FixedString32, Field<RawTransaction>> MessagePackFields => fields;

        public NativeList<FixedString32> GetFieldsToSerialize(Allocator allocator)
        {
            var list = new NativeList<FixedString32>(orderedFieldNames.Length, allocator);
            for (var i = 0; i < orderedFieldNames.Length; i++)
            {
                var fieldName = orderedFieldNames[i];
                if (!fields[fieldName].ShouldSerialize(ref this))
                    list.Add(fieldName);
            }
            return list;
        }

        public bool Equals(RawTransaction other)
        {
            for (var i = 0; i < orderedFieldNames.Length; i++)
            {
                var fieldName = orderedFieldNames[i];
                var field = fields[fieldName];
                if (!field.FieldsEqual(ref this, ref other))
                    return false;
            }
            return true;
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

    public struct NativeListComparer<T> : IEqualityComparer<NativeList<T>>
        where T : unmanaged
    {
        public unsafe bool Equals(NativeList<T> x, NativeList<T> y)
        {
            return x.IsCreated == y.IsCreated
                && (!x.IsCreated || (x.Length == y.Length && x.GetUnsafePtr() == y.GetUnsafePtr()));
        }

        public int GetHashCode(NativeList<T> obj)
        {
            return obj.GetHashCode();
        }
    }

    public struct TransactionTypeComparer : IEqualityComparer<TransactionType>
    {
        public bool Equals(TransactionType x, TransactionType y)
        {
            return x == y;
        }

        public int GetHashCode(TransactionType obj)
        {
            return obj.GetHashCode();
        }
    }

    public struct NativeTextComparer : IEqualityComparer<NativeText>
    {
        public bool Equals(NativeText x, NativeText y)
        {
            return x.IsCreated == y.IsCreated
                && (!x.IsCreated || x.Equals(y));
        }

        public int GetHashCode(NativeText obj)
        {
            return obj.GetHashCode();
        }
    }
}

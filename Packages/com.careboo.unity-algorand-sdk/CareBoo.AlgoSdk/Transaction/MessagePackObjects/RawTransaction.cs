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
                {"aamt", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetAmount)},
                {"aclose", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetCloseTo)},
                {"afrz", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.AssetFrozen, default(NativeReferenceComparer<bool>))},
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
                {"nonpart", Field<RawTransaction>.Assign((ref RawTransaction r) => ref r.NonParticipation, default(NativeReferenceComparer<bool>))},
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
}

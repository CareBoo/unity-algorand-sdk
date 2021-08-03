using System;
using System.Collections.Generic;
using AlgoSdk.Crypto;
using MessagePack;
using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    [MessagePackObject]
    public struct RawTransaction
        : IEquatable<RawTransaction>
    {
        [Key("fee")]
        public ulong Fee;

        [Key("fv")]
        public ulong FirstValidRound;

        [Key("gh")]
        public Sha512_256_Hash GenesisHash;

        [Key("lv")]
        public ulong LastValidRound;

        [Key("snd")]
        public Address Sender;

        [Key("type")]
        public TransactionType TransactionType;

        [Key("gen")]
        public FixedString32 GenesisId;

        [Key("grp")]
        public Address Group;

        [Key("lx")]
        public Address Lease;

        [Key("note")]
        public NativeText Note;

        [Key("rekey")]
        public Address RekeyTo;

        [Key("rcv")]
        public Address Receiver;

        [Key("amt")]
        public ulong Amount;

        [Key("close")]
        public Address CloseRemainderTo;

        [Key("votekey")]
        public Ed25519.PublicKey VotePk;

        [Key("selkey")]
        public VrfPubkey SelectionPk;

        [Key("votefst")]
        public ulong VoteFirst;

        [Key("votelst")]
        public ulong VoteLast;

        [Key("votekd")]
        public ulong VoteKeyDilution;

        [Key("nonpart")]
        public NativeReference<bool> NonParticipation;

        [Key("caid")]
        public ulong ConfigAsset;

        [Key("apar")]
        public AssetParams AssetParams;

        [Key("xaid")]
        public ulong XferAsset;

        [Key("aamt")]
        public ulong AssetAmount;

        [Key("asnd")]
        public Address AssetSender;

        [Key("arcv")]
        public Address AssetReceiver;

        [Key("aclose")]
        public Address AssetCloseTo;

        [Key("fadd")]
        public Address FreezeAccount;

        [Key("faid")]
        public ulong FreezeAsset;

        [Key("afrz")]
        public NativeReference<bool> AssetFrozen;

        [Key("apid")]
        public ulong ApplicationId;

        [Key("apan")]
        public ulong OnComplete;

        [Key("apat")]
        public NativeList<Address> Accounts;

        [Key("apap")]
        public NativeArray<byte> ApprovalProgram;

        [Key("apaa")]
        public NativeArray<byte> AppArguments;

        [Key("apsu")]
        public NativeArray<byte> ClearStateProgram;

        [Key("apfa")]
        public NativeList<Address> ForeignApps;

        [Key("apas")]
        public NativeList<Address> ForeignAssets;

        [Key("apgs")]
        public StateSchema GlobalStateSchema;

        [Key("apls")]
        public StateSchema LocalStateSchema;

        [Key("apep")]
        public StateSchema ExtraProgramPages;

        private static readonly FixedString32[] OrderedFields = new FixedString32[]
        {
            "aamt",
            "aclose",
            "afrz",
            "amt",
            "apaa",
            "apan",
            "apap",
            "apar",
            "apas",
            "apat",
            "apep",
            "apfa",
            "apgs",
            "apid",
            "apls",
            "apsu",
            "arcv",
            "asnd",
            "caid",
            "close",
            "fadd",
            "faid",
            "fee",
            "fv",
            "gen",
            "gh",
            "grp",
            "lv",
            "lx",
            "nonpart",
            "note",
            "rcv",
            "rekey",
            "selkey",
            "snd",
            "type",
            "votefst",
            "votekd",
            "votekey",
            "votelst",
            "xaid",
        };

        private delegate bool SerializePredicate(in RawTransaction data);

        private static readonly SerializePredicate[] ShouldSerialize = new SerializePredicate[]
        {
            (in RawTransaction data) => data.AssetAmount != default,
            (in RawTransaction data) => data.AssetCloseTo != default,
            (in RawTransaction data) => data.AssetFrozen.IsCreated,
            (in RawTransaction data) => data.Amount != default,
            (in RawTransaction data) => data.AppArguments.IsCreated,
            (in RawTransaction data) => data.OnComplete != default,
            (in RawTransaction data) => data.ApprovalProgram.IsCreated,
            (in RawTransaction data) => data.AssetParams != default,
            (in RawTransaction data) => data.ForeignAssets.IsCreated,
            (in RawTransaction data) => data.Accounts.IsCreated,
            (in RawTransaction data) => data.ExtraProgramPages != default,
            (in RawTransaction data) => data.ForeignApps.IsCreated,
            (in RawTransaction data) => data.GlobalStateSchema != default,
            (in RawTransaction data) => data.ApplicationId != default,
            (in RawTransaction data) => data.LocalStateSchema != default,
            (in RawTransaction data) => data.ClearStateProgram.IsCreated,
            (in RawTransaction data) => data.AssetReceiver != default,
            (in RawTransaction data) => data.AssetSender != default,
            (in RawTransaction data) => data.ConfigAsset != default,
            (in RawTransaction data) => data.CloseRemainderTo != default,
            (in RawTransaction data) => data.FreezeAccount != default,
            (in RawTransaction data) => data.FreezeAsset != default,
            (in RawTransaction data) => data.Fee != default,
            (in RawTransaction data) => data.FirstValidRound != default,
            (in RawTransaction data) => data.GenesisId != default,
            (in RawTransaction data) => data.GenesisHash != default,
            (in RawTransaction data) => data.Group != default,
            (in RawTransaction data) => data.LastValidRound != default,
            (in RawTransaction data) => data.Lease != default,
            (in RawTransaction data) => data.NonParticipation.IsCreated,
            (in RawTransaction data) => data.Note.IsCreated,
            (in RawTransaction data) => data.Receiver != default,
            (in RawTransaction data) => data.RekeyTo != default,
            (in RawTransaction data) => data.SelectionPk != default,
            (in RawTransaction data) => data.Sender != default,
            (in RawTransaction data) => data.TransactionType != default,
            (in RawTransaction data) => data.VoteFirst != default,
            (in RawTransaction data) => data.VoteKeyDilution != default,
            (in RawTransaction data) => data.VoteLast != default,
            (in RawTransaction data) => data.XferAsset != default,
        };

        public delegate T Getter<T>(in RawTransaction data);
        public delegate void Setter<T>(ref RawTransaction data, T value);
        public delegate void Deserializer(ref RawTransaction data, ref MessagePackReader reader, MessagePackSerializerOptions options);
        public delegate void Serializer(in RawTransaction data, ref MessagePackWriter writer, MessagePackSerializerOptions options);

        public static Dictionary<FixedString32, (Serializer serializer, Deserializer deserializer)> Serializers = new Dictionary<FixedString32, (Serializer serializer, Deserializer deserializer)>()
        {
            {"aamt", (GetSerializer((in RawTransaction r) => r.AssetAmount), GetDeserializer((ref RawTransaction r, ulong value) => r.AssetAmount = value))},
            {"aclose", (GetSerializer((in RawTransaction r) => r.AssetCloseTo), GetDeserializer((ref RawTransaction r, Address value) => r.AssetCloseTo = value))},
            {"afrz", (GetSerializer((in RawTransaction r) => r.AssetFrozen), GetDeserializer((ref RawTransaction r, NativeReference<bool> value) => r.AssetFrozen = value))},
            {"amt", (GetSerializer((in RawTransaction r) => r.Amount), GetDeserializer((ref RawTransaction r, ulong value) => r.Amount = value))},
            {"apaa", (GetSerializer((in RawTransaction r) => r.AppArguments), GetDeserializer((ref RawTransaction r, NativeArray<byte> value) => r.AppArguments = value))},
            {"apan", (GetSerializer((in RawTransaction r) => r.OnComplete), GetDeserializer((ref RawTransaction r, ulong value) => r.OnComplete = value))},
            {"apap", (GetSerializer((in RawTransaction r) => r.ApprovalProgram), GetDeserializer((ref RawTransaction r, NativeArray<byte> value) => r.ApprovalProgram = value))},
            {"apar", (GetSerializer((in RawTransaction r) => r.AssetParams), GetDeserializer((ref RawTransaction r, AssetParams value) => r.AssetParams = value))},
            {"apas", (GetSerializer((in RawTransaction r) => r.ForeignAssets), GetDeserializer((ref RawTransaction r, NativeList<Address> value) => r.ForeignAssets = value))},
            {"apat", (GetSerializer((in RawTransaction r) => r.Accounts), GetDeserializer((ref RawTransaction r, NativeList<Address> value) => r.Accounts = value))},
            {"apep", (GetSerializer((in RawTransaction r) => r.ExtraProgramPages), GetDeserializer((ref RawTransaction r, StateSchema value) => r.ExtraProgramPages = value))},
            {"apfa", (GetSerializer((in RawTransaction r) => r.ForeignApps), GetDeserializer((ref RawTransaction r, NativeList<Address> value) => r.ForeignApps = value))},
            {"apgs", (GetSerializer((in RawTransaction r) => r.GlobalStateSchema), GetDeserializer((ref RawTransaction r, StateSchema value) => r.GlobalStateSchema = value))},
            {"apid", (GetSerializer((in RawTransaction r) => r.ApplicationId), GetDeserializer((ref RawTransaction r, ulong value) => r.ApplicationId = value))},
            {"apls", (GetSerializer((in RawTransaction r) => r.LocalStateSchema), GetDeserializer((ref RawTransaction r, StateSchema value) => r.LocalStateSchema = value))},
            {"apsu", (GetSerializer((in RawTransaction r) => r.ClearStateProgram), GetDeserializer((ref RawTransaction r, NativeArray<byte> value) => r.ClearStateProgram = value))},
            {"arcv", (GetSerializer((in RawTransaction r) => r.AssetReceiver), GetDeserializer((ref RawTransaction r, Address value) => r.AssetReceiver = value))},
            {"asnd", (GetSerializer((in RawTransaction r) => r.AssetSender), GetDeserializer((ref RawTransaction r, Address value) => r.AssetSender = value))},
            {"caid", (GetSerializer((in RawTransaction r) => r.ConfigAsset), GetDeserializer((ref RawTransaction r, ulong value) => r.ConfigAsset = value))},
            {"close", (GetSerializer((in RawTransaction r) => r.CloseRemainderTo), GetDeserializer((ref RawTransaction r, Address value) => r.CloseRemainderTo = value))},
            {"fadd", (GetSerializer((in RawTransaction r) => r.FreezeAccount), GetDeserializer((ref RawTransaction r, Address value) => r.FreezeAccount = value))},
            {"faid", (GetSerializer((in RawTransaction r) => r.FreezeAsset), GetDeserializer((ref RawTransaction r, ulong value) => r.FreezeAsset = value))},
            {"fee", (GetSerializer((in RawTransaction r) => r.Fee), GetDeserializer((ref RawTransaction r, ulong value) => r.Fee = value))},
            {"fv", (GetSerializer((in RawTransaction r) => r.FirstValidRound), GetDeserializer((ref RawTransaction r, ulong value) => r.FirstValidRound = value))},
            {"gen", (GetSerializer((in RawTransaction r) => r.GenesisId), GetDeserializer((ref RawTransaction r, FixedString32 value) => r.GenesisId = value))},
            {"gh", (GetSerializer((in RawTransaction r) => r.GenesisHash), GetDeserializer((ref RawTransaction r, Sha512_256_Hash value) => r.GenesisHash = value))},
            {"grp", (GetSerializer((in RawTransaction r) => r.Group), GetDeserializer((ref RawTransaction r, Address value) => r.Group = value))},
            {"lv", (GetSerializer((in RawTransaction r) => r.LastValidRound), GetDeserializer((ref RawTransaction r, ulong value) => r.LastValidRound = value))},
            {"lx", (GetSerializer((in RawTransaction r) => r.Lease), GetDeserializer((ref RawTransaction r, Address value) => r.Lease = value))},
            {"nonpart", (GetSerializer((in RawTransaction r) => r.NonParticipation), GetDeserializer((ref RawTransaction r, NativeReference<bool> value) => r.NonParticipation = value))},
            {"note", (GetSerializer((in RawTransaction r) => r.Note), GetDeserializer((ref RawTransaction r, NativeText value) => r.Note = value))},
            {"rcv", (GetSerializer((in RawTransaction r) => r.Receiver), GetDeserializer((ref RawTransaction r, Address value) => r.Receiver = value))},
            {"rekey", (GetSerializer((in RawTransaction r) => r.RekeyTo), GetDeserializer((ref RawTransaction r, Address value) => r.RekeyTo = value))},
            {"selkey", (GetSerializer((in RawTransaction r) => r.SelectionPk), GetDeserializer((ref RawTransaction r, VrfPubkey value) => r.SelectionPk = value))},
            {"snd", (GetSerializer((in RawTransaction r) => r.Sender), GetDeserializer((ref RawTransaction r, Address value) => r.Sender = value))},
            {"type", (GetSerializer((in RawTransaction r) => r.TransactionType), GetDeserializer((ref RawTransaction r, TransactionType value) => r.TransactionType = value))},
            {"votefst", (GetSerializer((in RawTransaction r) => r.VoteFirst), GetDeserializer((ref RawTransaction r, ulong value) => r.VoteFirst = value))},
            {"votekd", (GetSerializer((in RawTransaction r) => r.VoteKeyDilution), GetDeserializer((ref RawTransaction r, ulong value) => r.VoteKeyDilution = value))},
            {"votekey", (GetSerializer((in RawTransaction r) => r.VotePk), GetDeserializer((ref RawTransaction r, Ed25519.PublicKey value) => r.VotePk = value))},
            {"votelst", (GetSerializer((in RawTransaction r) => r.VoteLast), GetDeserializer((ref RawTransaction r, ulong value) => r.VoteLast = value))},
            {"xaid", (GetSerializer((in RawTransaction r) => r.XferAsset), GetDeserializer((ref RawTransaction r, ulong value) => r.XferAsset = value))},
        };

        public static Deserializer GetDeserializer<T>(Setter<T> setter)
        {
            void deserializer(ref RawTransaction data, ref MessagePackReader reader, MessagePackSerializerOptions options)
            {
                var value = options.Resolver.GetFormatter<T>().Deserialize(ref reader, options);
                setter(ref data, value);
            }
            return deserializer;
        }

        public static Serializer GetSerializer<T>(Getter<T> getter)
        {
            void serializer(in RawTransaction data, ref MessagePackWriter writer, MessagePackSerializerOptions options)
            {
                options.Resolver.GetFormatter<T>().Serialize(ref writer, getter(in data), options);
            }
            return serializer;
        }

        public NativeList<FixedString32> GetFieldsToSerialize(Allocator allocator)
        {
            var list = new NativeList<FixedString32>(OrderedFields.Length, allocator);
            for (var i = 0; i < ShouldSerialize.Length; i++)
                if (ShouldSerialize[i](in this))
                    list.Add(OrderedFields[i]);
            return list;
        }

        public bool Equals(RawTransaction other)
        {
            return Fee.Equals(other.Fee)
                && FirstValidRound.Equals(other.FirstValidRound)
                && GenesisHash.Equals(other.GenesisHash)
                && LastValidRound.Equals(other.LastValidRound)
                && Sender.Equals(other.Sender)
                && TransactionType.Equals(other.TransactionType)
                && GenesisId.Equals(other.GenesisId)
                && Group.Equals(other.Group)
                && Lease.Equals(other.Lease)
                && Note.IsCreated == other.Note.IsCreated
                && (!Note.IsCreated || Note.Equals(other.Note))
                && RekeyTo.Equals(other.RekeyTo)
                && AssetAmount.Equals(other.AssetAmount)
                && AssetCloseTo.Equals(other.AssetCloseTo)
                && AssetFrozen.IsCreated == other.AssetFrozen.IsCreated
                && (!AssetFrozen.IsCreated || AssetFrozen.Equals(other.AssetFrozen))
                && Amount.Equals(other.Amount)
                && AppArguments.IsCreated == other.AppArguments.IsCreated
                && (!AppArguments.IsCreated || AppArguments.Equals(other.AppArguments))
                && OnComplete.Equals(other.OnComplete)
                && ApprovalProgram.IsCreated == other.ApprovalProgram.IsCreated
                && (!ApprovalProgram.IsCreated || ApprovalProgram.Equals(other.ApprovalProgram))
                && AssetParams.Equals(other.AssetParams)
                && ForeignAssets.IsCreated == other.ForeignAssets.IsCreated
                && (!ForeignAssets.IsCreated || ForeignAssets.Equals(other.ForeignAssets))
                && Accounts.IsCreated == other.Accounts.IsCreated
                && (!Accounts.IsCreated || Accounts.Equals(other.Accounts))
                && ExtraProgramPages.Equals(other.ExtraProgramPages)
                && ForeignApps.IsCreated == other.ForeignApps.IsCreated
                && (!ForeignApps.IsCreated || ForeignApps.Equals(other.ForeignApps))
                && GlobalStateSchema.Equals(other.GlobalStateSchema)
                && ApplicationId.Equals(other.ApplicationId)
                && LocalStateSchema.Equals(other.LocalStateSchema)
                && ClearStateProgram.IsCreated == other.ClearStateProgram.IsCreated
                && (!ClearStateProgram.IsCreated || ClearStateProgram.Equals(other.ClearStateProgram))
                && AssetReceiver.Equals(other.AssetReceiver)
                && AssetSender.Equals(other.AssetSender)
                && ConfigAsset.Equals(other.ConfigAsset)
                && CloseRemainderTo.Equals(other.CloseRemainderTo)
                && FreezeAccount.Equals(other.FreezeAccount)
                && FreezeAsset.Equals(other.FreezeAsset)
                && NonParticipation.IsCreated == other.NonParticipation.IsCreated
                && (!NonParticipation.IsCreated || NonParticipation.Equals(other.NonParticipation))
                && Receiver.Equals(other.Receiver)
                && SelectionPk.Equals(other.SelectionPk)
                && VoteFirst.Equals(other.VoteFirst)
                && VoteKeyDilution.Equals(other.VoteKeyDilution)
                && VoteLast.Equals(other.VoteLast)
                && XferAsset.Equals(other.XferAsset)
                ;
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

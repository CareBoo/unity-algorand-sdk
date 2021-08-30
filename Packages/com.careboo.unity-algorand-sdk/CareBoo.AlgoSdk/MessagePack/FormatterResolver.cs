using System;
using System.Collections.Generic;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack.Formatters;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Resolvers
{
    public class FormatterResolver : IFormatterResolver
    {
        public static FormatterResolver Instance = new FormatterResolver();

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                var rawFormatter = GetFormatter(typeof(T));
                if (rawFormatter == null)
                    throw new NullReferenceException($"{typeof(T)} has no formatter...");
                if (rawFormatter is IMessagePackFormatter<T> formatter)
                    Formatter = formatter;
                else
                    throw new InvalidCastException($"{rawFormatter.GetType()} is not an IMessagePackFormatter<{typeof(T)}>");
            }
        }

        private static readonly Dictionary<Type, object> formatterLookup = new Dictionary<Type, object>()
        {
            {typeof(ulong), UInt64Formatter.Instance},
            {typeof(bool), BooleanFormatter.Instance},
            {typeof(string), NullableStringFormatter.Instance},
            {typeof(Address), new AddressFormatter()},
            {typeof(GenesisHash), new GenesisHashFormatter()},
            {typeof(Sha512_256_Hash), ByteArrayFormatter<Sha512_256_Hash>.Instance},
            {typeof(Optional<bool>), new OptionalFormatter<bool>()},
            {typeof(Optional<ulong>), new OptionalFormatter<ulong>()},
            {typeof(Optional<ApplicationStateSchema>), new OptionalFormatter<ApplicationStateSchema>()},
            {typeof(Optional<Address>), new OptionalFormatter<Address>()},
            {typeof(Optional<AccountParticipation>), new OptionalFormatter<AccountParticipation>()},
            {typeof(TransactionType), new TransactionTypeFormatter()},
            {typeof(TransactionId), new TransactionIdFormatter()},
            {typeof(TealValueType), new GenericEnumFormatter<TealValueType>()},
            {typeof(EvalDeltaAction), new GenericEnumFormatter<EvalDeltaAction>()},
            {typeof(FixedString32Bytes), new FixedStringFormatter<FixedString32Bytes>()},
            {typeof(FixedString64Bytes), new FixedStringFormatter<FixedString64Bytes>()},
            {typeof(FixedString128Bytes), new FixedStringFormatter<FixedString128Bytes>()},
            {typeof(NativeText), new NativeTextFormatter()},
            {typeof(byte[]), ByteArrayFormatter.Instance},
            {typeof(ApplicationLocalState[]), new ArrayFormatter<ApplicationLocalState>()},
            {typeof(AssetHolding[]), new ArrayFormatter<AssetHolding>()},
            {typeof(Application[]), new ArrayFormatter<Application>()},
            {typeof(Asset[]), new ArrayFormatter<Asset>()},
            {typeof(PendingTransaction[]), new ArrayFormatter<PendingTransaction>()},
            {typeof(FixedString32Bytes[]), new ArrayFormatter<FixedString32Bytes>()},
            {typeof(FixedString64Bytes[]), new ArrayFormatter<FixedString32Bytes>()},
            {typeof(FixedString128Bytes[]), new ArrayFormatter<FixedString32Bytes>()},
            {typeof(DryrunTxnResult[]), new ArrayFormatter<DryrunTxnResult>()},
            {typeof(DryrunState[]), new ArrayFormatter<DryrunState>()},
            {typeof(AccountStateDelta[]), new ArrayFormatter<AccountStateDelta>()},
            {typeof(TealValue[]), new ArrayFormatter<TealValue>()},
            {typeof(EvalDeltaKeyValue[]), new ArrayFormatter<EvalDeltaKeyValue>()},
            {typeof(BlockTransaction[]), new ArrayFormatter<BlockTransaction>()},
            {typeof(VrfPubkey), ByteArrayFormatter<VrfPubkey>.Instance},
            {typeof(ITransaction), new TransactionFormatter()},
            {typeof(SignedTransaction<Transaction.Payment>), new SignedTransactionFormatter<SignedTransaction<Transaction.Payment>>()},
            {typeof(Signature), ByteArrayFormatter<Signature>.Instance},
            {typeof(ErrorResponse), new MessagePackObjectFormatter<ErrorResponse>()},
            {typeof(RawSignedTransaction), new MessagePackObjectFormatter<RawSignedTransaction>()},
            {typeof(RawTransaction), new MessagePackObjectFormatter<RawTransaction>()},
            {typeof(Account), new MessagePackObjectFormatter<Account>()},
            {typeof(AccountParticipation), new MessagePackObjectFormatter<AccountParticipation>()},
            {typeof(AccountStateDelta), new MessagePackObjectFormatter<AccountStateDelta>()},
            {typeof(Application), new MessagePackObjectFormatter<Application>()},
            {typeof(ApplicationLocalState), new MessagePackObjectFormatter<ApplicationLocalState>()},
            {typeof(ApplicationParams), new MessagePackObjectFormatter<ApplicationParams>()},
            {typeof(ApplicationStateSchema), new MessagePackObjectFormatter<ApplicationStateSchema>()},
            {typeof(Asset), new MessagePackObjectFormatter<Asset>()},
            {typeof(AssetHolding), new MessagePackObjectFormatter<AssetHolding>()},
            {typeof(AssetParams), new MessagePackObjectFormatter<AssetParams>()},
            {typeof(BuildVersion), new MessagePackObjectFormatter<BuildVersion>()},
            {typeof(CatchupMessage), new MessagePackObjectFormatter<CatchupMessage>()},
            {typeof(DryrunRequest), new MessagePackObjectFormatter<DryrunRequest>()},
            {typeof(DryrunSource), new MessagePackObjectFormatter<DryrunSource>()},
            {typeof(DryrunResults), new MessagePackObjectFormatter<DryrunResults>()},
            {typeof(DryrunTxnResult), new MessagePackObjectFormatter<DryrunTxnResult>()},
            {typeof(DryrunState), new MessagePackObjectFormatter<DryrunState>()},
            {typeof(TealValue), new TealValueFormatter()},
            {typeof(TealBytes), ByteArrayFormatter<TealBytes>.Instance},
            {typeof(PendingTransactions), new MessagePackObjectFormatter<PendingTransactions>()},
            {typeof(PendingTransaction), new MessagePackObjectFormatter<PendingTransaction>()},
            {typeof(Block), new MessagePackObjectFormatter<Block>()},
            {typeof(Block.Header), new MessagePackObjectFormatter<Block.Header>()},
            {typeof(BlockTransaction), new MessagePackObjectFormatter<BlockTransaction>()},
            {typeof(MerkleProof), new MessagePackObjectFormatter<MerkleProof>()},
            {typeof(LedgerSupply), new MessagePackObjectFormatter<LedgerSupply>()},
            {typeof(Status), new MessagePackObjectFormatter<Status>()},
            {typeof(Version), new MessagePackObjectFormatter<Version>()},
            {typeof(TransactionParams), new MessagePackObjectFormatter<TransactionParams>()},
            {typeof(EvalDeltaKeyValue), new MessagePackObjectFormatter<EvalDeltaKeyValue>()},
            {typeof(EvalDelta), new MessagePackObjectFormatter<EvalDelta>()},
        };

        private static object GetFormatter(Type t)
        {
            if (formatterLookup.TryGetValue(t, out var formatter))
                return formatter;
            return null;
        }
    }

    [Obsolete("TODO: Implement a formatter for the type, T")]
    public class TodoFormatter<T> : IMessagePackFormatter<T>
    {
        public T Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            reader.Skip();
            return default;
        }

        public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
        {
            writer.WriteNil();
        }
    }
}

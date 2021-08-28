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
            {typeof(Address), new AddressFormatter()},
            {typeof(Sha512_256_Hash), new ByteArrayFormatter<Sha512_256_Hash>()},
            {typeof(Optional<ulong>), new OptionalFormatter<ulong>()},
            {typeof(Optional<ApplicationStateSchema>), new OptionalFormatter<ApplicationStateSchema>()},
            {typeof(Optional<Address>), new OptionalFormatter<Address>()},
            {typeof(Optional<AccountParticipation>), new OptionalFormatter<AccountParticipation>()},
            {typeof(TransactionType), new TransactionTypeFormatter()},
            {typeof(FixedString32Bytes), new FixedStringFormatter<FixedString32Bytes>()},
            {typeof(FixedString64Bytes), new FixedStringFormatter<FixedString64Bytes>()},
            {typeof(FixedString128Bytes), new FixedStringFormatter<FixedString128Bytes>()},
            {typeof(ApplicationLocalState[]), new ArrayFormatter<ApplicationLocalState>()},
            {typeof(AssetHolding[]), new ArrayFormatter<AssetHolding>()},
            {typeof(Application[]), new ArrayFormatter<Application>()},
            {typeof(Asset[]), new ArrayFormatter<Asset>()},
            {typeof(PendingTransaction[]), new ArrayFormatter<PendingTransaction>()},
            {typeof(Ed25519.PublicKey), new ByteArrayFormatter<Ed25519.PublicKey>()},
            {typeof(VrfPubkey), new ByteArrayFormatter<VrfPubkey>()},
            {typeof(ITransaction), new TransactionFormatter()},
            {typeof(SignedTransaction<Transaction.Payment>), new SignedTransactionFormatter<SignedTransaction<Transaction.Payment>>()},
            {typeof(Signature), new ByteArrayFormatter<Signature>()},
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
            {typeof(PendingTransactions), new MessagePackObjectFormatter<PendingTransactions>()},
        };

        private static object GetFormatter(Type t)
        {
            if (formatterLookup.TryGetValue(t, out var formatter))
                return formatter;
            return null;
        }
    }
}

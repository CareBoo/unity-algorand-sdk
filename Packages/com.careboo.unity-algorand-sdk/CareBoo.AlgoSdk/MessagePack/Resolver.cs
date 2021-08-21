using System;
using System.Collections.Generic;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack.Formatters;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.MsgPack.Resolvers
{
    public class AlgoSdkMessagePackResolver : IFormatterResolver
    {
        public static AlgoSdkMessagePackResolver Instance = new AlgoSdkMessagePackResolver();

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                Formatter = (IMessagePackFormatter<T>)ResolverHelper.GetFormatter(typeof(T));
            }
        }
    }

    internal static class ResolverHelper
    {
        private static readonly Dictionary<Type, object> lookup = new Dictionary<Type, object>()
        {
            {typeof(ulong), UInt64Formatter.Instance},
            {typeof(bool), BooleanFormatter.Instance},
            {typeof(Address), new AddressFormatter()},
            {typeof(Sha512_256_Hash), new ByteArrayFormatter<Sha512_256_Hash>()},
            {typeof(Optional<>), typeof(OptionalFormatter<>)},
            {typeof(IMessagePackObject), typeof(MessagePackObjectFormatter<>)},
            {typeof(TransactionType), new TransactionTypeFormatter()},
            {typeof(FixedString32), new FixedStringFormatter<FixedString32>()},
            {typeof(FixedString64), new FixedStringFormatter<FixedString64>()},
            {typeof(FixedString128), new FixedStringFormatter<FixedString128>()},
            {typeof(NativeText), new NativeTextFormatter()},
            {typeof(UnsafeText), new UnsafeTextFormatter()},
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
        };

        internal static object GetFormatter(Type t)
        {
            if (lookup.TryGetValue(t, out var formatter))
                return formatter;
            return null;
        }
    }
}

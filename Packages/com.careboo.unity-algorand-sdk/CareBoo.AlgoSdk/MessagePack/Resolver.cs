using System;
using System.Collections.Generic;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack.Formatters;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Resolvers
{
    public class Resolver : IFormatterResolver
    {
        public static Resolver Instance = new Resolver();

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
            {typeof(Ed25519.PublicKey), new ByteArrayFormatter<Ed25519.PublicKey>()},
            {typeof(VrfPubkey), new ByteArrayFormatter<VrfPubkey>()},
            {typeof(ITransaction), new TransactionFormatter()},
            {typeof(SignedTransaction<Transaction.Payment>), new SignedTransactionFormatter<SignedTransaction<Transaction.Payment>>()},
            {typeof(Signature), new ByteArrayFormatter<Signature>()},
        };

        internal static object GetFormatter(Type t)
        {
            if (lookup.TryGetValue(t, out var formatter))
                return formatter;
            if (typeof(IMessagePackObject).IsAssignableFrom(t) && lookup.TryGetValue(typeof(IMessagePackObject), out var messagePackFormatter))
            {
                var messagePackFormatterType = (Type)messagePackFormatter;
                messagePackFormatterType = messagePackFormatterType.MakeGenericType(t);
                return Activator.CreateInstance(messagePackFormatterType);
            }
            if (t.IsGenericType && lookup.TryGetValue(t.GetGenericTypeDefinition(), out var genericFormatter))
            {
                var genericFormatterType = (Type)genericFormatter;
                genericFormatterType = genericFormatterType.MakeGenericType(t.GenericTypeArguments);
                return Activator.CreateInstance(genericFormatterType);
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AlgoSdk.Crypto;
using AlgoSdk.Formatters;
using Unity.Collections;

namespace AlgoSdk
{
    public static class AlgoApiFormatterCache<T>
    {
        public static readonly IAlgoApiFormatter<T> Formatter;

        static AlgoApiFormatterCache()
        {
            Formatter = AlgoApiFormatterLookup.GetFormatter<T>();
            if (Formatter == null)
                throw new NotImplementedException($"Formatter for type {typeof(T)} could not be found...");
        }
    }

    public partial class AlgoApiFormatterLookup
    {
        public const string AddFormatterMethodName = nameof(AddFormatter);

        static Dictionary<Type, object> lookup;

        static void InitLookup()
        {
            lookup = new Dictionary<Type, object>();
            AddFormatter<ulong>(new UInt64Formatter());
            AddFormatter<uint>(new UInt32Formatter());
            AddFormatter<ushort>(new UInt16Formatter());
            AddFormatter<byte>(new UInt8Formatter());
            AddFormatter<long>(new Int64Formatter());
            AddFormatter<int>(new Int32Formatter());
            AddFormatter<short>(new Int16Formatter());
            AddFormatter<sbyte>(new Int8Formatter());
            AddFormatter<bool>(new BoolFormatter());
            AddFormatter<string>(new StringFormatter());
            AddFormatter<Sha512_256_Hash>(ByteArrayFormatter<Sha512_256_Hash>.Instance);
            AddFormatter<Ed25519.PublicKey>(ByteArrayFormatter<Ed25519.PublicKey>.Instance);
            AddFormatter<byte[]>(new ByteArrayFormatter());
            AddFormatter<byte[][]>(ArrayFormatter<byte[]>.Instance);
            AddFormatter<FixedString32Bytes>(new FixedStringFormatter<FixedString32Bytes>());
            AddFormatter<FixedString64Bytes>(new FixedStringFormatter<FixedString64Bytes>());
            AddFormatter<FixedString128Bytes>(new FixedStringFormatter<FixedString128Bytes>());
            AddFormatter<FixedString512Bytes>(new FixedStringFormatter<FixedString512Bytes>());
            AddFormatter<FixedString4096Bytes>(new FixedStringFormatter<FixedString4096Bytes>());
            AddFormatter<FixedList32Bytes<byte>>(new FixedBytesFormatter<FixedList32Bytes<byte>>());
            AddFormatter<FixedList64Bytes<byte>>(new FixedBytesFormatter<FixedList64Bytes<byte>>());
            AddFormatter<FixedList128Bytes<byte>>(new FixedBytesFormatter<FixedList128Bytes<byte>>());
            AddFormatter<FixedList512Bytes<byte>>(new FixedBytesFormatter<FixedList512Bytes<byte>>());
            AddFormatter<FixedList4096Bytes<byte>>(new FixedBytesFormatter<FixedList4096Bytes<byte>>());
            AddFormatter<AlgoSdk.WalletConnect.WalletTransaction[]>(ArrayFormatter<AlgoSdk.WalletConnect.WalletTransaction>.Instance);
        }

        public static void AddFormatter(Type t, object formatter)
        {
            if (lookup.ContainsKey(t))
                return;
            lookup.Add(t, formatter);
        }

        public static void AddFormatter<T>(IAlgoApiFormatter<T> formatter)
        {
            var type = typeof(T);
            AddFormatter(type, formatter);
        }

        public static IAlgoApiFormatter<T> GetFormatter<T>()
        {
            if (lookup == null)
                InitLookup();

            var type = typeof(T);
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);

            if (lookup.TryGetValue(type, out var formatter)
                && formatter is IAlgoApiFormatter<T> algoApiFormatter)
            {
                return algoApiFormatter;
            }
            if (formatter == null)
                return null;

            throw new InvalidCastException($"formatter '{formatter.GetType().FullName}' cannot be cast to '{typeof(IAlgoApiFormatter<T>).FullName}'...");
        }
    }
}

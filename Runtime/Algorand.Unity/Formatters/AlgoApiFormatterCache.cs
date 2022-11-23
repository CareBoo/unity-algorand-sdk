using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Algorand.Unity.Crypto;
using Algorand.Unity.Experimental.Abi;
using Algorand.Unity.Formatters;
using Unity.Collections;

namespace Algorand.Unity
{
    public static class AlgoApiFormatterCache<T>
    {
        public static readonly IAlgoApiFormatter<T> Formatter;

        static AlgoApiFormatterCache()
        {
            Formatter = AlgoApiFormatterLookup.Get<T>();
            if (Formatter == null)
                throw new NotImplementedException($"Formatter for type {typeof(T)} could not be found...");
        }
    }

    public class AlgoApiFormatterLookup : Dictionary<Type, object>
    {
        public const string AddFormatterMethodName = nameof(Add);

        private static AlgoApiFormatterLookup Instance = new AlgoApiFormatterLookup();

        static AlgoApiFormatterLookup()
        {
            Add<byte[]>(new ByteArrayFormatter());
            Add<ulong>(new UInt64Formatter());
            Add<uint>(new UInt32Formatter());
            Add<ushort>(new UInt16Formatter());
            Add<byte>(new UInt8Formatter());
            Add<long>(new Int64Formatter());
            Add<int>(new Int32Formatter());
            Add<short>(new Int16Formatter());
            Add<sbyte>(new Int8Formatter());
            Add<bool>(new BoolFormatter());
            Add<string>(new StringFormatter());
            Add<Sha512_256_Hash>(ByteArrayFormatter<Sha512_256_Hash>.Instance);
            Add<Ed25519.PublicKey>(ByteArrayFormatter<Ed25519.PublicKey>.Instance);
            Add<FixedString32Bytes>(new FixedStringFormatter<FixedString32Bytes>());
            Add<FixedString64Bytes>(new FixedStringFormatter<FixedString64Bytes>());
            Add<FixedString128Bytes>(new FixedStringFormatter<FixedString128Bytes>());
            Add<FixedString512Bytes>(new FixedStringFormatter<FixedString512Bytes>());
            Add<FixedString4096Bytes>(new FixedStringFormatter<FixedString4096Bytes>());
            Add<FixedList32Bytes<byte>>(new FixedBytesFormatter<FixedList32Bytes<byte>>());
            Add<FixedList64Bytes<byte>>(new FixedBytesFormatter<FixedList64Bytes<byte>>());
            Add<FixedList128Bytes<byte>>(new FixedBytesFormatter<FixedList128Bytes<byte>>());
            Add<FixedList512Bytes<byte>>(new FixedBytesFormatter<FixedList512Bytes<byte>>());
            Add<FixedList4096Bytes<byte>>(new FixedBytesFormatter<FixedList4096Bytes<byte>>());
            Add<OnCompletion>(new OnCompletionFormatter());
            Add<AddressRole>(new AddressRoleFormatter());
            Add<DeltaAction>(new ByteEnumFormatter<DeltaAction>());
            Add<TransactionType>(new TransactionTypeFormatter());
            Add<TealValueType>(new ByteEnumFormatter<TealValueType>());
            Add<SignatureType>(new SignatureTypeFormatter());
            Add<HashType>(new HashTypeFormatter());
            Add<IAbiType>(new AbiType.Formatter());
        }

        public static void Add<T>(IAlgoApiFormatter<T> formatter)
        {
            if (!TryAdd<T>(formatter)) return;
            TryAdd<T[]>(ArrayFormatter<T>.Instance);
        }

        public static IAlgoApiFormatter<T> Get<T>()
        {
            var type = typeof(T);
            EnsureStaticConstructor(type);

            if (Instance.TryGetValue(type, out var formatter)
                && formatter is IAlgoApiFormatter<T> algoApiFormatter)
            {
                return algoApiFormatter;
            }
            if (formatter == null)
                return null;

            throw new InvalidCastException($"formatter '{formatter.GetType().FullName}' cannot be cast to '{typeof(IAlgoApiFormatter<T>).FullName}'...");
        }

        private static bool TryAdd<T>(IAlgoApiFormatter<T> formatter)
        {
            var type = typeof(T);
            if (Instance.ContainsKey(type)) return false;
            Instance.Add(type, formatter);
            return true;
        }

        private static void EnsureStaticConstructor(Type type)
        {
            if (type.IsArray)
                type = type.GetElementType();

            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }
}

using System.Collections.Generic;
using System;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public sealed class TransactionTypeFormatter : IAlgoApiFormatter<TransactionType>
    {
        private static readonly FixedString32Bytes[] typeToString = new FixedString32Bytes[(int)TransactionType.Count]
        {
            default,
            "pay",
            "keyreg",
            "axfer",
            "afrz",
            "acfg",
            "appl"
        };

        private static readonly Dictionary<FixedString32Bytes, TransactionType> nameToType = new Dictionary<FixedString32Bytes, TransactionType>()
        {
            {"pay", TransactionType.Payment},
            {"keyreg", TransactionType.KeyRegistration},
            {"axfer", TransactionType.AssetTransfer},
            {"afrz", TransactionType.AssetFreeze},
            {"acfg", TransactionType.AssetConfiguration},
            {"appl", TransactionType.ApplicationCall}
        };

        public TransactionType Deserialize(ref JsonReader reader)
        {
            if (reader.Peek() == JsonToken.Null)
            {
                reader.ReadNull();
                return TransactionType.None;
            }
            var t = new FixedString32Bytes();
            reader.ReadString(ref t);
            return nameToType.TryGetValue(t, out var transactionType)
                ? transactionType
                : throw new ArgumentException($"{t} is not a valid transaction type");
        }

        public TransactionType Deserialize(ref MessagePackReader reader)
        {
            if (reader.TryReadNil())
                return TransactionType.None;
            var t = new FixedString32Bytes();
            reader.ReadString(ref t);
            return nameToType.TryGetValue(t, out var transactionType)
                ? transactionType
                : throw new ArgumentException($"{t} is not a valid transaction type");
        }

        public void Serialize(ref JsonWriter writer, TransactionType value)
        {
            if (value == TransactionType.None)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteString(in typeToString[(int)value]);
        }

        public void Serialize(ref MessagePackWriter writer, TransactionType value)
        {
            if (value == TransactionType.None)
            {
                writer.WriteNil();
                return;
            }

            writer.WriteString(in typeToString[(int)value]);
        }
    }
}

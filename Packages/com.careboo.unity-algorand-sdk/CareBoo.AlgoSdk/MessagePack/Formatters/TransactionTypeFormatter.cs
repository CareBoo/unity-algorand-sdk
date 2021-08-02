using System.Collections.Generic;
using MessagePack;
using MessagePack.Formatters;
using System.Text;
using System;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class TransactionTypeFormatter : IMessagePackFormatter<TransactionType>
    {
        private static readonly byte[][] typeToBytes = new byte[(int)TransactionType.Count][]
        {
            null, // None
            Encoding.UTF8.GetBytes("pay"), // Payment
            Encoding.UTF8.GetBytes("keyreg"), // KeyRegistration
            Encoding.UTF8.GetBytes("axfer"), // AssetTransfer
            Encoding.UTF8.GetBytes("afrz"), // AssetFreeze
            Encoding.UTF8.GetBytes("acfg"), // AssetConfiguration
            Encoding.UTF8.GetBytes("appl") // ApplicationCall
        };

        private static readonly Dictionary<string, TransactionType> nameToType = new Dictionary<string, TransactionType>()
        {
            {"pay", TransactionType.Payment},
            {"keyreg", TransactionType.KeyRegistration},
            {"axfer", TransactionType.AssetTransfer},
            {"afrz", TransactionType.AssetFreeze},
            {"acfg", TransactionType.AssetConfiguration},
            {"appl", TransactionType.ApplicationCall}
        };

        public TransactionType Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil() || !nameToType.TryGetValue(reader.ReadString(), out var value))
                return TransactionType.None;
            return value;
        }

        public void Serialize(ref MessagePackWriter writer, TransactionType value, MessagePackSerializerOptions options)
        {
            if (value == TransactionType.None)
            {
                writer.WriteNil();
                return;
            }

            var bytes = new ReadOnlySpan<byte>(typeToBytes[(int)value]);
            writer.WriteStringHeader(bytes.Length);
            writer.WriteRaw(bytes);
        }
    }
}

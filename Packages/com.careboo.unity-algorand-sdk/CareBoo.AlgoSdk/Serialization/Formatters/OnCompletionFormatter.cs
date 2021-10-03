using System.Collections.Generic;
using System;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public sealed class OnCompletionFormatter : IAlgoApiFormatter<OnCompletion>
    {
        private static readonly FixedString32Bytes[] typeToString = new FixedString32Bytes[(int)OnCompletion.Count]
        {
            default,
            "noop",
            "optin",
            "closeout",
            "clear",
            "update",
            "delete"
        };

        private static readonly Dictionary<FixedString32Bytes, OnCompletion> nameToType = new Dictionary<FixedString32Bytes, OnCompletion>()
        {
            {"noop", OnCompletion.NoOp},
            {"optin", OnCompletion.OptIn},
            {"closeout", OnCompletion.CloseOut},
            {"clear", OnCompletion.Clear},
            {"update", OnCompletion.Update},
            {"delete", OnCompletion.Delete}
        };

        public OnCompletion Deserialize(ref JsonReader reader)
        {
            if (reader.Peek() == JsonToken.Null)
            {
                reader.ReadNull();
                return OnCompletion.None;
            }
            var t = new FixedString32Bytes();
            reader.ReadString(ref t);
            return nameToType.TryGetValue(t, out var onCompletion)
                ? onCompletion
                : throw new ArgumentException($"{t} is not a valid transaction type");
        }

        public OnCompletion Deserialize(ref MessagePackReader reader)
        {
            if (reader.TryReadNil())
                return OnCompletion.None;
            var t = new FixedString32Bytes();
            reader.ReadString(ref t);
            return nameToType.TryGetValue(t, out var onCompletion)
                ? onCompletion
                : throw new ArgumentException($"{t} is not a valid transaction type");
        }

        public void Serialize(ref JsonWriter writer, OnCompletion value)
        {
            if (value == OnCompletion.None)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteString(in typeToString[(int)value]);
        }

        public void Serialize(ref MessagePackWriter writer, OnCompletion value)
        {
            if (value == OnCompletion.None)
            {
                writer.WriteNil();
                return;
            }

            writer.WriteString(in typeToString[(int)value]);
        }
    }
}

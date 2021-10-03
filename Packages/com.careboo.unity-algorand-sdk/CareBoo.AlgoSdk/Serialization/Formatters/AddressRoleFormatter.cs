using System;
using System.Collections.Generic;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public class AddressRoleFormatter : IAlgoApiFormatter<AddressRole>
    {
        private static readonly FixedString32Bytes[] typeToString = new FixedString32Bytes[(int)AddressRole.Count]
        {
            default,
            "sender",
            "receiver",
            "freeze-target"
        };

        private static readonly Dictionary<FixedString32Bytes, AddressRole> nameToType = new Dictionary<FixedString32Bytes, AddressRole>()
        {
            {"sender", AddressRole.Sender},
            {"receiver", AddressRole.Receiver},
            {"freeze-target", AddressRole.FreezeTarget},
        };

        public AddressRole Deserialize(ref JsonReader reader)
        {
            if (reader.Peek() == JsonToken.Null)
            {
                reader.ReadNull();
                return AddressRole.None;
            }
            var t = new FixedString32Bytes();
            reader.ReadString(ref t);
            return nameToType.TryGetValue(t, out var addressRole)
                ? addressRole
                : throw new ArgumentException($"{t} is not a valid {nameof(AddressRole)} type");
        }

        public AddressRole Deserialize(ref MessagePackReader reader)
        {
            if (reader.TryReadNil())
                return AddressRole.None;
            var t = new FixedString32Bytes();
            reader.ReadString(ref t);
            return nameToType.TryGetValue(t, out var addressRole)
                ? addressRole
                : throw new ArgumentException($"{t} is not a valid {nameof(AddressRole)} type");
        }

        public void Serialize(ref JsonWriter writer, AddressRole value)
        {
            if (value == AddressRole.None)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteString(in typeToString[(int)value]);
        }

        public void Serialize(ref MessagePackWriter writer, AddressRole value)
        {
            if (value == AddressRole.None)
            {
                writer.WriteNil();
                return;
            }

            writer.WriteString(in typeToString[(int)value]);
        }
    }
}

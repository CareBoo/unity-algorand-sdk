using System;
using System.Collections.Generic;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.Formatters
{
    public class ByteEnumFormatter<T> : IAlgoApiFormatter<T>
        where T : Enum
    {
        private readonly Dictionary<FixedString32Bytes, T> stringToType;
        private readonly FixedString32Bytes[] typeToString;

        public ByteEnumFormatter() { }

        public ByteEnumFormatter(FixedString32Bytes[] typeToString)
        {
            this.typeToString = typeToString;
            if (typeToString == null || typeToString.Length < 1)
                throw new ArgumentNullException(nameof(typeToString));

            stringToType = new Dictionary<FixedString32Bytes, T>();
            for (var i = 1; i < typeToString.Length; i++)
            {
                stringToType[typeToString[i]] = UnsafeUtility.As<int, T>(ref i);
            }
        }

        public T Deserialize(ref JsonReader reader)
        {
            if (stringToType == null)
            {
                reader.ReadNumber(out byte val);
                return UnsafeUtility.As<byte, T>(ref val);
            }

            if (reader.Peek() == JsonToken.Null)
            {
                reader.ReadNull();
                byte nil = 0;
                return UnsafeUtility.As<byte, T>(ref nil);
            }
            var s = new FixedString32Bytes();
            reader.ReadString(ref s);
            return stringToType.TryGetValue(s, out var t)
                ? t
                : throw new ArgumentException($"{s} is not a valid name for {typeof(T)}");
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            var v = reader.ReadByte();
            return UnsafeUtility.As<byte, T>(ref v);
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            if (typeToString == null)
            {
                writer.WriteNumber(UnsafeUtility.As<T, byte>(ref value));
                return;
            }

            var b = UnsafeUtility.As<T, byte>(ref value);
            if (b == 0)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteString(typeToString[(int)UnsafeUtility.As<T, byte>(ref value)]);
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.Write(UnsafeUtility.As<T, byte>(ref value));
        }
    }
}

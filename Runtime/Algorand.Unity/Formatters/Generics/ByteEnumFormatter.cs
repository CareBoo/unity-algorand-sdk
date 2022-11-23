using System;
using System.Collections.Generic;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Formatters
{
    public abstract class KeywordByteEnumFormatter<T> : IAlgoApiFormatter<T>
        where T : Enum
    {
        private readonly Dictionary<string, T> stringToType;
        private readonly string[] typeToString;


        public KeywordByteEnumFormatter(string[] typeToString)
        {
            this.typeToString = typeToString ?? throw new ArgumentNullException(nameof(typeToString));
            if (typeToString.Length < 1)
                throw new ArgumentException("should have a length of at least 1", nameof(typeToString));

            stringToType = new Dictionary<string, T>();
            for (byte i = 1; i < typeToString.Length; i++)
            {
                stringToType[typeToString[i]] = UnsafeUtility.As<byte, T>(ref i);
            }
        }

        public virtual T Deserialize(ref JsonReader reader)
        {
            var token = reader.Peek();
            switch (token)
            {
                case JsonToken.Null:
                    reader.ReadNull();
                    byte nil = 0;
                    return UnsafeUtility.As<byte, T>(ref nil);
                case JsonToken.String:
                    reader.ReadString(out var s);
                    return stringToType.TryGetValue(s, out var t)
                        ? t
                        : throw new ArgumentException($"{s} is not a valid name for {typeof(T)}");
                case JsonToken.Number:
                    reader.ReadNumber(out byte b);
                    return UnsafeUtility.As<byte, T>(ref b);
                default:
                    throw new NotSupportedException($"Cannot deserialize enum with JsonToken {token}");
            }
        }

        public virtual T Deserialize(ref MessagePackReader reader)
        {
            if (reader.TryReadNil())
            {
                byte nil = 0;
                return UnsafeUtility.As<byte, T>(ref nil);
            }
            reader.ReadString(out var s);
            return stringToType.TryGetValue(s, out var t)
                ? t
                : throw new ArgumentException($"{s} is not a valid name for {typeof(T)}");
        }

        public virtual void Serialize(ref JsonWriter writer, T value)
        {
            var b = UnsafeUtility.As<T, byte>(ref value);
            if (b == 0)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteString(typeToString[b]);
        }

        public virtual void Serialize(ref MessagePackWriter writer, T value)
        {
            var b = UnsafeUtility.As<T, byte>(ref value);
            if (b == 0)
            {
                writer.WriteNil();
                return;
            }

            writer.WriteString(typeToString[b]);
        }
    }

    public sealed class ByteEnumFormatter<T> : IAlgoApiFormatter<T>
        where T : Enum
    {
        public T Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out byte b).ThrowIfError(reader);
            return UnsafeUtility.As<byte, T>(ref b);
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            var b = reader.ReadByte();
            return UnsafeUtility.As<byte, T>(ref b);
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            writer.WriteNumber(UnsafeUtility.As<T, byte>(ref value));
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            writer.Write(UnsafeUtility.As<T, byte>(ref value));
        }
    }
}

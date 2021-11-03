using System;
using System.Collections.Generic;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.Formatters
{
    public abstract class KeywordByteEnumFormatter<T> : IAlgoApiFormatter<T>
        where T : Enum
    {
        readonly Dictionary<string, T> stringToType;
        readonly string[] typeToString;


        public KeywordByteEnumFormatter(string[] typeToString)
        {
            this.typeToString = typeToString;
            if (typeToString == null || typeToString.Length < 1)
                throw new ArgumentNullException(nameof(typeToString));

            stringToType = new Dictionary<string, T>();
            for (var i = 1; i < typeToString.Length; i++)
            {
                stringToType[typeToString[i]] = UnsafeUtility.As<int, T>(ref i);
            }
        }

        public virtual T Deserialize(ref JsonReader reader)
        {
            if (reader.Peek() == JsonToken.Null)
            {
                reader.ReadNull();
                byte nil = 0;
                return UnsafeUtility.As<byte, T>(ref nil);
            }
            reader.ReadString(out var s);
            return stringToType.TryGetValue(s, out var t)
                ? t
                : throw new ArgumentException($"{s} is not a valid name for {typeof(T)}");
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
            var b = UnsafeUtility.As<T, int>(ref value);
            if (b == 0)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteString(typeToString[b]);
        }

        public virtual void Serialize(ref MessagePackWriter writer, T value)
        {
            var b = UnsafeUtility.As<T, int>(ref value);
            if (b == 0)
            {
                writer.WriteNil();
                return;
            }

            UnityEngine.Debug.Log(
                $"Debugging {nameof(KeywordByteEnumFormatter<T>)}.{nameof(Serialize)}:\n" +
                $"b: {b}\ns: {typeToString[b]}");
            writer.WriteString(typeToString[b]);
        }
    }

    public sealed class ByteEnumFormatter<T> : IAlgoApiFormatter<T>
        where T : Enum
    {
        public T Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out byte b);
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

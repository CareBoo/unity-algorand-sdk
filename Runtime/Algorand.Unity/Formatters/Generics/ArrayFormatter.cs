using System;
using System.Collections.Generic;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity.Formatters
{
    public class ArrayFormatter<T> : IAlgoApiFormatter<T[]>
    {
        public static ArrayFormatter<T> Instance = new ArrayFormatter<T>();

        public T[] Deserialize(ref JsonReader reader)
        {
            if (reader.TryReadNull())
                return null;

            var list = new List<T>();
            if (!reader.TryRead(JsonToken.ArrayBegin))
                JsonReadError.IncorrectType.ThrowIfError(reader);
            while (reader.Peek() != JsonToken.ArrayEnd && reader.Peek() != JsonToken.None)
            {
                var nextItem = AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
                list.Add(nextItem);
            }
            if (!reader.TryRead(JsonToken.ArrayEnd))
                JsonReadError.IncorrectFormat.ThrowIfError(reader);

            return list.ToArray();
        }

        public T[] Deserialize(ref MessagePackReader reader)
        {
            if (reader.TryReadNil())
                return null;

            var len = reader.ReadArrayHeader();
            if (len == 0)
                return Array.Empty<T>();

            var arr = new T[len];
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
            }
            return arr;
        }

        public void Serialize(ref JsonWriter writer, T[] value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.BeginArray();
            for (var i = 0; i < value.Length; i++)
            {
                if (i > 0)
                    writer.BeginNextItem();

                AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, value[i]);
            }
            writer.EndArray();
        }

        public void Serialize(ref MessagePackWriter writer, T[] value)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            writer.WriteArrayHeader(value.Length);
            for (var i = 0; i < value.Length; i++)
            {
                AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, value[i]);
            }
        }
    }
}

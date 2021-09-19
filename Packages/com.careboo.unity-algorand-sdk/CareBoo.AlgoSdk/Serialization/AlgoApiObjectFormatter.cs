using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public sealed class AlgoApiObjectFormatter<T> : IAlgoApiFormatter<T>
        where T : struct
    {
        readonly AlgoApiField<T>.Map fieldMap;

        public AlgoApiObjectFormatter(AlgoApiField<T>.Map fieldMap)
        {
            this.fieldMap = fieldMap;
        }

        public T Deserialize(ref JsonReader reader)
        {
            T result = default;
            if (!reader.TryRead(JsonToken.ObjectBegin))
                JsonReadError.IncorrectType.ThrowIfError();
            while (reader.Peek() != JsonToken.ObjectEnd && reader.Peek() != JsonToken.None)
            {
                FixedString64Bytes key = default;
                reader.ReadString(ref key).ThrowIfError();
                fieldMap.GetField(key).DeserializeJson(ref result, ref reader);
            }
            if (!reader.TryRead(JsonToken.ObjectEnd))
                JsonReadError.IncorrectFormat.ThrowIfError();
            return result;
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            T result = default;
            var length = reader.ReadMapHeader();
            for (var i = 0; i < length; i++)
            {
                FixedString64Bytes key = default;
                reader.ReadString(ref key);
                fieldMap.GetField(key).DeserializeMessagePack(ref result, ref reader);
            }
            return result;
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            using var fieldsToSerialize = fieldMap.GetFieldsToSerialize(value, Allocator.Temp);
            for (var i = 0; i < fieldsToSerialize.Length; i++)
            {
                if (i == 0)
                    writer.BeginObject();
                else
                    writer.BeginNextItem();
                var key = fieldsToSerialize[i];
                writer.WriteObjectKey(key);
                fieldMap.GetField(key).SerializeJson(value, ref writer);
            }
            writer.EndObject();
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            using var fieldsToSerialize = fieldMap.GetFieldsToSerialize(value, Allocator.Temp);
            writer.WriteMapHeader(fieldsToSerialize.Length);
            for (var i = 0; i < fieldsToSerialize.Length; i++)
            {
                var key = fieldsToSerialize[i];
                writer.WriteString(key);
                fieldMap.GetField(key).SerializeMessagePack(value, ref writer);
            }
        }
    }
}

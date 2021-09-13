using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;
using UnityEngine.Assertions;

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
            var token = reader.Read();
            while (token == JsonToken.ObjectBegin || token == JsonToken.Next)
            {
                FixedString64Bytes key = default;
                reader.ReadString(ref key);
                Assert.AreEqual(JsonToken.KeyValueSeparator, reader.Read());
                fieldMap[key].DeserializeJson(ref result, ref reader);
                token = reader.Read();
            }
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
                fieldMap[key].DeserializeMessagePack(ref result, ref reader);
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
                fieldMap[key].SerializeJson(ref value, ref writer);
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
                fieldMap[key].SerializeMessagePack(ref value, ref writer);
            }
        }
    }
}

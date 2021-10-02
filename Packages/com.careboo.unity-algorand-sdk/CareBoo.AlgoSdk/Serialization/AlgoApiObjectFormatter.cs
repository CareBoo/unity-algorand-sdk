using System;
using System.Collections.Generic;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public sealed class AlgoApiObjectFormatter<T> : IAlgoApiFormatter<T>
        where T : struct
    {
        readonly AlgoApiField<T>.Map<FixedString64Bytes> jsonFieldMap = new AlgoApiField<T>.Map<FixedString64Bytes>();
        readonly AlgoApiField<T>.Map<FixedString32Bytes> msgPackFieldMap = new AlgoApiField<T>.Map<FixedString32Bytes>();

        public AlgoApiObjectFormatter<T> Assign<TField>(
            string jsonKey,
            string messagePackKey,
            AlgoApiField<T>.FieldGetter<TField> getter,
            AlgoApiField<T>.FieldSetter<TField> setter,
            bool readOnly)
            where TField : IEquatable<TField>
        {
            if (!string.IsNullOrEmpty(jsonKey))
                jsonFieldMap.Assign(jsonKey, getter, setter, readOnly);
            if (!string.IsNullOrEmpty(messagePackKey))
                msgPackFieldMap.Assign(messagePackKey, getter, setter, readOnly);
            return this;
        }

        public AlgoApiObjectFormatter<T> Assign<TField>(
            string jsonKey,
            string messagePackKey,
            AlgoApiField<T>.FieldGetter<TField> getter,
            AlgoApiField<T>.FieldSetter<TField> setter,
            IEqualityComparer<TField> comparer,
            bool readOnly)
        {
            if (!string.IsNullOrEmpty(jsonKey))
                jsonFieldMap.Assign(jsonKey, getter, setter, comparer, readOnly);
            if (!string.IsNullOrEmpty(messagePackKey))
                msgPackFieldMap.Assign(messagePackKey, getter, setter, comparer, readOnly);
            return this;
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
                jsonFieldMap.GetField(key).DeserializeJson(ref result, ref reader);
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
                FixedString32Bytes key = default;
                reader.ReadString(ref key);
                msgPackFieldMap.GetField(key).DeserializeMessagePack(ref result, ref reader);
            }
            return result;
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            using var fieldsToSerialize = jsonFieldMap.GetFieldsToSerialize(value, Allocator.Temp);
            for (var i = 0; i < fieldsToSerialize.Length; i++)
            {
                if (i == 0)
                    writer.BeginObject();
                else
                    writer.BeginNextItem();
                var key = fieldsToSerialize[i];
                writer.WriteObjectKey(key);
                jsonFieldMap.GetField(key).SerializeJson(value, ref writer);
            }
            writer.EndObject();
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            using var fieldsToSerialize = msgPackFieldMap.GetFieldsToSerialize(value, Allocator.Temp);
            writer.WriteMapHeader(fieldsToSerialize.Length);
            for (var i = 0; i < fieldsToSerialize.Length; i++)
            {
                var key = fieldsToSerialize[i];
                writer.WriteString(key);
                msgPackFieldMap.GetField(key).SerializeMessagePack(value, ref writer);
            }
        }
    }
}

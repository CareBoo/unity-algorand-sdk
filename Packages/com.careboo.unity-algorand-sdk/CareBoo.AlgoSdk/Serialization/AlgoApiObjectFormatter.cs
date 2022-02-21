using System;
using System.Collections.Generic;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public class AlgoApiObjectFormatter<T> : IAlgoApiFormatter<T>
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
                JsonReadError.IncorrectType.ThrowIfError(reader.Char, reader.Position);
            while (reader.Peek() != JsonToken.ObjectEnd && reader.Peek() != JsonToken.None)
            {
                FixedString64Bytes key = default;
                reader.ReadString(ref key).ThrowIfError(reader.Char, reader.Position);
                try
                {
                    jsonFieldMap.GetField(key).DeserializeJson(ref result, ref reader);
                }
                catch (Exception ex)
                {
                    throw new SerializationException($"Got exception when deserializing \"{key}\" for type {typeof(T)}", ex);
                }
            }
            if (!reader.TryRead(JsonToken.ObjectEnd))
                JsonReadError.IncorrectFormat.ThrowIfError(reader.Char, reader.Position);
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
                try
                {
                    msgPackFieldMap.GetField(key).DeserializeMessagePack(ref result, ref reader);
                }
                catch (Exception ex)
                {
                    throw new SerializationException($"Got exception when deserializing \"{key}\" for type {typeof(T)}", ex);
                }
            }
            return result;
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            using var fieldsToSerialize = jsonFieldMap.GetFieldsToSerialize(value, Allocator.Temp);
            writer.BeginObject();
            for (var i = 0; i < fieldsToSerialize.Length; i++)
            {
                if (i > 0)
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

    public class AlgoApiObjectFormatter : IAlgoApiFormatter<AlgoApiObject>
    {
        public AlgoApiObject Deserialize(ref JsonReader reader)
        {
            var json = new NativeText(Allocator.Temp);
            try
            {
                reader.ReadRaw(ref json).ThrowIfError(reader.Char, reader.Position);
                return json;
            }
            finally
            {
                json.Dispose();
            }
        }

        public AlgoApiObject Deserialize(ref MessagePackReader reader)
        {
            var msgPack = new NativeList<byte>(Allocator.Temp);
            try
            {
                reader.ReadRaw(msgPack);
                return msgPack;
            }
            finally
            {
                msgPack.Dispose();
            }
        }

        public void Serialize(ref JsonWriter writer, AlgoApiObject value)
        {
            if (!value.IsJson || value.Json == null)
                throw new ArgumentException("cannot serialize non-json to json...", nameof(value));
            var json = new NativeText(value.Json.Length, Allocator.Temp);
            try
            {
                for (var i = 0; i < value.Json.Length; i++)
                {
                    json.AppendRawByte(value.Json[i]);
                }
                writer.WriteRaw(json);
            }
            finally
            {
                json.Dispose();
            }
        }

        public void Serialize(ref MessagePackWriter writer, AlgoApiObject value)
        {
            if (!value.IsMessagePack || value.MessagePack == null)
                throw new ArgumentException("cannot serialize non-msgpack to msgpack...", nameof(value));
            using var msgPack = new NativeArray<byte>(value.MessagePack, Allocator.Temp);
            writer.WriteRaw(msgPack);
        }
    }
}

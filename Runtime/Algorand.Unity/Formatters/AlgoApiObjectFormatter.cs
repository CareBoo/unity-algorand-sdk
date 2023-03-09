using System;
using System.Collections.Generic;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    public class AlgoApiObjectFormatter<T> : IAlgoApiFormatter<T>
        where T : struct
    {
        private readonly bool isStrict;
        private readonly AlgoApiField<T>.Map<FixedString64Bytes> fieldMap = new AlgoApiField<T>.Map<FixedString64Bytes>();

        public AlgoApiObjectFormatter(bool isStrict)
        {
            this.isStrict = isStrict;
        }

        public AlgoApiObjectFormatter<T> Assign<TField>(
            string key,
            AlgoApiField<T>.FieldGetter<TField> getter,
            AlgoApiField<T>.FieldSetter<TField> setter
        )
            where TField : IEquatable<TField>
        {
            if (!string.IsNullOrEmpty(key))
                fieldMap.Assign(key, getter, setter);
            return this;
        }

        public AlgoApiObjectFormatter<T> Assign<TField>(
            string key,
            AlgoApiField<T>.FieldGetter<TField> getter,
            AlgoApiField<T>.FieldSetter<TField> setter,
            IEqualityComparer<TField> comparer
        )
        {
            if (!string.IsNullOrEmpty(key))
                fieldMap.Assign(key, getter, setter, comparer);
            return this;
        }

        public T Deserialize(ref JsonReader reader)
        {
            T result = default;
            if (!reader.TryRead(JsonToken.ObjectBegin))
                JsonReadError.IncorrectType.ThrowIfError(reader);
            while (reader.Peek() != JsonToken.ObjectEnd && reader.Peek() != JsonToken.None)
            {
                FixedString64Bytes key = default;
                reader.ReadString(ref key).ThrowIfError(reader);
                try
                {
                    if (fieldMap.TryGetValue(key, out var field))
                    {
                        field.DeserializeJson(ref result, ref reader);
                    }
                    else
                    {
                        var obj = AlgoApiFormatterCache<AlgoApiObject>.Formatter.Deserialize(ref reader);
                        var msg = $"Could not recognize the field \"{key}\", for any fields on {typeof(T)}.\njson: {System.Text.Encoding.UTF8.GetString(obj.Json)}";
                        if (isStrict)
                            throw new KeyNotFoundException(msg);
                        else
                            Debug.LogWarning(msg);
                    }
                }
                catch (Exception ex)
                {
                    throw new SerializationException($"Got exception when deserializing \"{key}\" for type {typeof(T)}", ex);
                }
            }
            if (!reader.TryRead(JsonToken.ObjectEnd))
                JsonReadError.IncorrectFormat.ThrowIfError(reader);
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
                    if (fieldMap.TryGetValue(key, out var field))
                    {
                        field.DeserializeMessagePack(ref result, ref reader);
                    }
                    else
                    {
                        var obj = AlgoApiFormatterCache<AlgoApiObject>.Formatter.Deserialize(ref reader);
                        var msg = $"Could not recognize the msgpack field \"{key}\" for any fields on {typeof(T)}.\nmsgpack base64: {System.Convert.ToBase64String(obj.MessagePack)}";
                        if (isStrict)
                            throw new KeyNotFoundException(msg);
                        else
                            Debug.LogWarning(msg);
                    }
                }
                catch (Exception ex)
                {
                    throw new SerializationException($"Got exception when deserializing \"{key}\" for type {typeof(T)}.", ex);
                }
            }
            return result;
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            using var fieldsToSerialize = fieldMap.GetFieldsToSerialize(value, Allocator.Temp);
            writer.BeginObject();
            for (var i = 0; i < fieldsToSerialize.Length; i++)
            {
                if (i > 0)
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

    public class AlgoApiObjectFormatter : IAlgoApiFormatter<AlgoApiObject>
    {
        public AlgoApiObject Deserialize(ref JsonReader reader)
        {
            var json = new NativeText(Allocator.Temp);
            try
            {
                reader.ReadRaw(ref json).ThrowIfError(reader);
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
            using var json = new NativeArray<byte>(value.Json, Allocator.Temp);
            writer.WriteRaw(json);
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

using System;
using System.Collections.Generic;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{

    public class AlgoApiField<TAlgoApiObject>
        where TAlgoApiObject : struct
    {
        public delegate void MessagePackDeserializer(
            ref TAlgoApiObject obj,
            ref MessagePackReader reader);

        public delegate void JsonDeserializer(
            ref TAlgoApiObject obj,
            ref JsonReader reader);

        public delegate void MessagePackSerializer(
            TAlgoApiObject obj,
            ref MessagePackWriter writer);

        public delegate void JsonSerializer(
            TAlgoApiObject obj,
            ref JsonWriter writer);

        public delegate bool EqualityComparer(TAlgoApiObject messagePackObject, TAlgoApiObject other);

        public delegate bool SerializePredicate(TAlgoApiObject messagePackObject);

        public delegate T FieldGetter<T>(TAlgoApiObject messagePackObject);

        public delegate void FieldSetter<T>(ref TAlgoApiObject messagePackObject, T value);

        public readonly MessagePackDeserializer DeserializeMessagePack;
        public readonly MessagePackSerializer SerializeMessagePack;
        public readonly JsonDeserializer DeserializeJson;
        public readonly JsonSerializer SerializeJson;
        public readonly SerializePredicate ShouldSerialize;
        public readonly EqualityComparer FieldsEqual;

        public AlgoApiField(
            MessagePackDeserializer deserializeMessagePack,
            MessagePackSerializer serializeMessagePack,
            JsonDeserializer deserializeJson,
            JsonSerializer serializeJson,
            SerializePredicate shouldSerialize,
            EqualityComparer fieldsEqual)
        {
            DeserializeMessagePack = deserializeMessagePack;
            SerializeMessagePack = serializeMessagePack;
            DeserializeJson = deserializeJson;
            SerializeJson = serializeJson;
            FieldsEqual = fieldsEqual;
            ShouldSerialize = shouldSerialize;
        }

        public static AlgoApiField<TAlgoApiObject> Assign<T>(FieldGetter<T> getter, FieldSetter<T> setter)
            where T : IEquatable<T>
        {
            bool fieldsEqual(TAlgoApiObject messagePackObject, TAlgoApiObject other)
            {
                return getter(messagePackObject).Equals(getter(other));
            }
            bool shouldSerialize(TAlgoApiObject messagePackObject)
            {
                return !getter(messagePackObject).Equals(default);
            }
            return Assign(getter, setter, fieldsEqual, shouldSerialize);
        }

        public static AlgoApiField<TAlgoApiObject> Assign<T>(FieldGetter<T> getter, FieldSetter<T> setter, IEqualityComparer<T> comparer)
        {
            bool fieldsEqual(TAlgoApiObject messagePackObject, TAlgoApiObject other)
            {
                return comparer.Equals(getter(messagePackObject), getter(other));
            }
            bool shouldSerialize(TAlgoApiObject messagePackObject)
            {
                return !comparer.Equals(getter(messagePackObject), default);
            }
            return Assign(getter, setter, fieldsEqual, shouldSerialize);
        }

        public static AlgoApiField<TAlgoApiObject> Assign<T>(FieldGetter<T> getter, FieldSetter<T> setter, EqualityComparer fieldsEqual, SerializePredicate shouldSerialize)
        {
            void deserializeMessagePack(ref TAlgoApiObject obj, ref MessagePackReader reader)
            {
                setter(ref obj, AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader));
            }
            void deserializeJson(ref TAlgoApiObject obj, ref JsonReader reader)
            {
                setter(ref obj, AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader));
            }
            void serializeMessagePack(TAlgoApiObject obj, ref MessagePackWriter writer)
            {
                AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, getter(obj));
            }
            void serializeJson(TAlgoApiObject obj, ref JsonWriter writer)
            {
                AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, getter(obj));
            }
            return new AlgoApiField<TAlgoApiObject>(
                deserializeMessagePack,
                serializeMessagePack,
                deserializeJson,
                serializeJson,
                shouldSerialize,
                fieldsEqual);
        }

        public class Map : SortedDictionary<FixedString64Bytes, AlgoApiField<TAlgoApiObject>>
        {
            public Map Assign<T>(FixedString64Bytes key, FieldGetter<T> getter, FieldSetter<T> setter)
                where T : IEquatable<T>
            {
                Add(key, AlgoApiField<TAlgoApiObject>.Assign(getter, setter));
                return this;
            }

            public Map Assign<T>(FixedString64Bytes key, FieldGetter<T> getter, FieldSetter<T> setter, IEqualityComparer<T> comparer)
            {
                Add(key, AlgoApiField<TAlgoApiObject>.Assign(getter, setter, comparer));
                return this;
            }

            public AlgoApiField<TAlgoApiObject> GetField(FixedString64Bytes key)
            {
                if (TryGetValue(key, out var field))
                    return field;
                throw new KeyNotFoundException($"Could not find the key, \"{key}\", for any fields on {typeof(TAlgoApiObject)}");
            }

            public NativeList<FixedString64Bytes> GetFieldsToSerialize(TAlgoApiObject obj, Allocator allocator)
            {
                var list = new NativeList<FixedString64Bytes>(Count, allocator);
                var fieldEnum = GetEnumerator();
                while (fieldEnum.MoveNext())
                {
                    var kvp = fieldEnum.Current;
                    if (kvp.Value.ShouldSerialize(obj))
                        list.Add(kvp.Key);
                }
                return list;
            }
        }
    }
}

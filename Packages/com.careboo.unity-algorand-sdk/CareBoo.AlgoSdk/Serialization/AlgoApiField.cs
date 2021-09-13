using System;
using System.Collections.Generic;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{

    public class AlgoApiField<AlgoApiObject>
        where AlgoApiObject : struct
    {
        public delegate void MessagePackDeserializer(
            ref AlgoApiObject obj,
            ref MessagePackReader reader);

        public delegate void JsonDeserializer(
            ref AlgoApiObject obj,
            ref JsonReader reader);

        public delegate void MessagePackSerializer(
            ref AlgoApiObject obj,
            ref MessagePackWriter writer);

        public delegate void JsonSerializer(
            ref AlgoApiObject obj,
            ref JsonWriter writer);

        public delegate bool EqualityComparer(ref AlgoApiObject messagePackObject, ref AlgoApiObject other);

        public delegate bool SerializePredicate(ref AlgoApiObject messagePackObject);

        public delegate ref T FieldGetter<T>(ref AlgoApiObject messagePackObject);

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

        public static AlgoApiField<AlgoApiObject> Assign<T>(FieldGetter<T> field)
            where T : IEquatable<T>
        {
            bool fieldsEqual(ref AlgoApiObject messagePackObject, ref AlgoApiObject other)
            {
                return field(ref messagePackObject).Equals(field(ref other));
            }
            bool shouldSerialize(ref AlgoApiObject messagePackObject)
            {
                return !field(ref messagePackObject).Equals(default);
            }
            return Assign(field, fieldsEqual, shouldSerialize);
        }

        public static AlgoApiField<AlgoApiObject> Assign<T>(FieldGetter<T> field, IEqualityComparer<T> comparer)
        {
            bool fieldsEqual(ref AlgoApiObject messagePackObject, ref AlgoApiObject other)
            {
                return comparer.Equals(field(ref messagePackObject), field(ref other));
            }
            bool shouldSerialize(ref AlgoApiObject messagePackObject)
            {
                return !comparer.Equals(field(ref messagePackObject), default);
            }
            return Assign(field, fieldsEqual, shouldSerialize);
        }

        public static AlgoApiField<AlgoApiObject> Assign<T>(FieldGetter<T> field, EqualityComparer fieldsEqual, SerializePredicate shouldSerialize)
        {
            void deserializeMessagePack(ref AlgoApiObject obj, ref MessagePackReader reader)
            {
                field(ref obj) = AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
            }
            void deserializeJson(ref AlgoApiObject obj, ref JsonReader reader)
            {
                field(ref obj) = AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
            }
            void serializeMessagePack(ref AlgoApiObject obj, ref MessagePackWriter writer)
            {
                AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, field(ref obj));
            }
            void serializeJson(ref AlgoApiObject obj, ref JsonWriter writer)
            {
                AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, field(ref obj));
            }
            return new AlgoApiField<AlgoApiObject>(
                deserializeMessagePack,
                serializeMessagePack,
                deserializeJson,
                serializeJson,
                shouldSerialize,
                fieldsEqual);
        }

        public class Map : SortedDictionary<FixedString64Bytes, AlgoApiField<AlgoApiObject>>
        {
            public Map Assign<T>(FixedString64Bytes key, FieldGetter<T> field)
                where T : IEquatable<T>
            {
                Add(key, AlgoApiField<AlgoApiObject>.Assign(field));
                return this;
            }

            public Map Assign<T>(FixedString64Bytes key, FieldGetter<T> field, IEqualityComparer<T> comparer)
            {
                Add(key, AlgoApiField<AlgoApiObject>.Assign(field, comparer));
                return this;
            }

            public NativeList<FixedString64Bytes> GetFieldsToSerialize(AlgoApiObject obj, Allocator allocator)
            {
                var list = new NativeList<FixedString64Bytes>(Count, allocator);
                var fieldEnum = GetEnumerator();
                while (fieldEnum.MoveNext())
                {
                    var kvp = fieldEnum.Current;
                    if (kvp.Value.ShouldSerialize(ref obj))
                        list.Add(kvp.Key);
                }
                return list;
            }
        }
    }
}

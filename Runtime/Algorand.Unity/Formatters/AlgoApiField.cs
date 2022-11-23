using System;
using System.Collections.Generic;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity
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

        private static readonly SerializePredicate shouldSerializeReadOnly = _ => false;

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
                var field = getter(messagePackObject);
                return field.Equals(getter(other));
            }
            bool shouldSerialize(TAlgoApiObject messagePackObject)
            {
                var field = getter(messagePackObject);
                return !field.Equals(default);
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

        public class Map<TKey> : SortedDictionary<TKey, AlgoApiField<TAlgoApiObject>>
            where TKey : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            public Map<TKey> Assign<T>(TKey key, FieldGetter<T> getter, FieldSetter<T> setter)
                where T : IEquatable<T>
            {
                Add(key, AlgoApiField<TAlgoApiObject>.Assign(getter, setter));
                return this;
            }

            public Map<TKey> Assign<T>(TKey key, FieldGetter<T> getter, FieldSetter<T> setter, IEqualityComparer<T> comparer)
            {
                Add(key, AlgoApiField<TAlgoApiObject>.Assign(getter, setter, comparer));
                return this;
            }

            public AlgoApiField<TAlgoApiObject> GetField(TKey key)
            {
                if (TryGetValue(key, out var field))
                    return field;
                throw new KeyNotFoundException($"Could not find the key, \"{key}\", for any fields on {typeof(TAlgoApiObject)}");
            }

            public NativeList<TKey> GetFieldsToSerialize(TAlgoApiObject obj, Allocator allocator)
            {
                var list = new NativeList<TKey>(Count, allocator);
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

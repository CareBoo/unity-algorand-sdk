using System;
using System.Collections.Generic;
using MessagePack;

namespace AlgoSdk.MsgPack
{
    public class FieldFor<TMessagePackObject>
        where TMessagePackObject : struct
    {
        public delegate bool SerializePredicate(ref TMessagePackObject messagePackObject);

        public delegate void Deserializer(
            ref TMessagePackObject messagePackObject,
            ref MessagePackReader reader,
            MessagePackSerializerOptions options);

        public delegate void Serializer(
            ref TMessagePackObject messagePackObject,
            ref MessagePackWriter writer,
            MessagePackSerializerOptions options);

        public delegate ref Field<T> PropGetter<T>(ref TMessagePackObject messagePackObject);

        public delegate bool EqualityComparer(ref TMessagePackObject messagePackObject, ref TMessagePackObject other);

        public readonly Deserializer Deserialize;
        public readonly Serializer Serialize;
        public readonly SerializePredicate ShouldSerialize;
        public readonly EqualityComparer FieldsEqual;

        public FieldFor(Deserializer deserialize, Serializer serialize, SerializePredicate shouldSerialize, EqualityComparer fieldsEqual)
        {
            Deserialize = deserialize;
            Serialize = serialize;
            FieldsEqual = fieldsEqual;
            ShouldSerialize = shouldSerialize;
        }

        public static FieldFor<TMessagePackObject> Assign<T>(PropGetter<T> prop, EqualityComparer fieldsEqual)
        {
            void serialize(ref TMessagePackObject messagePackObject, ref MessagePackWriter writer, MessagePackSerializerOptions options)
            {
                options.Resolver.GetFormatter<T>().Serialize(ref writer, prop(ref messagePackObject).Get(), options);
            }
            void deserialize(ref TMessagePackObject messagePackObject, ref MessagePackReader reader, MessagePackSerializerOptions options)
            {
                prop(ref messagePackObject).Set(options.Resolver.GetFormatter<T>().Deserialize(ref reader, options));
            }
            bool shouldSerialize(ref TMessagePackObject messagePackObject)
            {
                return prop(ref messagePackObject).IsCreated;
            }
            return new FieldFor<TMessagePackObject>(deserialize, serialize, shouldSerialize, fieldsEqual);
        }

        public static FieldFor<TMessagePackObject> Assign<T>(PropGetter<T> prop) where T : IEquatable<T>
        {
            bool fieldsEqual(ref TMessagePackObject messagePackObject, ref TMessagePackObject other)
            {
                return prop(ref messagePackObject).Value.Equals(prop(ref other).Value);
            }
            return Assign(prop, fieldsEqual);
        }

        public static FieldFor<TMessagePackObject> Assign<T, TComparer>(PropGetter<T> prop, TComparer comparer)
            where TComparer : IEqualityComparer<T>
        {
            bool fieldsEqual(ref TMessagePackObject messagePackObject, ref TMessagePackObject other)
            {
                return comparer.Equals(prop(ref messagePackObject).Value, prop(ref other).Value);
            }
            return Assign(prop, fieldsEqual);
        }
    }

    public struct Field<T>
    {
        public T Value { get; private set; }
        public bool IsCreated { get; private set; }

        public Field(T value)
        {
            this.Value = value;
            IsCreated = true;
        }

        public T Get()
        {
            return Value;
        }

        public void Set(T value)
        {
            IsCreated = true;
            Value = value;
        }

        public static implicit operator T(Field<T> prop)
        {
            return prop.Value;
        }

        public static implicit operator Field<T>(T value)
        {
            return new Field<T>(value);
        }
    }
}

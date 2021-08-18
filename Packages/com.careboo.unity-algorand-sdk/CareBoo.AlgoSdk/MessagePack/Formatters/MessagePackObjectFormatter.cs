using System;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    public sealed class MessagePackObjectFormatter<TMessagePackObject>
        : IMessagePackFormatter<TMessagePackObject>
        where TMessagePackObject : struct, IMessagePackObject
    {
        public TMessagePackObject Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            TMessagePackObject result = default;
            var length = reader.ReadMapHeader();
            for (var i = 0; i < length; i++)
            {
                var key = options.Resolver.GetFormatter<FixedString64>().Deserialize(ref reader, options);
                try
                {
                    FieldCache<TMessagePackObject>.Map[key].Deserialize(ref result, ref reader, options);
                }
                catch (NullReferenceException nullRef)
                {
                    UnityEngine.Debug.LogError($"Encountered {nameof(NullReferenceException)} when trying to serialize \"{key}\"");
                    throw nullRef;
                }
            }
            return result;
        }

        public void Serialize(ref MessagePackWriter writer, TMessagePackObject value, MessagePackSerializerOptions options)
        {
            using var fieldsToSerialize = value.GetFieldsToSerialize(Allocator.Temp);
            writer.WriteMapHeader(fieldsToSerialize.Length);
            for (var i = 0; i < fieldsToSerialize.Length; i++)
            {
                var key = fieldsToSerialize[i];
                options.Resolver.GetFormatter<FixedString64>().Serialize(ref writer, key, options);
                try
                {
                    FieldCache<TMessagePackObject>.Map[key].Serialize(ref value, ref writer, options);
                }
                catch (NullReferenceException nullRef)
                {
                    UnityEngine.Debug.LogError($"Encountered {nameof(NullReferenceException)} when trying to serialize \"{key}\"");
                    throw nullRef;
                }
            }
        }
    }
}

using System;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;
using UnityEngine;

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
                var key = options.Resolver.GetFormatter<FixedString64Bytes>().Deserialize(ref reader, options);
                try
                {
                    if (FieldCache<TMessagePackObject>.Map.TryGetValue(key, out var field))
                    {
                        field.Deserialize(ref result, ref reader, options);
                    }
                    else
                    {
                        Debug.Log($"Given key: {key} not found in FieldCache<{typeof(TMessagePackObject)}>.Map");
                        reader.Skip();
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Encountered {ex.GetType().Name} when trying to deserialize \"{key}\"\n{ex}");
                    throw ex;
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
                options.Resolver.GetFormatter<FixedString64Bytes>().Serialize(ref writer, key, options);
                try
                {
                    FieldCache<TMessagePackObject>.Map[key].Serialize(ref value, ref writer, options);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Encountered {ex.GetType().Name} when trying to serialize \"{key}\"\n{ex}");
                    throw ex;
                }
            }
        }
    }
}

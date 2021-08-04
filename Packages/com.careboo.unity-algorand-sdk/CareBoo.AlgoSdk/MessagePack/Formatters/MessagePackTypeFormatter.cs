using System;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    public sealed class MessagePackTypeFormatter<TMessagePackObject>
        : IMessagePackFormatter<TMessagePackObject>
        where TMessagePackObject : struct, IMessagePackType<TMessagePackObject>
    {
        public TMessagePackObject Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            TMessagePackObject result = default;
            var length = reader.ReadMapHeader();
            for (var i = 0; i < length; i++)
            {
                var key = options.Resolver.GetFormatter<FixedString32>().Deserialize(ref reader, options);
                try
                {
                    result.MessagePackFields[key].Deserialize(ref result, ref reader, options);
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
                options.Resolver.GetFormatter<FixedString32>().Serialize(ref writer, key, options);
                try
                {
                    value.MessagePackFields[key].Serialize(ref value, ref writer, options);
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

using System.Collections.Generic;
using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    public interface IMessagePackType<TMessagePackObject>
        where TMessagePackObject : struct
    {
        SortedDictionary<FixedString32, Field<TMessagePackObject>> MessagePackFields { get; }
    }

    public static class MessagePackType
    {
        public static NativeList<FixedString32> GetFieldsToSerialize<TMessagePackObject>(this ref TMessagePackObject obj, Allocator allocator)
            where TMessagePackObject : struct, IMessagePackType<TMessagePackObject>
        {
            var list = new NativeList<FixedString32>(obj.MessagePackFields.Count, allocator);
            var fieldEnum = obj.MessagePackFields.GetEnumerator();
            while (fieldEnum.MoveNext())
            {
                var kvp = fieldEnum.Current;
                if (kvp.Value.ShouldSerialize(ref obj))
                    list.Add(kvp.Key);
            }
            return list;
        }

        public static bool Equals<TMessagePackObject>(this ref TMessagePackObject obj, ref TMessagePackObject other)
            where TMessagePackObject : struct, IMessagePackType<TMessagePackObject>
        {
            using var thisFields = obj.GetFieldsToSerialize(Allocator.Temp);
            using var otherFields = other.GetFieldsToSerialize(Allocator.Temp);
            if (thisFields.Length != otherFields.Length)
                return false;
            for (var i = 0; i < thisFields.Length; i++)
            {
                if (thisFields[i] != otherFields[i])
                    return false;

                var fieldName = thisFields[i];
                var field = obj.MessagePackFields[fieldName];
                if (!field.FieldsEqual(ref obj, ref other))
                    return false;
            }
            return true;
        }
    }
}

using System.Collections.Generic;
using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    public interface IMessagePackType<TMessagePackObject>
        where TMessagePackObject : struct
    {
        Dictionary<FixedString32, Field<TMessagePackObject>> MessagePackFields { get; }
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
            list.Sort();
            return list;
        }
    }
}

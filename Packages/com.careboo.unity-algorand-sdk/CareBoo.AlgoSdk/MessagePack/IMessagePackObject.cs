using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    public interface IMessagePackObject { }

    public static class MessagePackObject
    {
        public static NativeList<FixedString32> GetFieldsToSerialize<TMessagePackObject>(this ref TMessagePackObject obj, Allocator allocator)
            where TMessagePackObject : struct, IMessagePackObject
        {
            var list = new NativeList<FixedString32>(FieldCache<TMessagePackObject>.Map.Count, allocator);
            var fieldEnum = FieldCache<TMessagePackObject>.Map.GetEnumerator();
            while (fieldEnum.MoveNext())
            {
                var kvp = fieldEnum.Current;
                if (kvp.Value.ShouldSerialize(ref obj))
                    list.Add(kvp.Key);
            }
            return list;
        }

        public static bool Equals<TMessagePackObject>(this ref TMessagePackObject obj, ref TMessagePackObject other)
            where TMessagePackObject : struct, IMessagePackObject
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
                var field = FieldCache<TMessagePackObject>.Map[fieldName];
                if (!field.FieldsEqual(ref obj, ref other))
                    return false;
            }
            return true;
        }
    }
}

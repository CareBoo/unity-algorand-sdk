using System.Collections.Generic;
using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    public interface IMessagePackType<TMessagePackObject>
        where TMessagePackObject : struct
    {
        NativeList<FixedString32> GetFieldsToSerialize(Allocator allocator);

        Dictionary<FixedString32, Field<TMessagePackObject>> MessagePackFields { get; }
    }
}

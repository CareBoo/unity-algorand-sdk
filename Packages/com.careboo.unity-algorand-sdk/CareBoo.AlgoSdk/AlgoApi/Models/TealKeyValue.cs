using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct TealKeyValue
        : IMessagePackObject
        , IEquatable<TealKeyValue>
    {
        public FixedString128Bytes Key;
        public TealValue Value;

        public bool Equals(TealKeyValue other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<TealKeyValue>.Map tealKeyValueFields =
            new Field<TealKeyValue>.Map()
                .Assign("key", (ref TealKeyValue x) => ref x.Key)
                .Assign("value", (ref TealKeyValue x) => ref x.Value)
                ;
    }
}

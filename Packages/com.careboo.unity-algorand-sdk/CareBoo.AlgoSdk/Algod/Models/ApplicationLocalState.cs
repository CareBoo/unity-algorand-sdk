using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct ApplicationLocalState
        : IMessagePackObject
        , IEquatable<ApplicationLocalState>
    {
        public ulong Id;
        public TealKeyValue[] KeyValues;
        public ApplicationStateSchema Schema;

        public bool Equals(ApplicationLocalState other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<ApplicationLocalState>.Map applicationLocalStateFields =
            new Field<ApplicationLocalState>.Map()
                .Assign("id", (ref ApplicationLocalState x) => ref x.Id)
                .Assign("key-value", (ref ApplicationLocalState x) => ref x.KeyValues, ArrayComparer<TealKeyValue>.Instance)
                .Assign("schema", (ref ApplicationLocalState x) => ref x.Schema)
                ;
    }
}

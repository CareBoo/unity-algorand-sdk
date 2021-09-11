using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationLocalState
        : IMessagePackObject
        , IEquatable<ApplicationLocalState>
    {
        [AlgoApiKey("id")]
        public ulong Id;

        [AlgoApiKey("key-value")]
        public TealKeyValue[] KeyValues;

        [AlgoApiKey("schema")]
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

using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountStateDelta
        : IMessagePackObject
        , IEquatable<AccountStateDelta>
    {
        [AlgoApiKey("address")]
        public Address Address;

        [AlgoApiKey("delta")]
        public EvalDeltaKeyValue[] Delta;

        public bool Equals(AccountStateDelta other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<AccountStateDelta>.Map accountStateDeltaFields =
            new Field<AccountStateDelta>.Map()
                .Assign("address", (ref AccountStateDelta x) => ref x.Address)
                .Assign("delta", (ref AccountStateDelta x) => ref x.Delta, ArrayComparer<EvalDeltaKeyValue>.Instance)
                ;
    }
}

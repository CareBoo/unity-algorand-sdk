using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Application
        : IMessagePackObject
        , IEquatable<Application>
    {
        [AlgoApiKey("id")]
        public ulong Id;

        [AlgoApiKey("params")]
        public ApplicationParams Params;

        public bool Equals(Application other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal readonly static Field<Application>.Map applicationFields =
            new Field<Application>.Map()
                .Assign("id", (ref Application x) => ref x.Id)
                .Assign("params", (ref Application x) => ref x.Params)
                ;
    }
}

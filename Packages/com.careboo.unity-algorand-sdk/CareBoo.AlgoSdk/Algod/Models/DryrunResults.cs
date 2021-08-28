using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct DryrunResults
        : IMessagePackObject
        , IEquatable<DryrunResults>
    {
        public string Error;
        public FixedString128Bytes ProtocolVersion;
        public DryrunTxnResult[] Txns;

        public bool Equals(DryrunResults other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<DryrunResults>.Map dryrunResultsFields =
            new Field<DryrunResults>.Map()
                .Assign("", (ref DryrunResults x) => ref x.Error, StringComparer.Instance)
                .Assign("", (ref DryrunResults x) => ref x.ProtocolVersion)
                .Assign("", (ref DryrunResults x) => ref x.Txns, ArrayComparer<DryrunTxnResult>.Instance)
                ;
    }
}

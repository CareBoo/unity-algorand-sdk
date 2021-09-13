using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunResults
        : IMessagePackObject
        , IEquatable<DryrunResults>
    {
        [AlgoApiKey("error")]
        public string Error;
        [AlgoApiKey("protocol-version")]
        public FixedString128Bytes ProtocolVersion;
        [AlgoApiKey("txns")]
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
                .Assign("error", (ref DryrunResults x) => ref x.Error, StringComparer.Instance)
                .Assign("protocol-version", (ref DryrunResults x) => ref x.ProtocolVersion)
                .Assign("txns", (ref DryrunResults x) => ref x.Txns, ArrayComparer<DryrunTxnResult>.Instance)
                ;
    }
}

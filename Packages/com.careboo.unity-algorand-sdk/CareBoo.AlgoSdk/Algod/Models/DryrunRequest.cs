using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct DryrunRequest
        : IEquatable<DryrunRequest>
        , IMessagePackObject
    {
        public Account[] Accounts;
        public Application[] Applications;
        public ulong LatestTimestamp;
        public FixedString64 ProtocolVersion;
        public ulong Round;
        public DryrunSource[] Sources;
        public RawTransaction[] Transactions;

        public bool Equals(DryrunRequest other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<DryrunRequest>.Map dryrunRequestFields =
            new Field<DryrunRequest>.Map()
                .Assign("accounts", (ref DryrunRequest x) => ref x.Accounts, ArrayComparer<Account>.Instance)
                .Assign("apps", (ref DryrunRequest x) => ref x.Applications, ArrayComparer<Application>.Instance)
                .Assign("latest-timestamp", (ref DryrunRequest x) => ref x.LatestTimestamp)
                .Assign("protocol-version", (ref DryrunRequest x) => ref x.ProtocolVersion)
                .Assign("round", (ref DryrunRequest x) => ref x.Round)
                .Assign("sources", (ref DryrunRequest x) => ref x.Sources, ArrayComparer<DryrunSource>.Instance)
                .Assign("txns", (ref DryrunRequest x) => ref x.Transactions, ArrayComparer<RawTransaction>.Instance)
                ;
    }
}

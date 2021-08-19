using System;
using AlgoSdk.MsgPack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk
{
    public struct DryrunRequest
        : IEquatable<DryrunRequest>
        , IMessagePackObject
    {
        public UnsafeList<Account> Accounts;
        public UnsafeList<Application> Applications;
        public ulong LatestTimestamp;
        public FixedString64 ProtocolVersion;
        public ulong Round;
        public UnsafeList<DryrunSource> Sources;
        public UnsafeList<RawTransaction> Transactions;

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
                .Assign("accounts", (ref DryrunRequest x) => ref x.Accounts, default(UnsafeListComparer<Account>))
                .Assign("apps", (ref DryrunRequest x) => ref x.Applications, default(UnsafeListComparer<Application>))
                .Assign("latest-timestamp", (ref DryrunRequest x) => ref x.LatestTimestamp)
                .Assign("protocol-version", (ref DryrunRequest x) => ref x.ProtocolVersion)
                .Assign("round", (ref DryrunRequest x) => ref x.Round)
                .Assign("sources", (ref DryrunRequest x) => ref x.Sources, default(UnsafeListComparer<DryrunSource>))
                .Assign("txns", (ref DryrunRequest x) => ref x.Transactions, default(UnsafeListComparer<RawTransaction>))
                ;
    }
}

using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AssetsQuery
        : IEquatable<AssetsQuery>
    {
        [AlgoApiField("asset-id", null)]
        public ulong AssetId;

        [AlgoApiField("creator", null)]
        public Address Creator;

        [AlgoApiField("include-all", null)]
        public Optional<bool> IncludeAll;

        [AlgoApiField("limit", null)]
        public ulong Limit;

        [AlgoApiField("name", null)]
        public FixedString128Bytes Name;

        [AlgoApiField("next", null)]
        public FixedString128Bytes Next;

        [AlgoApiField("unit", null)]
        public FixedString128Bytes Unit;

        public bool Equals(AssetsQuery other)
        {
            return AssetId.Equals(other.AssetId)
                && Creator.Equals(other.Creator)
                && IncludeAll.Equals(other.IncludeAll)
                && Limit.Equals(other.Limit)
                && Name.Equals(other.Name)
                && Next.Equals(other.Next)
                && Unit.Equals(other.Unit)
                ;
        }
    }
}

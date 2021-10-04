using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AssetQuery
        : IEquatable<AssetQuery>
    {
        [AlgoApiField("include-all", null)]
        public Optional<bool> IncludeAll;

        public bool Equals(AssetQuery other)
        {
            return IncludeAll.Equals(other.IncludeAll);
        }
    }
}

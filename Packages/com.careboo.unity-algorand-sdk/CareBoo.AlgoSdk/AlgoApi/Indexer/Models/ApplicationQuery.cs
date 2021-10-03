using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationQuery
        : IEquatable<ApplicationQuery>
    {
        [AlgoApiField("include-all", null)]
        public Optional<bool> IncludeAll;

        public bool Equals(ApplicationQuery other)
        {
            return IncludeAll.Equals(other.IncludeAll);
        }
    }
}

using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct VersionsRequest
        : IEquatable<VersionsRequest>
    {
        public bool Equals(VersionsRequest other)
        {
            return true;
        }
    }
}

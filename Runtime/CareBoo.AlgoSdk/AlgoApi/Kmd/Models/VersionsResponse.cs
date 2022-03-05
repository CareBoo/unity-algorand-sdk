using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct VersionsResponse
        : IEquatable<VersionsResponse>
    {
        [AlgoApiField("versions", null)]
        public FixedString64Bytes[] Versions;

        public bool Equals(VersionsResponse other)
        {
            return ArrayComparer.Equals(Versions, other.Versions);
        }
    }
}

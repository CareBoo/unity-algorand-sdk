using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BuildVersion
        : IEquatable<BuildVersion>
    {
        [AlgoApiKey("branch")]
        public FixedString64Bytes Branch;
        [AlgoApiKey("build_number")]
        public ulong BuildNumber;
        [AlgoApiKey("channel")]
        public FixedString64Bytes Channel;
        [AlgoApiKey("commit_hash")]
        public FixedString128Bytes CommitHash;
        [AlgoApiKey("major")]
        public ulong Major;
        [AlgoApiKey("minor")]
        public ulong Minor;

        public bool Equals(BuildVersion other)
        {
            return CommitHash.Equals(other.CommitHash);
        }
    }
}

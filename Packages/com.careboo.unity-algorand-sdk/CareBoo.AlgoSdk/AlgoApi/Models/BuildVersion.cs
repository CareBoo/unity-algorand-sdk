using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BuildVersion
        : IEquatable<BuildVersion>
    {
        [AlgoApiKey("branch", null)]
        public FixedString64Bytes Branch;
        [AlgoApiKey("build_number", null)]
        public ulong BuildNumber;
        [AlgoApiKey("channel", null)]
        public FixedString64Bytes Channel;
        [AlgoApiKey("commit_hash", null)]
        public FixedString128Bytes CommitHash;
        [AlgoApiKey("major", null)]
        public ulong Major;
        [AlgoApiKey("minor", null)]
        public ulong Minor;

        public bool Equals(BuildVersion other)
        {
            return CommitHash.Equals(other.CommitHash);
        }
    }
}

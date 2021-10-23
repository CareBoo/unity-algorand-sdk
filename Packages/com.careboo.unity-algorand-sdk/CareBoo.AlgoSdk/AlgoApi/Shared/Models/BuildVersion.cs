using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// Information regarding the algod service's build version.
    /// </summary>
    [AlgoApiObject]
    public struct BuildVersion
        : IEquatable<BuildVersion>
    {
        [AlgoApiField("branch", null)]
        public FixedString64Bytes Branch;

        [AlgoApiField("build_number", null)]
        public ulong BuildNumber;

        [AlgoApiField("channel", null)]
        public FixedString64Bytes Channel;

        [AlgoApiField("commit_hash", null)]
        public FixedString128Bytes CommitHash;

        [AlgoApiField("major", null)]
        public ulong Major;

        [AlgoApiField("minor", null)]
        public ulong Minor;

        public bool Equals(BuildVersion other)
        {
            return CommitHash.Equals(other.CommitHash);
        }
    }
}

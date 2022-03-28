using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// Information regarding the algod service's build version.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct BuildVersion
        : IEquatable<BuildVersion>
    {
        [AlgoApiField("branch")]
        public FixedString64Bytes Branch;

        [AlgoApiField("build_number")]
        public ulong BuildNumber;

        [AlgoApiField("channel")]
        public FixedString64Bytes Channel;

        [AlgoApiField("commit_hash")]
        public FixedString128Bytes CommitHash;

        [AlgoApiField("major")]
        public ulong Major;

        [AlgoApiField("minor")]
        public ulong Minor;

        public bool Equals(BuildVersion other)
        {
            return CommitHash.Equals(other.CommitHash);
        }
    }
}

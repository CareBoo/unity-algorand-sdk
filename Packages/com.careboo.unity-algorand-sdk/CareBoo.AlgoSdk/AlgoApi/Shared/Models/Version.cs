using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// algod version information
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public struct Version
        : IEquatable<Version>
    {
        /// <summary>
        /// See <see cref="BuildVersion"/>
        /// </summary>
        [AlgoApiField("build", null)]
        public BuildVersion Build;

        [AlgoApiField("genesis_hash_b64", null)]
        public FixedString64Bytes GenesisHashBase64;

        [AlgoApiField("genesis_id", null)]
        public FixedString32Bytes GenesisId;

        [AlgoApiField("versions", null)]
        public FixedString32Bytes[] Versions;

        public bool Equals(Version other)
        {
            return Build.Equals(other.Build)
                && GenesisHashBase64.Equals(other.GenesisHashBase64)
                && GenesisId.Equals(other.GenesisId)
                && ArrayComparer.Equals(Versions, other.Versions)
                ;
        }
    }
}

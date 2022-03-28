using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// algod version information
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct Version
        : IEquatable<Version>
    {
        /// <summary>
        /// See <see cref="BuildVersion"/>
        /// </summary>
        [AlgoApiField("build")]
        public BuildVersion Build;

        [AlgoApiField("genesis_hash_b64")]
        public FixedString64Bytes GenesisHashBase64;

        [AlgoApiField("genesis_id")]
        public FixedString32Bytes GenesisId;

        [AlgoApiField("versions")]
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

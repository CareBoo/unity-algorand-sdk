using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Version
        : IEquatable<Version>
    {
        [AlgoApiKey("build")]
        public BuildVersion Build;
        [AlgoApiKey("genesis_hash_b64")]
        public FixedString64Bytes GenesisHashBase64;
        [AlgoApiKey("genesis_id")]
        public FixedString32Bytes GenesisId;
        [AlgoApiKey("versions")]
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

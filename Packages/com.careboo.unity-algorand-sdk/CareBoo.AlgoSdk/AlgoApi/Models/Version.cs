using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Version
        : IMessagePackObject
        , IEquatable<Version>
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
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<Version>.Map versionFields =
            new Field<Version>.Map()
                .Assign("build", (ref Version x) => ref x.Build)
                .Assign("genesis_hash_b64", (ref Version x) => ref x.GenesisHashBase64)
                .Assign("genesis_id", (ref Version x) => ref x.GenesisId)
                .Assign("versions", (ref Version x) => ref x.Versions, ArrayComparer<FixedString32Bytes>.Instance)
                ;
    }
}

using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BuildVersion
        : IMessagePackObject
        , IEquatable<BuildVersion>
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
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal readonly static Field<BuildVersion>.Map buildVersionFields =
            new Field<BuildVersion>.Map()
                .Assign("branch", (ref BuildVersion x) => ref x.Branch)
                .Assign("build_number", (ref BuildVersion x) => ref x.BuildNumber)
                .Assign("channel", (ref BuildVersion x) => ref x.Channel)
                .Assign("commit_hash", (ref BuildVersion x) => ref x.CommitHash)
                .Assign("major", (ref BuildVersion x) => ref x.Major)
                .Assign("minor", (ref BuildVersion x) => ref x.Minor)
                ;
    }
}

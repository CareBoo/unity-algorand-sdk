using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct BuildVersion
        : IMessagePackObject
        , IEquatable<BuildVersion>
    {
        public FixedString64Bytes Branch;
        public ulong BuildNumber;
        public FixedString64Bytes Channel;
        public FixedString128Bytes CommitHash;
        public ulong Major;
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

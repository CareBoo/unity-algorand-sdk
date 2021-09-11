using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct DryrunState
        : IMessagePackObject
        , IEquatable<DryrunState>
    {
        [AlgoApiKey("error")]
        public FixedString128Bytes Error;
        [AlgoApiKey("line")]
        public ulong Line;
        [AlgoApiKey("pc")]
        public ulong ProgramCounter;
        [AlgoApiKey("scratch")]
        public TealValue[] Scratch;
        [AlgoApiKey("stack")]
        public TealValue[] Stack;

        public bool Equals(DryrunState other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<DryrunState>.Map dryrunStateFields =
            new Field<DryrunState>.Map()
                .Assign("error", (ref DryrunState x) => ref x.Error)
                .Assign("line", (ref DryrunState x) => ref x.Line)
                .Assign("pc", (ref DryrunState x) => ref x.ProgramCounter)
                .Assign("scratch", (ref DryrunState x) => ref x.Scratch, ArrayComparer<TealValue>.Instance)
                .Assign("stack", (ref DryrunState x) => ref x.Stack, ArrayComparer<TealValue>.Instance)
                ;
    }
}

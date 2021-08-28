using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct DryrunState
        : IMessagePackObject
        , IEquatable<DryrunState>
    {
        public FixedString128Bytes Error;
        public ulong Line;
        public ulong ProgramCounter;
        public TealValue[] Scratch;
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

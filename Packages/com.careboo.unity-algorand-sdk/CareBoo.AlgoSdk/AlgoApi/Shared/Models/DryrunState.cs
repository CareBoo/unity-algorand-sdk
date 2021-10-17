using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunState
        : IEquatable<DryrunState>
    {
        [AlgoApiField("error", null)]
        public FixedString128Bytes Error;
        [AlgoApiField("line", null)]
        public ulong Line;
        [AlgoApiField("pc", null)]
        public ulong ProgramCounter;
        [AlgoApiField("scratch", null)]
        public TealValue[] Scratch;
        [AlgoApiField("stack", null)]
        public TealValue[] Stack;

        public bool Equals(DryrunState other)
        {
            return Error.Equals(other.Error)
                && Line.Equals(other.Line)
                && ProgramCounter.Equals(other.ProgramCounter)
                && ArrayComparer.Equals(Scratch, other.Scratch)
                && ArrayComparer.Equals(Stack, other.Stack);
            ;
        }
    }
}

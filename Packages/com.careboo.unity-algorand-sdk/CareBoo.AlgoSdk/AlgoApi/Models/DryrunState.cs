using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunState
        : IEquatable<DryrunState>
    {
        [AlgoApiKey("error", null)]
        public FixedString128Bytes Error;
        [AlgoApiKey("line", null)]
        public ulong Line;
        [AlgoApiKey("pc", null)]
        public ulong ProgramCounter;
        [AlgoApiKey("scratch", null)]
        public TealValue[] Scratch;
        [AlgoApiKey("stack", null)]
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

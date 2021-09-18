using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunState
        : IEquatable<DryrunState>
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
            return Error.Equals(other.Error)
                && Line.Equals(other.Line)
                && ProgramCounter.Equals(other.ProgramCounter)
                && ArrayComparer.Equals(Scratch, other.Scratch)
                && ArrayComparer.Equals(Stack, other.Stack);
            ;
        }
    }
}

using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct StateSchema
        : IEquatable<StateSchema>
    {
        [AlgoApiField("num-byte-slice", "nbs")]
        public ulong NumByteSlices;

        [AlgoApiField("num-uint", "nui")]
        public ulong NumUints;

        public bool Equals(StateSchema other)
        {
            return NumByteSlices.Equals(other.NumByteSlices)
                && NumUints.Equals(other.NumUints)
                ;
        }
    }
}

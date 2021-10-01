using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationStateSchema
        : IEquatable<ApplicationStateSchema>
    {
        [AlgoApiKey("num-byte-slice", "nbs")]
        public ulong NumByteSlices;

        [AlgoApiKey("num-uint", "nui")]
        public ulong NumUints;

        public bool Equals(ApplicationStateSchema other)
        {
            return NumByteSlices.Equals(other.NumByteSlices)
                && NumUints.Equals(other.NumUints)
                ;
        }
    }
}

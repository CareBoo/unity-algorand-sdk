using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationStateSchema
        : IEquatable<ApplicationStateSchema>
    {
        [AlgoApiKey("num-byte-slice")]
        public ulong NumByteSlices;

        [AlgoApiKey("num-uint")]
        public ulong NumUints;

        public bool Equals(ApplicationStateSchema other)
        {
            return NumByteSlices.Equals(other.NumByteSlices)
                && NumUints.Equals(other.NumUints)
                ;
        }
    }
}

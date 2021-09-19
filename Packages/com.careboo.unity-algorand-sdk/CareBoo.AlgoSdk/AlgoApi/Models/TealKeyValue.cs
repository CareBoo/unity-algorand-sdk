using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TealKeyValue
        : IEquatable<TealKeyValue>
    {
        [AlgoApiKey("key")]
        public FixedString128Bytes Key;
        [AlgoApiKey("value")]
        public TealValue Value;

        public bool Equals(TealKeyValue other)
        {
            return Key.Equals(other.Key)
                && Value.Equals(other.Value)
                ;
        }
    }
}

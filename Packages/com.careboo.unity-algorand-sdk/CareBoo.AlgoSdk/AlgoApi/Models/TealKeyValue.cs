using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TealKeyValue
        : IEquatable<TealKeyValue>
    {
        [AlgoApiKey("key", null)]
        public FixedString128Bytes Key;

        [AlgoApiKey("value", null)]
        public TealValue Value;

        public bool Equals(TealKeyValue other)
        {
            return Key.Equals(other.Key)
                && Value.Equals(other.Value)
                ;
        }
    }
}

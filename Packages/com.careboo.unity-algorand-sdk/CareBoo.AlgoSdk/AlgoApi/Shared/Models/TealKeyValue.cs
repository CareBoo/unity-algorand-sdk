using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// Represents a key-value pair in an application store.
    /// </summary>
    [AlgoApiObject]
    public struct TealKeyValue
        : IEquatable<TealKeyValue>
    {
        [AlgoApiField("key", "key")]
        public FixedString128Bytes Key;

        [AlgoApiField("value", "value")]
        public TealValue Value;

        public bool Equals(TealKeyValue other)
        {
            return Key.Equals(other.Key)
                && Value.Equals(other.Value)
                ;
        }
    }
}

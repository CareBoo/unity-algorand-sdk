using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Represents a key-value pair in an application store.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct TealKeyValue
        : IEquatable<TealKeyValue>
    {
        [AlgoApiField("key")]
        public FixedString128Bytes Key;

        [AlgoApiField("value")]
        public TealValue Value;

        public bool Equals(TealKeyValue other)
        {
            return Key.Equals(other.Key)
                && Value.Equals(other.Value)
                ;
        }
    }
}

using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    [Serializable]
    public partial struct ValueDeltaKeyValue
        : IEquatable<ValueDeltaKeyValue>
    {
        [SerializeField] private FixedString64Bytes key;

        [SerializeField] private ValueDelta value;

        public FixedString64Bytes Key
        {
            get => this.key;
            set => this.key = value;
        }

        public ValueDelta Value
        {
            get => this.value;
            set => this.value = value;
        }

        public bool Equals(ValueDeltaKeyValue other)
        {
            return Key.Equals(other.Key)
                && Value.Equals(other.Value)
                ;
        }
    }
}

using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct EvalDeltaKeyValue
        : IEquatable<EvalDeltaKeyValue>
    {
        [AlgoApiField("key", "key")]
        public FixedString64Bytes Key;

        [AlgoApiField("value", "value")]
        public EvalDelta Value;

        public bool Equals(EvalDeltaKeyValue other)
        {
            return Key.Equals(other.Key)
                && Value.Equals(other.Value)
                ;
        }
    }
}

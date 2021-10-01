using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct EvalDeltaKeyValue
        : IEquatable<EvalDeltaKeyValue>
    {
        [AlgoApiKey("key", null)]
        public FixedString64Bytes Key;
        [AlgoApiKey("value", null)]
        public EvalDelta Value;

        public bool Equals(EvalDeltaKeyValue other)
        {
            return Key.Equals(other.Key)
                && Value.Equals(other.Value)
                ;
        }
    }
}

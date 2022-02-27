using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// EvalDeltaKeyValue
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct EvalDeltaKeyValue
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

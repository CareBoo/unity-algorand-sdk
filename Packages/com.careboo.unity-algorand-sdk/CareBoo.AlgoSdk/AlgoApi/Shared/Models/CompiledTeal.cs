using System;

namespace AlgoSdk
{
    /// <summary>
    /// A wrapper class around compiled teal bytes.
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(CompiledTealFormatter))]
    public struct CompiledTeal
        : IEquatable<CompiledTeal>
        , IEquatable<byte[]>
    {
        public byte[] Bytes;

        public bool Equals(CompiledTeal other)
        {
            return ArrayComparer.Equals(Bytes, other.Bytes);
        }

        public bool Equals(byte[] other)
        {
            return ArrayComparer.Equals(Bytes, other);
        }

        public static implicit operator byte[](CompiledTeal compiledTeal)
        {
            return compiledTeal.Bytes;
        }

        public static implicit operator CompiledTeal(byte[] bytes)
        {
            return new CompiledTeal { Bytes = bytes };
        }
    }
}

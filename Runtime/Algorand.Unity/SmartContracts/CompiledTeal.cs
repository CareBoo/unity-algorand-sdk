using System;
using Unity.Collections;
using Algorand.Unity.Collections;

namespace Algorand.Unity
{
    /// <summary>
    /// A wrapper class around compiled teal bytes.
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(CompiledTealFormatter))]
    public partial struct CompiledTeal
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

        public static implicit operator CompiledTeal(long x) => x.ToBytesBigEndian();

        public static implicit operator CompiledTeal(int x) => x.ToBytesBigEndian();

        public static implicit operator CompiledTeal(short x) => x.ToBytesBigEndian();

        public static implicit operator CompiledTeal(ulong x) => x.ToBytesBigEndian();

        public static implicit operator CompiledTeal(uint x) => x.ToBytesBigEndian();

        public static implicit operator CompiledTeal(ushort x) => x.ToBytesBigEndian();

        public static implicit operator CompiledTeal(byte x) => new byte[] { x };

        public static implicit operator CompiledTeal(sbyte x) => unchecked((byte)x);

        public static implicit operator CompiledTeal(NativeText x) => x.ToByteArray();

        public static implicit operator CompiledTeal(FixedString32Bytes x) => x.ToByteArray();

        public static implicit operator CompiledTeal(FixedString64Bytes x) => x.ToByteArray();

        public static implicit operator CompiledTeal(FixedString128Bytes x) => x.ToByteArray();

        public static implicit operator CompiledTeal(FixedString512Bytes x) => x.ToByteArray();

        public static implicit operator CompiledTeal(FixedString4096Bytes x) => x.ToByteArray();

        public static implicit operator CompiledTeal(string x)
        {
            using var text = new NativeText(x, Allocator.Temp);
            return text.ToByteArray();
        }

        public static implicit operator CompiledTeal(NativeArray<byte> x) => x.ToArray();

        public static implicit operator CompiledTeal(NativeArray<byte>.ReadOnly x) => x.ToArray();

        public static implicit operator CompiledTeal(NativeList<byte> x) => x.AsArray().ToArray();
    }
}

using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using Unity.Collections;

namespace Algorand.Unity.WalletConnect.Core
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Size = 32)]
    public unsafe struct SymKey : IByteArray
    {
        public fixed byte bytes[32];

        public int Length => 32;

        public byte this[int index] { get => this.GetByteAt(index); set => this.SetByteAt(index, value); }

        public override string ToString()
        {
            Span<char> output = stackalloc char[64];
            fixed (byte* ptr = bytes)
            {
                var byteSpan = new ReadOnlySpan<byte>(ptr, 32);
                HexConverter.ToHex(byteSpan, output, out _);
            }
            return output.ToString();
        }

        public FixedString128Bytes ToFixedString()
        {
            var output = default(FixedString128Bytes);

            fixed (byte* ptr = bytes)
            {
                var byteSpan = new ReadOnlySpan<byte>(ptr, 32);
                var error = HexConverter.ToHex(byteSpan, ref output);
                if (error > 0)
                {
                    throw new FormatException(error.ToString());
                }
            }

            return output;
        }

        public static SymKey Random()
        {
            return Crypto.Random.Bytes<SymKey>();
        }

        public static SymKey FromString<TFixedString>(TFixedString fixedString)
            where TFixedString : struct, INativeList<byte>, IUTF8Bytes
        {
            var output = default(SymKey);
            var outputSpan = new Span<byte>(output.bytes, 32);
            var error = HexConverter.FromHex(fixedString, outputSpan, out _);
            if (error > 0)
            {
                throw new FormatException(error.ToString());
            }
            return output;
        }

        public void* GetUnsafePtr()
        {
            fixed (byte* ptr = bytes)
            {
                return ptr;
            }
        }
    }
}

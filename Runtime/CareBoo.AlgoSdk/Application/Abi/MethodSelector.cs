using System.Runtime.InteropServices;
using AlgoSdk.Collections;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk.Abi
{
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public struct MethodSelector
        : IByteArray
    {
        public const int SizeBytes = 4;

        [FieldOffset(0)] unsafe fixed byte bytes[SizeBytes];

        public MethodSelector(Method method)
        {
            using var signature = method.GetSignature(Allocator.Temp);
            var hash = Sha512.Hash256Truncated(signature.AsArray().AsReadOnly());
            unsafe
            {
                for (var i = 0; i < SizeBytes; i++)
                    bytes[i] = hash[i];
            }
        }

        public byte this[int index]
        {
            get
            {
                unsafe
                {
                    return bytes[index];
                }
            }
            set
            {
                unsafe
                {
                    bytes[index] = value;
                }
            }
        }

        public int Length => SizeBytes;

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = bytes)
                return b;
        }

        public static explicit operator CompiledTeal(MethodSelector selector)
        {
            var bytes = new byte[selector.Length];
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] = selector[i];
            return bytes;
        }
    }
}

using System.Runtime.InteropServices;
using Algorand.Unity.Collections;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using Unity.Collections;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Used to select a method defined in an ABI. See <see href="https://github.com/algorandfoundation/ARCs/blob/main/ARCs/arc-0004.md#method-selector">Algorand's Method Selector Spec</see> for more details.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public struct MethodSelector
        : IByteArray
    {
        public const int SizeBytes = 4;

        [FieldOffset(0)] private unsafe fixed byte bytes[SizeBytes];

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

        ///<inheritdoc />
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

        ///<inheritdoc />
        public int Length => SizeBytes;

        ///<inheritdoc />
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

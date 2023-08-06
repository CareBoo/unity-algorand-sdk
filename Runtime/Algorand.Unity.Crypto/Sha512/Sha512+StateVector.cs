using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Crypto
{
    public static partial class Sha512
    {
        [StructLayout(LayoutKind.Explicit, Size = 64)]
        internal struct StateVector
        {
            public const int Length = 8;

            [FieldOffset(0)]
            internal FixedBytes64 buffer;

            internal unsafe byte* Buffer
            {
                get
                {
                    fixed (byte* b = &buffer.offset0000.offset0000.byte0000)
                    {
                        return b;
                    }
                }
            }

            public ulong this[int index]
            {
                get
                {
                    ByteArray.CheckElementAccess(index, Length);
                    unsafe
                    {
                        return UnsafeUtility.ReadArrayElement<ulong>(Buffer, index);
                    }
                }
                set
                {
                    ByteArray.CheckElementAccess(index, Length);
                    unsafe
                    {
                        UnsafeUtility.WriteArrayElement(Buffer, index, value);
                    }
                }
            }

            public static implicit operator StateVector(ulong[] arr)
            {
                var result = new StateVector();
                unsafe
                {
                    fixed (void* a = &arr[0])
                    {
                        UnsafeUtility.MemCpy(result.Buffer, a, 64);
                    }
                }

                return result;
            }
        }
    }
}
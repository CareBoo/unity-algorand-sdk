using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Crypto
{
    public static partial class Sha512
    {
        [StructLayout(LayoutKind.Explicit, Size = 16)]
        internal struct StateCount
        {
            public const int Length = 2;

            [FieldOffset(0)]
            internal FixedBytes16 buffer;

            internal unsafe byte* Buffer
            {
                get
                {
                    fixed (byte* b = &buffer.byte0000)
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
        }
    }
}
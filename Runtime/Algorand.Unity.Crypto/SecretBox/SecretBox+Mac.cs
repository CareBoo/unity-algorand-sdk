using System;
using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    public static partial class SecretBox
    {
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public unsafe struct Mac
        {
            public const int SizeBytes = sodium.crypto_secretbox_MACBYTES;

            [FieldOffset(0)] public fixed byte bytes[SizeBytes];

            public static implicit operator Span<byte>(Mac mac)
            {
                return new Span<byte>(mac.bytes, SizeBytes);
            }
        }
    }
}

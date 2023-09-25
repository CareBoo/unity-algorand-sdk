using System;
using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    public static partial class SecretBox
    {
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public unsafe struct Nonce
        {
            public const int SizeBytes = sodium.crypto_secretbox_NONCEBYTES;

            [FieldOffset(0)] public fixed byte bytes[SizeBytes];

            public static implicit operator Span<byte>(Nonce nonce)
            {
                return new Span<byte>(nonce.bytes, SizeBytes);
            }
        }
    }
}

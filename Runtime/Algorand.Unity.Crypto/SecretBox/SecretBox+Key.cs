using System;
using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    public static partial class SecretBox
    {
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public unsafe struct Key
        {
            public const int SizeBytes = sodium.crypto_secretbox_KEYBYTES;

            [FieldOffset(0)] public fixed byte bytes[SizeBytes];

            public static implicit operator Span<byte>(Key key)
            {
                return new Span<byte>(key.bytes, SizeBytes);
            }
        }
    }
}

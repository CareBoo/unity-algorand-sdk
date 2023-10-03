using System.Runtime.InteropServices;
using Algorand.Unity.Crypto;

namespace Algorand.Unity
{
    public partial struct LocalAccountStore
    {
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct EncryptHeader
        {
            /// <summary>
            /// The size of the prefix in bytes.
            /// </summary>
            public const int PrefixSize = sizeof(uint);

            /// <summary>
            /// The prefix of the encrypted store.
            /// </summary>
            public const uint Prefix = 'K' << 24 + 'e' << 16 + 'y' << 8 + 'S';

            /// <summary>
            /// The size of the salt in bytes.
            /// </summary>
            public const int SaltSize = PwHash.Salt.SizeBytes;

            /// <summary>
            /// The size of the nonce in bytes.
            /// </summary>
            public const int NonceSize = SecretBox.Nonce.SizeBytes;

            /// <summary>
            /// The size of the key in bytes.
            /// </summary>
            public const int KeySize = Ed25519.SecretKey.SizeBytes;

            /// <summary>
            /// The size of the mac in bytes.
            /// </summary>
            public const int MacSize = SecretBox.Mac.SizeBytes;

            /// <summary>
            /// The offset of the prefix in bytes.
            /// </summary>
            public const int PrefixOffset = 0;

            /// <summary>
            /// The offset of the salt in bytes.
            /// </summary>
            public const int SaltOffset = PrefixOffset + PrefixSize;

            /// <summary>
            /// The offset of the nonce in bytes.
            /// </summary>
            public const int NonceOffset = SaltSize + SaltOffset;

            /// <summary>
            /// The size of the header in bytes.
            /// </summary>
            public const int SizeBytes = NonceOffset + NonceSize;

            [FieldOffset(0)] public unsafe fixed byte bytes[SizeBytes];
            [FieldOffset(PrefixOffset)] public uint prefix;
            [FieldOffset(SaltOffset)] public PwHash.Salt salt;
            [FieldOffset(NonceOffset)] public SecretBox.Nonce nonce;

            public EncryptHeader(PwHash.Salt salt, SecretBox.Nonce nonce)
            {
                prefix = Prefix;
                this.salt = salt;
                this.nonce = nonce;
            }

            public readonly bool IsValid => prefix == Prefix;
        }
    }
}

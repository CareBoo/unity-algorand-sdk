using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Crypto;

namespace Algorand.Unity
{
    public readonly partial struct LocalAccountStore
    {
        /// <summary>
        /// A view into the encrypted store.
        /// </summary>
        internal readonly ref struct EncryptedView
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
            /// The offset of the cipher in bytes.
            /// </summary>
            public const int CipherOffset = NonceSize + NonceOffset;

            /// <summary>
            /// The prefix bytes.
            /// </summary>
            public readonly ReadOnlySpan<byte> prefix;

            /// <summary>
            /// The salt bytes.
            /// </summary>
            public readonly ReadOnlySpan<byte> salt;

            /// <summary>
            /// The nonce bytes.
            /// </summary>
            public readonly ReadOnlySpan<byte> nonce;

            /// <summary>
            /// The cipher bytes.
            /// </summary>
            public readonly ReadOnlySpan<byte> cipher;

            /// <summary>
            /// The size of an encrypted store with the given number of keys.
            /// </summary>
            /// <param name="keyCount">The number of keys in the store.</param>
            /// <returns></returns>
            public static int SizeBytes(int keyCount) => PrefixSize + SaltSize + NonceSize + SecretBox.CipherLength(keyCount * KeySize);

            /// <summary>
            /// Create an encrypted view from the given bytes.
            /// </summary>
            /// <param name="bytes"></param>
            public EncryptedView(ReadOnlySpan<byte> bytes)
            {
                prefix = bytes.Slice(PrefixOffset, PrefixSize);
                salt = bytes.Slice(SaltOffset, SaltSize);
                nonce = bytes.Slice(NonceOffset, NonceSize);
                cipher = bytes.Slice(CipherOffset, bytes.Length - CipherOffset);
            }

            /// <summary>
            /// The number of keys in the store.
            /// </summary>
            public int KeyCount => SecretBox.MessageLength(cipher.Length) / KeySize;

            /// <summary>
            /// Whether the prefix is valid.
            /// </summary>
            public bool IsPrefixValid => MemoryMarshal.Read<uint>(prefix) == Prefix;

            /// <summary>
            /// Whether the cipher is a valid length.
            /// </summary>
            public bool IsCipherValidLength => SecretBox.MessageLength(cipher.Length) % KeySize == 0;

            /// <summary>
            /// Whether this is an encrypted view of a store.
            /// </summary>
            public bool IsValidFormat => IsPrefixValid && IsCipherValidLength;

            /// <summary>
            /// An error that can occur when decrypting an encrypted store.
            /// </summary>
            public enum DecryptError
            {
                /// <summary>
                /// Not enough memory to decrypt the store.
                /// </summary>
                OutOfMemory = -1,

                /// <summary>
                /// No error occurred.
                /// </summary>
                None,

                /// <summary>
                /// The store is not a valid format.
                /// </summary>
                InvalidFormat = 1,

                /// <summary>
                /// The password is invalid.
                /// </summary>
                InvalidPassword = 2
            }

            /// <summary>
            /// Decrypt the encrypted store with the given password.
            /// </summary>
            /// <param name="password"></param>
            /// <param name="store">The store to encrypt into. Should be the same length as <see cref="KeyCount"/>.</param>
            /// <returns></returns>
            public unsafe DecryptError Decrypt(
                SodiumString password,
                out SodiumArray<Ed25519.SecretKey> secretKeys,
                out SodiumReference<SecretBox.Key> pwHash)
            {
                secretKeys = default;
                pwHash = default;
                var pwHashError = PwHash.Hash(
                    password,
                    MemoryMarshal.Read<PwHash.Salt>(salt),
                    SecretBox.Key.SizeBytes,
                    out var pwHashHandle);
                if (pwHashError != PwHash.HashError.None) return DecryptError.OutOfMemory;
                pwHash = new SodiumReference<SecretBox.Key>(pwHashHandle);

                var messageLength = SecretBox.MessageLength(cipher.Length);
                var keyCount = messageLength / KeySize;
                secretKeys = new SodiumArray<Ed25519.SecretKey>(keyCount);

                if (secretKeys.Length > 0)
                {
                    var decryptError = SecretBox.Decrypt(
                        secretKeys.AsSpan(),
                        cipher,
                        pwHash.GetUnsafePtr(),
                        MemoryMarshal.Read<SecretBox.Nonce>(nonce));

                    if (decryptError != SecretBox.DecryptError.None)
                    {
                        pwHash.Dispose();
                        secretKeys.Dispose();
                        pwHash = default;
                        secretKeys = default;
                        return DecryptError.InvalidPassword;
                    }
                }


                return DecryptError.None;
            }
        }
    }
}

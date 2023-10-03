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
            public const int KeySizeBytes = Ed25519.SecretKey.SizeBytes;

            public readonly EncryptHeader header;

            public readonly ReadOnlySpan<byte> cipher;

            /// <summary>
            /// The size of an encrypted store with the given number of keys.
            /// </summary>
            /// <param name="keyCount">The number of keys in the store.</param>
            /// <returns></returns>
            public static int SizeBytes(int keyCount) => EncryptHeader.SizeBytes + SecretBox.CipherLength(keyCount * KeySizeBytes);

            /// <summary>
            /// Create an encrypted view from the given bytes.
            /// </summary>
            /// <param name="bytes"></param>
            public EncryptedView(ReadOnlySpan<byte> bytes)
            {
                header = MemoryMarshal.Read<EncryptHeader>(bytes.Slice(0, EncryptHeader.SizeBytes));
                cipher = bytes.Slice(EncryptHeader.SizeBytes);
            }

            public int MessageLength => SecretBox.MessageLength(cipher.Length);

            /// <summary>
            /// The number of keys in the store.
            /// </summary>
            public int KeyCount => MessageLength / KeySizeBytes;

            /// <summary>
            /// Whether the cipher is a valid length.
            /// </summary>
            public bool IsCipherValid => MessageLength % KeySizeBytes == 0;

            /// <summary>
            /// Whether this is an encrypted view of a store.
            /// </summary>
            public bool IsValidFormat => header.IsValid && IsCipherValid;

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
                    header.salt,
                    SecretBox.Key.SizeBytes,
                    out var pwHashHandle);
                if (pwHashError != PwHash.HashError.None) return DecryptError.OutOfMemory;
                pwHash = new SodiumReference<SecretBox.Key>(pwHashHandle);

                var keyCount = KeyCount;
                secretKeys = new SodiumArray<Ed25519.SecretKey>(keyCount);

                if (secretKeys.Length > 0)
                {
                    var decryptError = SecretBox.Decrypt(
                        secretKeys.AsByteSpan(),
                        cipher,
                        header.nonce,
                        ref pwHash.RefValue);

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

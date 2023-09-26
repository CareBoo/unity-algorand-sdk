using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Crypto;
using UnityEngine;
using Random = Algorand.Unity.Crypto.Random;

namespace Algorand.Unity
{
    public readonly partial struct LocalAccountStore
    {
        /// <summary>
        /// Errors that can occur when encrypting the store.
        /// </summary>
        public enum EncryptError
        {
            /// <summary>
            /// No error occurred.
            /// </summary>
            None,

            /// <summary>
            /// The store is too large to encrypt.
            /// </summary>
            OutOfMemory
        }

        /// <summary>
        /// The size of the encrypted store in bytes.
        /// </summary>
        public int EncryptedSizeBytes => EncryptedView.SizeBytes(Length);

        /// <summary>
        /// Re-encrypt the store with the given password, creating a new store.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encrypted"></param>
        /// <returns></returns>
        public EncryptError Encrypt(SodiumString password, out string encrypted, out LocalAccountStore store)
        {
            store = default;
            encrypted = null;
            var newSalt = Random.Bytes<PwHash.Salt>();
            var pwHashError = PwHash.Hash(
                password,
                newSalt,
                SecretBox.Key.SizeBytes,
                out var pwHashHandle);
            if (pwHashError != PwHash.HashError.None) return EncryptError.OutOfMemory;

            store = new LocalAccountStore(secretKeys, new SodiumReference<SecretBox.Key>(pwHashHandle), newSalt);
            var result = store.Encrypt(out encrypted);
            if (result != EncryptError.None)
            {
                store.Dispose();
                store = default;
            }
            return result;
        }

        /// <summary>
        /// Encrypt the store with the existing password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encrypted"></param>
        /// <returns></returns>
        public EncryptError Encrypt(out string encrypted)
        {
            encrypted = null;
            var encryptedSizeBytes = EncryptedSizeBytes;
            Span<byte> s = encryptedSizeBytes >= 1024
                ? new byte[encryptedSizeBytes]
                : stackalloc byte[encryptedSizeBytes];
            var prefixSpan = s.Slice(EncryptedView.PrefixOffset, EncryptedView.PrefixSize);
            var saltSpan = s.Slice(EncryptedView.SaltOffset, EncryptedView.SaltSize);
            var nonceSpan = s.Slice(EncryptedView.NonceOffset, EncryptedView.NonceSize);
            var cipherSpan = s.Slice(EncryptedView.CipherOffset, s.Length - EncryptedView.CipherOffset);

            var prefix = EncryptedView.Prefix;
            MemoryMarshal.Write(prefixSpan, ref prefix);

            var saltTmp = salt;
            MemoryMarshal.Write(saltSpan, ref saltTmp);

            if (cipherSpan.Length > 0)
            {
                SecretBox.EncryptError encryptError;
                SecretBox.Nonce nonce;
                unsafe
                {
                    encryptError = SecretBox.Encrypt(cipherSpan, SecretKeySpan, pwHash.GetUnsafePtr(), out nonce);
                }
                MemoryMarshal.Write(nonceSpan, ref nonce);

                if (encryptError != SecretBox.EncryptError.None) return EncryptError.OutOfMemory;
            }

            encrypted = Convert.ToBase64String(s);
            return default;
        }
    }
}

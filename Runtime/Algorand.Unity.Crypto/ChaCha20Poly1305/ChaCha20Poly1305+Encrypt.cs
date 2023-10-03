using System;

namespace Algorand.Unity.Crypto
{
    public static partial class ChaCha20Poly1305
    {
        public enum EncryptionError
        {
            None = 0,
            CipherInvalidSize = 1,
        }

        /// <summary>
        /// Encrypt a message using the ChaCha20Poly1305 algorithm.
        /// </summary>
        /// <param name="cipher">The encrypted message.</param>
        /// <param name="message">The message to encrypt.</param>
        /// <param name="key">The symmetric key to encrypt with.</param>
        /// <param name="nonce">The 12 byte iv to use to encrypt.</param>
        /// <returns></returns>
        public static unsafe EncryptionError Encrypt(
            Span<byte> cipher,
            ReadOnlySpan<byte> message,
            Key key,
            Nonce nonce
        )
        {
            var cipherLength = message.Length + AuthTag.Size;
            if (cipher.Length != cipherLength)
            {
                return EncryptionError.CipherInvalidSize;
            }
            var errorCode = default(int);

            fixed (byte* c = &cipher[0])
            fixed (byte* m = &message[0])
            {
                errorCode = sodium.crypto_aead_chacha20poly1305_ietf_encrypt(
                    c,
                    out _,
                    m,
                    (ulong)message.Length,
                    null,
                    0,
                    null,
                    nonce.GetUnsafePtr(),
                    key.GetUnsafePtr()
                );
            }
            return (EncryptionError)errorCode;
        }

        public static unsafe EncryptionError Encrypt(
            Span<byte> cipher,
            ReadOnlySpan<byte> message,
            SecureMemoryHandle key,
            Nonce nonce
        )
        {
            var cipherLength = message.Length + AuthTag.Size;
            if (cipher.Length != cipherLength)
            {
                return EncryptionError.CipherInvalidSize;
            }
            var errorCode = default(int);

            fixed (byte* c = &cipher[0])
            fixed (byte* m = &message[0])
            {
                errorCode = sodium.crypto_aead_chacha20poly1305_ietf_encrypt(
                    c,
                    out _,
                    m,
                    (ulong)message.Length,
                    null,
                    0,
                    null,
                    nonce.GetUnsafePtr(),
                    key.GetUnsafePtr()
                );
            }
            return (EncryptionError)errorCode;
        }
    }
}

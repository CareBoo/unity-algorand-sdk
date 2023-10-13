using System;

namespace Algorand.Unity.Crypto
{
    public static partial class ChaCha20Poly1305
    {
        public enum DecryptionError
        {
            None = 0,
            VerificationFailed = -1,
            InvalidMessageLength = 1
        }

        public static unsafe DecryptionError Decrypt(
            Span<byte> message,
            ReadOnlySpan<byte> cipher,
            Key key,
            Nonce nonce
        )
        {
            if (message.Length != cipher.Length - AuthTag.Size)
            {
                return DecryptionError.InvalidMessageLength;
            }

            var errorCode = 0;
            fixed (byte* m = &message[0])
            fixed (byte* c = &cipher[0])
            {
                errorCode = sodium.crypto_aead_chacha20poly1305_ietf_decrypt(
                    m,
                    out _,
                    null,
                    c,
                    (ulong)cipher.Length,
                    null,
                    0,
                    nonce.GetUnsafePtr(),
                    key.GetUnsafePtr()
                );
            }
            return (DecryptionError)errorCode;
        }

        public static unsafe DecryptionError Decrypt(
            Span<byte> message,
            ReadOnlySpan<byte> cipher,
            SecureMemoryHandle key,
            Nonce nonce
        )
        {
            if (message.Length != cipher.Length - AuthTag.Size)
            {
                return DecryptionError.InvalidMessageLength;
            }

            var errorCode = 0;
            fixed (byte* m = &message[0])
            fixed (byte* c = &cipher[0])
            {
                errorCode = sodium.crypto_aead_chacha20poly1305_ietf_decrypt(
                    m,
                    out _,
                    null,
                    c,
                    (ulong)cipher.Length,
                    null,
                    0,
                    nonce.GetUnsafePtr(),
                    key.GetUnsafePtr()
                );
            }
            return (DecryptionError)errorCode;
        }
    }
}

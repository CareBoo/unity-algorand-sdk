using System;

namespace Algorand.Unity.Crypto
{
    public static partial class SecretBox
    {
        public enum EncryptError
        {
            Error = -1,
            None
        }

        public enum DecryptError
        {
            Error = -1,
            None
        }

        public static int CipherLength(int messageLength)
        {
            return messageLength + Mac.SizeBytes;
        }

        public static int MessageLength(int cipherLength)
        {
            return cipherLength - Mac.SizeBytes;
        }

        public static unsafe EncryptError Encrypt(
            Span<byte> cipher,
            ReadOnlySpan<byte> message,
            Key* key,
            out Nonce nonce
        )
        {
            nonce = Random.Bytes<Nonce>();
            fixed (byte* noncePtr = nonce.bytes)
            fixed (byte* cipherPtr = cipher)
            fixed (byte* messagePtr = message)
            {
                return (EncryptError)sodium.crypto_secretbox_easy(
                    cipherPtr,
                    messagePtr,
                    (ulong)message.Length,
                    noncePtr,
                    key
                );
            }
        }

        public static unsafe DecryptError Decrypt(
            Span<byte> message,
            ReadOnlySpan<byte> cipher,
            Key* key,
            Nonce nonce
        )
        {
            fixed (byte* messagePtr = message)
            fixed (byte* cipherPtr = cipher)
            {
                return (DecryptError)sodium.crypto_secretbox_open_easy(
                    messagePtr,
                    cipherPtr,
                    (ulong)cipher.Length,
                    nonce.bytes,
                    key
                );
            }
        }
    }
}

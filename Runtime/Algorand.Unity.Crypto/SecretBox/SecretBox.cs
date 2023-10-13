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
            Nonce nonce,
            ref Key key
        )
        {
            fixed (byte* cipherPtr = cipher)
            fixed (byte* messagePtr = message)
            fixed (Key* keyPtr = &key)
            {
                return (EncryptError)sodium.crypto_secretbox_easy(
                    cipherPtr,
                    messagePtr,
                    (ulong)message.Length,
                    nonce.bytes,
                    keyPtr
                );
            }
        }

        public static unsafe DecryptError Decrypt(
            Span<byte> message,
            ReadOnlySpan<byte> cipher,
            Nonce nonce,
            ref Key key
        )
        {
            fixed (byte* messagePtr = message)
            fixed (byte* cipherPtr = cipher)
            fixed (Key* keyPtr = &key)
            {
                return (DecryptError)sodium.crypto_secretbox_open_easy(
                    messagePtr,
                    cipherPtr,
                    (ulong)cipher.Length,
                    nonce.bytes,
                    keyPtr
                );
            }
        }
    }
}

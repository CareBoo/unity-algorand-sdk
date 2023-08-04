using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Crypto
{
    public static partial class ChaCha20Poly1305
    {
        public enum EncryptionError
        {
            None = 0
        }

        public static unsafe EncryptionError Encrypt<TMessage>(
            NativeList<byte> cipher,
            TMessage message,
            Key key,
            Nonce nonce
        )
            where TMessage : struct, IByteArray
        {
            cipher.Length = message.Length + AuthTag.Size;
            var errorCode = sodium.crypto_aead_chacha20poly1305_ietf_encrypt(
                cipher.GetUnsafePtr(),
                out var cipherLength,
                message.GetUnsafePtr(),
                (ulong)message.Length,
                null,
                0,
                null,
                nonce.GetUnsafePtr(),
                key.GetUnsafePtr()
            );
            cipher.Length = (int)cipherLength;
            nonce += 1;
            return (EncryptionError)errorCode;
        }
    }
}
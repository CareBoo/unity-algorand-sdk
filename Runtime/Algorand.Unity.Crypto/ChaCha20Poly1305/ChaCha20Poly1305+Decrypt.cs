using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Crypto
{
    public static partial class ChaCha20Poly1305
    {
        public enum DecryptionError
        {
            None = 0,
            VerificationFailed = -1
        }

        public static unsafe DecryptionError Decrypt<TCipher>(
            NativeList<byte> message,
            TCipher cipher,
            Key key,
            Nonce nonce
        )
            where TCipher : struct, IByteArray
        {
            message.Length = cipher.Length - AuthTag.Size;
            var errorCode = sodium.crypto_aead_chacha20poly1305_ietf_decrypt(
                message.GetUnsafePtr(),
                out var messageLength,
                null,
                cipher.GetUnsafePtr(),
                (ulong)cipher.Length,
                null,
                0,
                nonce.GetUnsafePtr(),
                key.GetUnsafePtr()
            );
            message.Length = (int)messageLength;
            return (DecryptionError)errorCode;
        }
    }
}
using System;
using Algorand.Unity.Crypto;
using Unity.Collections;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity.WalletConnect.Core
{
    public static class Envelope
    {
        public static NativeArray<byte> EncryptType0(
            ReadOnlySpan<byte> message,
            ChaCha20Poly1305.Key key,
            ChaCha20Poly1305.Nonce iv,
            Allocator allocator
        )
        {
            var envelope = new NativeArray<byte>(
                1 + ChaCha20Poly1305.Nonce.SizeBytes + message.Length + ChaCha20Poly1305.AuthTag.Size,
                allocator,
                NativeArrayOptions.UninitializedMemory);

            var offset = 0;
            envelope[offset] = (byte)EnvelopeType.Type0;
            offset += sizeof(byte);

            var ivSlice = envelope.AsSpan().Slice(offset, ChaCha20Poly1305.Nonce.SizeBytes);
            iv.CopyTo(ref ivSlice);
            offset += ChaCha20Poly1305.Nonce.SizeBytes;

            var cipherTextSlice = envelope.AsSpan().Slice(offset, message.Length + ChaCha20Poly1305.AuthTag.Size);
            var error = ChaCha20Poly1305.Encrypt(cipherTextSlice, message, key, iv);
            if (error > 0)
            {
                throw new Exception($"ChaCha20 Encryption error: {error}");
            }

            return envelope;
        }
    }
}

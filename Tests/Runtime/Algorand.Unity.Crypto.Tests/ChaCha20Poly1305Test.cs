using Algorand.Unity.LowLevel;
using NUnit.Framework;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Crypto.Tests
{
    public class ChaCha20Poly1305Test
    {
        [Test]
        public unsafe void EncryptedMessageShouldDecryptWithoutErrors()
        {
            var key = Random.Bytes<ChaCha20Poly1305.Key>();
            var nonce = Random.Bytes<ChaCha20Poly1305.Nonce>();
            var inputMessage = new NativeArray<byte>(1024, Allocator.Temp);
            Random.Randomize(inputMessage);
            using var cipher = new NativeArray<byte>(inputMessage.Length + ChaCha20Poly1305.AuthTag.Size, Allocator.Temp);
            using var outputMessage = new NativeArray<byte>(cipher.Length - ChaCha20Poly1305.AuthTag.Size, Allocator.Temp);
            var encryptError = ChaCha20Poly1305.Encrypt(cipher, inputMessage, key, nonce);
            Assert.AreEqual(ChaCha20Poly1305.EncryptionError.None, encryptError);
            var decryptError =
                ChaCha20Poly1305.Decrypt(outputMessage, cipher, key, nonce);
            Assert.AreEqual(ChaCha20Poly1305.DecryptionError.None, decryptError);
            Assert.AreEqual(inputMessage.Length, outputMessage.Length);
            var cmp = UnsafeUtility.MemCmp(
                inputMessage.GetUnsafePtr(),
                outputMessage.GetUnsafePtr(),
                inputMessage.Length
            );
            Assert.AreEqual(0, cmp);
        }

        [Test]
        public unsafe void AddingToNonceShouldWrapValueOnOverflow()
        {
            var inputNonce = new ChaCha20Poly1305.Nonce();
            for (var i = 0; i < ChaCha20Poly1305.Nonce.SizeUints; i++) inputNonce.buffer[i] = uint.MaxValue;

            inputNonce = inputNonce + 1;
            for (var i = 0; i < ChaCha20Poly1305.Nonce.SizeUints; i++)
                Assert.AreEqual(
                    0,
                    inputNonce.buffer[i],
                    $"Expected nonce to wrap around to 0 on overflow at index {i}");
        }
    }
}

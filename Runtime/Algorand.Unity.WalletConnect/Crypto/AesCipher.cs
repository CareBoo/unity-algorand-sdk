using System.IO;
using System.Security.Cryptography;

namespace Algorand.Unity.WalletConnect
{
    public static class AesCipher
    {
        public static EncryptedPayload EncryptWithKey(byte[] key, byte[] data)
        {
            var encryptedPayload = Encrypt(key, data);
            encryptedPayload.Signature = encryptedPayload.Sign(key);
            return encryptedPayload;
        }

        public static byte[] DecryptWithKey(byte[] key, EncryptedPayload encryptedData)
        {
            encryptedData.ValidateHmac(key);

            using var cipher = CreateCipher();
            cipher.Key = key;
            cipher.IV = encryptedData.Iv;
            var decryptor = cipher.CreateDecryptor(cipher.Key, cipher.IV);

            using var encryptedStream = new MemoryStream(encryptedData.Data);
            using var decryptedStream = new MemoryStream();
            using var cs = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read);
            cs.CopyTo(decryptedStream);
            cs.Flush();
            var result = decryptedStream.ToArray();
            return result;
        }

        private static EncryptedPayload Encrypt(byte[] key, byte[] data)
        {
            using var ms = new MemoryStream();
            using var cipher = CreateCipher();

            byte[] iv = cipher.IV;
            var encryptor = cipher.CreateEncryptor(key, iv);

            Encrypt(data, ms, encryptor);

            var encryptedData = ms.ToArray();

            return (iv, encryptedData);
        }

        private static AesManaged CreateCipher()
        {
            return new AesManaged()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 256
            };
        }

        private static void Encrypt(byte[] data, MemoryStream ms, ICryptoTransform encryptor)
        {
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
        }
    }
}

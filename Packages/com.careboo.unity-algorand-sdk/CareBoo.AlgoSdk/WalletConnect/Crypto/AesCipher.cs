using System.IO;
using System.Security.Cryptography;

namespace AlgoSdk.WalletConnect
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

            using var cryptor = CreateCipher();
            var decryptor = cryptor.CreateDecryptor(cryptor.Key, cryptor.IV);

            using var encryptedStream = new MemoryStream(encryptedData.Data);
            using var decryptedStream = new MemoryStream();
            using var cs = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read);
            cs.CopyTo(decryptedStream);
            return decryptedStream.ToArray();
        }

        static EncryptedPayload Encrypt(byte[] key, byte[] data)
        {
            using var ms = new MemoryStream();
            using var cipher = CreateCipher();

            byte[] iv = cipher.IV;
            var encryptor = cipher.CreateEncryptor(key, iv);

            Encrypt(data, ms, encryptor);

            var encryptedData = ms.ToArray();

            return (iv, encryptedData);
        }

        static AesManaged CreateCipher()
        {
            return new AesManaged()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 256
            };
        }

        static void Encrypt(byte[] data, MemoryStream ms, ICryptoTransform encryptor)
        {
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
        }
    }
}

using System;
using System.Security.Cryptography;

namespace AlgoSdk.WalletConnect
{
    public struct EncryptedPayload
    {
        public byte[] Iv;

        public byte[] Signature;

        public byte[] Data;

        public byte[] Sign(byte[] key)
        {
            if (Iv == null || Data == null || key == null)
                return null;

            using var hmac = new HMACSHA256(key);
            hmac.Initialize();

            byte[] toSign = new byte[Iv.Length + Data.Length];

            Buffer.BlockCopy(Data, 0, toSign, 0, Data.Length);
            Buffer.BlockCopy(Iv, 0, toSign, Data.Length, Iv.Length);

            return hmac.ComputeHash(toSign);
        }

        public static implicit operator EncryptedPayload((byte[] iv, byte[] encryptedData) encryptResult)
        {
            return new EncryptedPayload
            {
                Iv = encryptResult.iv,
                Data = encryptResult.encryptedData
            };
        }
    }
}

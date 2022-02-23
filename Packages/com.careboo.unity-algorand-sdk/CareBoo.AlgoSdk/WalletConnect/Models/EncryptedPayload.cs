using System;
using System.IO;
using System.Security.Cryptography;

namespace AlgoSdk.WalletConnect
{
    [AlgoApiObject]
    public struct EncryptedPayload
    {
        [AlgoApiField("iv", null)]
        public byte[] Iv;

        [AlgoApiField("hmac", null)]
        public byte[] Signature;

        [AlgoApiField("data", null)]
        public byte[] Data;

        public byte[] Sign(byte[] key)
        {
            if (Iv == null || Data == null || key == null)
                return null;

            using var hmac = new HMACSHA256(key);
            hmac.Initialize();

            byte[] toSign = GetSignData();

            return hmac.ComputeHash(toSign);
        }

        public void ValidateHmac(byte[] key)
        {
            var computedHmac = Sign(key);
            if (!ArrayComparer.Equals(Signature, computedHmac))
                throw new InvalidDataException("HMAC given does not match the HMAC computed with key");
        }

        public static implicit operator EncryptedPayload((byte[] iv, byte[] encryptedData) encryptResult)
        {
            return new EncryptedPayload
            {
                Iv = encryptResult.iv,
                Data = encryptResult.encryptedData
            };
        }

        byte[] GetSignData()
        {
            var result = new byte[Iv.Length + Data.Length];
            Buffer.BlockCopy(Data, 0, result, 0, Data.Length);
            Buffer.BlockCopy(Iv, 0, result, Data.Length, Iv.Length);
            return result;
        }
    }
}

using System;
using System.IO;
using System.Security.Cryptography;

namespace Algorand.Unity.WalletConnect
{
    [AlgoApiObject]
    public partial struct EncryptedPayload
    {
        [AlgoApiField("iv")]
        public Hex Iv;

        [AlgoApiField("hmac")]
        public Hex Signature;

        [AlgoApiField("data")]
        public Hex Data;

        public byte[] Sign(byte[] key)
        {
            if (Iv.Data == null || Data.Data == null || key == null)
                return null;

            using var hmac = new HMACSHA256(key);
            hmac.Initialize();

            byte[] toSign = GetSignData();

            return hmac.ComputeHash(toSign);
        }

        public void ValidateHmac(byte[] key)
        {
            var computedHmac = Sign(key);
            if (!Signature.Equals(computedHmac))
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

        private byte[] GetSignData()
        {
            var result = new byte[Iv.Data.Length + Data.Data.Length];
            Buffer.BlockCopy(Data, 0, result, 0, Data.Data.Length);
            Buffer.BlockCopy(Iv, 0, result, Data.Data.Length, Iv.Data.Length);
            return result;
        }
    }
}

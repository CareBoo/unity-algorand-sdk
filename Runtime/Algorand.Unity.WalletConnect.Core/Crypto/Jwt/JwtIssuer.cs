using System;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [Serializable]
    [AlgoApiObject]
    public partial struct JwtIssuer
    {
        [SerializeField]
        private Ed25519.PublicKey publicKey;

        public Ed25519.PublicKey PublicKey
        {
            get => publicKey;
            set => publicKey = value;
        }

        public string Encode()
        {
            Span<byte> headerAndKey = stackalloc byte[Multicodec.HeaderBase58.Length + Ed25519.PublicKey.SizeBytes];
            Multicodec.HeaderBase58.CopyTo(headerAndKey);
            var keyPart = headerAndKey.Slice(Multicodec.HeaderBase58.Length);
            publicKey.CopyTo(ref keyPart);
            return
                $"{Did.Prefix}{Did.Delimiter}{Did.Method}{Did.Delimiter}" +
                $"{Multicodec.Base}{Base58.Encode(headerAndKey)}";
        }

        public static class Did
        {
            public const string Delimiter = ":";
            public const string Prefix = "did";
            public const string Method = "key";
        }

        public static class Multicodec
        {
            public const string Header = "K36";
            public const string Base = "z";

            public static class HeaderBase58
            {
                public const int Length = 2;

                public static void CopyTo(Span<byte> destination)
                {
                    destination[0] = 0xed;
                    destination[1] = 0x01;
                }
            }
        }
    }
}
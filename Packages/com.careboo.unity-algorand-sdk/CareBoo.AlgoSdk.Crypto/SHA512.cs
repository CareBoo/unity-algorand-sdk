using NSec.Cryptography;
using System;

namespace Algorand.Crypto
{
    public static class SHA512
    {
        public static byte[] Hash(byte[] data)
        {
            var algorithm = HashAlgorithm.Sha512_256;
            var readonlyspan = new ReadOnlySpan<byte>(data);
            return algorithm.Hash(readonlyspan);
        }
    }
}

using System;
using static AlgoSdk.Crypto.sodium;

namespace AlgoSdk.Crypto
{
    public unsafe static class Sha512
    {
        public static byte[] Hash(byte[] data)
        {
            throw new NotImplementedException();
        }

        public static byte[] RandomBytes(uint size)
        {
            sodium_init();
            var bytes = new byte[size];
            fixed (byte* b = bytes)
            {
                sodium.randombytes_buf(b, (UIntPtr)size);
            }
            return bytes;
        }
    }
}

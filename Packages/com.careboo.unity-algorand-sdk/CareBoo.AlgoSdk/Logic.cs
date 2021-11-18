using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk
{
    public static class Logic
    {
        public static readonly byte[] SigningPrefix = Encoding.UTF8.GetBytes("Program");

        /// <summary>
        /// Return the <see cref="Address"/> of a program.
        /// </summary>
        /// <param name="program">The stateful or stateless program to get the address of.</param>
        /// <returns>An <see cref="Address"/> for a program.</returns>
        public static Address GetAddress(byte[] program)
        {
            using var bytes = GetSignBytes(program, Allocator.Temp);
            return Sha512.Hash256Truncated(bytes);
        }

        /// <summary>
        /// Get a <see cref="NativeByteArray"/> message used for signing.
        /// </summary>
        /// <param name="program">The program that will be signed.</param>
        /// <param name="allocator">Defines the memory lifetime used for <see cref="NativeByteArray"/>, which must be disposed.</param>
        /// <returns>A <see cref="NativeByteArray"/>. The caller must manage its lifetime.</returns>
        public static NativeByteArray GetSignBytes(byte[] program, Allocator allocator)
        {
            var bytes = new NativeByteArray(SigningPrefix.Length + program.Length, allocator);
            try
            {
                for (var i = 0; i < SigningPrefix.Length; i++)
                    bytes[i] = SigningPrefix[i];
                for (var i = SigningPrefix.Length; i < bytes.Length; i++)
                    bytes[i] = program[i - SigningPrefix.Length];
            }
            finally
            {
                bytes.Dispose();
            }
            return bytes;
        }
    }
}

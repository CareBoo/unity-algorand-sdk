using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;
using static AlgoSdk.Crypto.Ed25519;

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
        public static Address GetAddress(CompiledTeal program)
        {
            using var bytes = GetSignBytes(program, Allocator.Persistent);
            return Sha512.Hash256Truncated(bytes);
        }

        /// <summary>
        /// Get a <see cref="NativeByteArray"/> message used for signing.
        /// </summary>
        /// <param name="program">The program that will be signed.</param>
        /// <param name="allocator">Defines the memory lifetime used for <see cref="NativeByteArray"/>, which must be disposed.</param>
        /// <returns>A <see cref="NativeByteArray"/>. The caller must manage its lifetime.</returns>
        public static NativeByteArray GetSignBytes(CompiledTeal program, Allocator allocator)
        {
            var bytes = new NativeByteArray(SigningPrefix.Length + program.Bytes.Length, allocator);
            for (var i = 0; i < SigningPrefix.Length; i++)
                bytes[i] = SigningPrefix[i];
            for (var i = SigningPrefix.Length; i < bytes.Length; i++)
                bytes[i] = program.Bytes[i - SigningPrefix.Length];
            return bytes;
        }

        /// <summary>
        /// Signs the given program and returns the signature.
        /// </summary>
        /// <param name="program">Program to sign</param>
        /// <param name="secretKey">Key to sign this program with.</param>
        /// <returns><see cref="Sig"/></returns>
        public static Sig Sign(CompiledTeal program, SecretKeyHandle secretKey)
        {
            using var programSignBytes = Logic.GetSignBytes(program, Allocator.Persistent);
            return secretKey.Sign(programSignBytes);
        }

        /// <summary>
        /// Signs the <paramref name="program"/> with the <paramref name="msig"/> and returns a signature and its index.
        /// </summary>
        /// <param name="program">The program to sign.</param>
        /// <param name="msig">A <see cref="MultisigSig"/> that contains the <see cref="PublicKey"/> matching <paramref name="privateKey"/>.</param>
        /// <param name="privateKey">The private key to sign with. Its corresponding <see cref="PublicKey"/> must be inside of <paramref name="msig"/>.</param>
        /// <returns>A tuple of the <see cref="Sig"/> from signing the program and its index in the <paramref name="msig"/></returns>
        public static (Sig, int) Sign(CompiledTeal program, MultisigSig msig, PrivateKey privateKey)
        {
            if (msig.Subsigs == null)
                throw new ArgumentException("msig has null Sub signatures", nameof(msig));
            using var keyPair = privateKey.ToKeyPair();
            var index = -1;
            for (var i = 0; i < msig.Subsigs.Length; i++)
            {
                if (msig.Subsigs[i].PublicKey.Equals(keyPair.PublicKey))
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
                throw new ArgumentException("Could not find PublicKey in msig matching given private key", nameof(msig));

            var sig = Sign(program, keyPair.SecretKey);
            return (sig, index);
        }

        /// <summary>
        /// Creates a signature compatible with ed25519verify opcode from raw program bytes.
        /// </summary>
        /// <param name="sk">The secretkey handle to use to sign using the given data and the program.</param>
        /// <param name="data">The data to sign.</param>
        /// <param name="program">The program to hash and prepend to the signed message.</param>
        /// <returns>A signature compatible with ed25519verify opcode.</returns>
        public static Sig TealSignProgram(SecretKeyHandle sk, NativeArray<byte> data, CompiledTeal program)
        {
            var address = GetAddress(program);
            return TealSign(sk, data, address);
        }

        /// <summary>
        /// Creates a signature compatible with ed25519verify opcode from raw program bytes.
        /// </summary>
        /// <param name="sk">The secretkey handle to use to sign using the given data and the program.</param>
        /// <param name="data">The data to sign.</param>
        /// <param name="program">The program to hash and prepend to the signed message.</param>
        /// <returns>A signature compatible with ed25519verify opcode.</returns>
        public static Sig TealSignProgram(SecretKeyHandle sk, byte[] data, CompiledTeal program)
        {
            var address = GetAddress(program);
            return TealSign(sk, data, address);
        }

        /// <summary>
        /// Creates a signature compatible with ed25519verify opcode from raw program bytes.
        /// </summary>
        /// <typeparam name="TBytes">The type of the byte array.</typeparam>
        /// <param name="sk">The secretkey handle to use to sign using the given data and the program.</param>
        /// <param name="data">The data to sign.</param>
        /// <param name="program">The program to hash and prepend to the signed message.</param>
        /// <returns>A signature compatible with ed25519verify opcode.</returns>
        public static Sig TealSignProgram<TBytes>(SecretKeyHandle sk, TBytes data, CompiledTeal program)
            where TBytes : IByteArray
        {
            var address = GetAddress(program);
            return TealSign(sk, data, address);
        }

        /// <summary>
        /// Creates a signature compatible with ed25519verify opcode from the contract address.
        /// </summary>
        /// <param name="sk">The secretkey handle to use to sign using the given data and the program.</param>
        /// <param name="data">The data to sign.</param>
        /// <param name="contractAddress">The teal contract address to use to sign this data.</param>
        /// <returns>A signature compatible with ed25519verify opcode.</returns>
        public static Sig TealSign(SecretKeyHandle sk, NativeArray<byte> data, Address contractAddress)
        {
            return TealSign(sk, new NativeByteArray(data), contractAddress);
        }

        /// <summary>
        /// Creates a signature compatible with ed25519verify opcode from the contract address.
        /// </summary>
        /// <param name="sk">The secretkey handle to use to sign using the given data and the program.</param>
        /// <param name="data">The data to sign.</param>
        /// <param name="contractAddress">The teal contract address to use to sign this data.</param>
        /// <returns>A signature compatible with ed25519verify opcode.</returns>
        public static Sig TealSign(SecretKeyHandle sk, byte[] data, Address contractAddress)
        {
            using var nativeData = new NativeByteArray(data, Allocator.Temp);
            return TealSign(sk, data, contractAddress);
        }

        /// <summary>
        /// Creates a signature compatible with ed25519verify opcode from the contract address.
        /// </summary>
        /// <typeparam name="TBytes">The type of the byte array.</typeparam>
        /// <param name="sk">The secretkey handle to use to sign using the given data and the program.</param>
        /// <param name="data">The data to sign.</param>
        /// <param name="contractAddress">The teal contract address to use to sign this data.</param>
        /// <returns>A signature compatible with ed25519verify opcode.</returns>
        public static Sig TealSign<TBytes>(SecretKeyHandle sk, TBytes data, Address contractAddress)
            where TBytes : IByteArray
        {
            var concatenated = new NativeByteArray(contractAddress.Length + data.Length, Allocator.Temp);
            try
            {
                for (var i = 0; i < contractAddress.Length; i++)
                    concatenated[i] = contractAddress[i];
                for (var i = contractAddress.Length; i < data.Length; i++)
                    concatenated[i] = data[i - contractAddress.Length];

                return sk.Sign(concatenated);
            }
            finally
            {
                concatenated.Dispose();
            }

        }
    }
}

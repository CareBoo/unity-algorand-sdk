using System;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct LogicSig
        : ISignature
        , IEquatable<LogicSig>
    {
        [AlgoApiKey("l")]
        public byte[] Program;

        [AlgoApiKey("arg")]
        public FixedList128Bytes<byte>[] Args;

        [AlgoApiKey("sig")]
        public Signature Sig;

        [AlgoApiKey("msig")]
        public MultiSig MultiSig;

        public bool Equals(LogicSig other)
        {
            return Sig.Equals(other.Sig)
                && MultiSig.Equals(other.MultiSig)
                && ArrayComparer.Equals(Program, other.Program)
                && ArrayComparer.Equals(Args, other.Args)
                ;
        }

        public bool IsValid(Address sender)
        {

            using var programByteArray = new NativeByteArray(Program, Allocator.Temp);
            return Sig.Verify(programByteArray, sender)
                || MultiSig.Verify(programByteArray, sender)
                || VerifyProgram(programByteArray, sender)
                ;
        }

        bool VerifyProgram(NativeByteArray programByteArray, Ed25519.PublicKey sender)
        {
            Sha512_256_Hash programHash = default;
            unsafe
            {
                programHash = Sha512.Hash256Truncated(programByteArray.GetUnsafePtr(), programByteArray.Length);
            }
            return ByteArray.Equals(programHash, sender);
        }
    }
}

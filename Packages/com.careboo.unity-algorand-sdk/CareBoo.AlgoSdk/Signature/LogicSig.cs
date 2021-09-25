using System;
using System.Text;
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
        public byte[][] Args;

        [AlgoApiKey("sig")]
        public Signature Sig;

        [AlgoApiKey("msig")]
        public MultiSig MultiSig;

        public bool Equals(LogicSig other)
        {
            return ArrayComparer.Equals(Program, other.Program)
                && ArgsEqual(other)
                && Sig.Equals(other.Sig)
                && MultiSig.Equals(other.MultiSig)
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
            unsafe
            {
                var programHash = Sha512.Hash256Truncated((void*)programByteArray.Buffer, programByteArray.Length);
                return ByteArray.Equals(programHash, sender);
            }
        }

        bool ArgsEqual(LogicSig other)
        {
            if (Args == null)
                return other.Args == null;

            if (Args.Length != other.Args.Length)
                return false;

            for (var i = 0; i < Args.Length; i++)
                if (!ArrayComparer.Equals(Args[i], other.Args[i]))
                    return false;

            return true;
        }
    }
}

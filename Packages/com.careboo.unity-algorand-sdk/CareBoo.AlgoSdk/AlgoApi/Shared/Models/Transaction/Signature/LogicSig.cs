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
        [AlgoApiField("logicsig", "l")]
        public byte[] Program;

        [AlgoApiField("args", "arg")]
        public FixedList128Bytes<byte>[] Args;

        [AlgoApiField("signature", "sig")]
        public Sig Sig;

        [AlgoApiField("multisig-signature", "msig")]
        public Multisig Multisig;

        public bool Equals(LogicSig other)
        {
            return Sig.Equals(other.Sig)
                && Multisig.Equals(other.Multisig)
                && ArrayComparer.Equals(Program, other.Program)
                && ArrayComparer.Equals(Args, other.Args)
                ;
        }

        public bool IsValid(Address sender)
        {
            using var programByteArray = new NativeByteArray(Program, Allocator.Temp);
            return (!Sig.Equals(default) && Sig.Verify(programByteArray, sender))
                || (!Multisig.Equals(default) && Multisig.Verify(programByteArray))
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

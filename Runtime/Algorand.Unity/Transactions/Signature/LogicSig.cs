using System;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using Unity.Collections;

namespace Algorand.Unity
{
    [AlgoApiObject]
    public partial struct LogicSig
        : ISignature
        , IEquatable<LogicSig>
    {
        [AlgoApiField("l")]
        public byte[] Program;

        [AlgoApiField("arg")]
        public FixedList128Bytes<byte>[] Args;

        [AlgoApiField("sig")]
        public Sig Sig;

        [AlgoApiField("msig")]
        public MultisigSig Multisig;

        public bool Equals(LogicSig other)
        {
            return Sig.Equals(other.Sig)
                && Multisig.Equals(other.Multisig)
                && ArrayComparer.Equals(Program, other.Program)
                && ArrayComparer.Equals(Args, other.Args)
                ;
        }

        /// <summary>
        /// Determines if transactions from the sender can by signed by this <see cref="LogicSig"/>.
        /// </summary>
        /// <param name="sender">The <see cref="Transaction.Sender"/></param>
        /// <returns><c>true</c> if this <see cref="LogicSig"/> can sign for the sender.</returns>
        public bool IsValid(Address sender)
        {
            using var programByteArray = Logic.GetSignBytes(Program, Allocator.Temp);
            return (!Sig.Equals(default) && Sig.Verify(programByteArray, sender))
                || (!Multisig.Equals(default) && Multisig.Verify(programByteArray))
                || VerifyProgram(programByteArray, sender)
                ;
        }

        /// <summary>
        /// Get the <see cref="Address"/> of this logicsig.
        /// </summary>
        /// <returns>An <see cref="Address"/> from the checksum of the logic sig.</returns>
        public Address GetAddress()
        {
            return Logic.GetAddress(Program);
        }

        private bool VerifyProgram(NativeByteArray bytes, Address sender)
        {
            return GetAddress().Equals(Sha512.Hash256Truncated(bytes));
        }
    }
}

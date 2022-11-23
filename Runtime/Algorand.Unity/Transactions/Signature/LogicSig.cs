using System;
using System.Linq;
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
            using var programByteArray = Logic.GetSignBytes(Program, Allocator.Persistent);
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

        public static implicit operator Algorand.LogicsigSignature(LogicSig lsig)
        {
            return new Algorand.LogicsigSignature(
                lsig.Program,
                lsig.Args.Select(a => a.ToArray()).ToList(),
                lsig.Sig == default ? null : lsig.Sig.ToArray(),
                lsig.Multisig
                );
        }

        public static implicit operator LogicSig(Algorand.LogicsigSignature lsig)
        {
            var args = new FixedList128Bytes<byte>[lsig.Args.Count];
            for (var i = 0; i < args.Length; i++)
            {
                args[i] = new FixedList128Bytes<byte>();
                for (var j = 0; j < lsig.Args[i].Length; j++)
                {
                    args[i][j] = lsig.Args[i][j];
                }
            }

            return new LogicSig
            {
                Program = lsig.Logic,
                Args = args,
                Multisig = lsig.Msig,
                Sig = lsig.Sig
            };
        }
    }
}

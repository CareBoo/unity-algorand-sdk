using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct SignProgramMultisigResponse
        : IEquatable<SignProgramMultisigResponse>
    {
        [AlgoApiField("multisig", null)]
        public byte[] SignedProgram;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(SignProgramMultisigResponse other)
        {
            return ArrayComparer.Equals(SignedProgram, other.SignedProgram)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}

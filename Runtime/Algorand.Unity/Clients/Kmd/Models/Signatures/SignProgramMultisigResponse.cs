using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct SignProgramMultisigResponse
        : IEquatable<SignProgramMultisigResponse>
    {
        [AlgoApiField("multisig")]
        public byte[] SignedProgram;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
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

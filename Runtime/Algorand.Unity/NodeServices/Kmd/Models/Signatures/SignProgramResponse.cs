using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct SignProgramResponse
        : IEquatable<SignProgramResponse>
    {
        [AlgoApiField("sig")]
        public Sig SignedProgram;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        public bool Equals(SignProgramResponse other)
        {
            return ArrayComparer.Equals(SignedProgram, other.SignedProgram)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}

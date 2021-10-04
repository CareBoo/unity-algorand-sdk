using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct SignProgramResponse
        : IEquatable<SignProgramResponse>
    {
        [AlgoApiField("sig", null)]
        public byte[] SignedProgram;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
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

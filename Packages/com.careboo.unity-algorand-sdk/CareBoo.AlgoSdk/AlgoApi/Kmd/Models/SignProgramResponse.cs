using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct SignProgramResponse
        : IEquatable<SignProgramResponse>
    {
        [AlgoApiField("sig", null)]
        public Sig Sig;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(SignProgramResponse other)
        {
            return Sig.Equals(other.Sig)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}

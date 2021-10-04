using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct SignProgramMultiSigResponse
        : IEquatable<SignProgramMultiSigResponse>
    {
        [AlgoApiField("multisig", null)]
        public FixedString128Bytes MultiSig;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(SignProgramMultiSigResponse other)
        {
            return MultiSig.Equals(other.MultiSig)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}

using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct SignMultiSigResponse
        : IEquatable<SignMultiSigResponse>
    {
        [AlgoApiField("multisig", null)]
        public byte[] SignedTransaction;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(SignMultiSigResponse other)
        {
            return ArrayComparer.Equals(SignedTransaction, other.SignedTransaction)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}

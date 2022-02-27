using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct SignMultisigResponse
        : IEquatable<SignMultisigResponse>
    {
        [AlgoApiField("multisig", null)]
        public byte[] SignedTransaction;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(SignMultisigResponse other)
        {
            return ArrayComparer.Equals(SignedTransaction, other.SignedTransaction)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}

using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct SignMultisigResponse
        : IEquatable<SignMultisigResponse>
    {
        [AlgoApiField("multisig")]
        public byte[] SignedTransaction;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
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

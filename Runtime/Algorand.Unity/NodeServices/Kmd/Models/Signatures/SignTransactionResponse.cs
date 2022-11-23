using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct SignTransactionResponse
        : IEquatable<SignTransactionResponse>
    {
        [AlgoApiField("signed_transaction")]
        public byte[] SignedTransaction;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        public bool Equals(SignTransactionResponse other)
        {
            return ArrayComparer.Equals(SignedTransaction, other.SignedTransaction)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}

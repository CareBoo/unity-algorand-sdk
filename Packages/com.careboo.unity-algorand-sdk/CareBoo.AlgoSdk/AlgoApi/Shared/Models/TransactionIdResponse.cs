using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionIdResponse
        : IEquatable<TransactionIdResponse>
    {
        [AlgoApiField("txId", "txId")]
        public TransactionId TxId;

        public bool Equals(TransactionIdResponse other)
        {
            return TxId.Equals(other.TxId);
        }

        public static implicit operator TransactionId(TransactionIdResponse resp)
        {
            return resp.TxId;
        }
    }
}

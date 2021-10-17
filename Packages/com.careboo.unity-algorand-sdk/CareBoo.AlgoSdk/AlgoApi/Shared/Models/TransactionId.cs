using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionIdResponse
        : IEquatable<TransactionIdResponse>
    {
        [AlgoApiField("txId", "txId")]
        public FixedString64Bytes TxId;

        public bool Equals(TransactionIdResponse other)
        {
            return TxId.Equals(other.TxId);
        }

        public static implicit operator FixedString64Bytes(TransactionIdResponse resp)
        {
            return resp.TxId;
        }
    }
}

using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionIdResponse
        : IEquatable<TransactionIdResponse>
    {
        [AlgoApiField("txId", "txId")]
        public Sha512_256_Hash TxId;

        public bool Equals(TransactionIdResponse other)
        {
            return TxId.Equals(other.TxId);
        }

        public static implicit operator Sha512_256_Hash(TransactionIdResponse resp)
        {
            return resp.TxId;
        }
    }
}

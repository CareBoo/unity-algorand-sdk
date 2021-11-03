using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Response from <see cref="IAlgodClient.SendTransaction"/>. Wraps a <see cref="TransactionId"/>.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public struct TransactionIdResponse
        : IEquatable<TransactionIdResponse>
    {
        /// <summary>
        /// The returned <see cref="TransactionId"/>
        /// </summary>
        [AlgoApiField("txId", "txId")]
        [Tooltip("The returned TransactionId")]
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

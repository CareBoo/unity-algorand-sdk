using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Proof of transaction in a block.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct MerkleProof
        : IEquatable<MerkleProof>
    {
        /// <summary>
        /// Index of the transaction in the block's payset.
        /// </summary>
        [AlgoApiField("idx")]
        [Tooltip("Index of the transaction in the block's payset.")]
        public ulong TransactionIndex;

        /// <summary>
        /// Merkle proof of transaction membership.
        /// </summary>
        [AlgoApiField("proof")]
        [Tooltip("Merkle proof of transaction membership.")]
        public string Proof;

        /// <summary>
        /// Hash of SignedTxnInBlock for verifying proof.
        /// </summary>
        [AlgoApiField("stibhash")]
        [Tooltip("Hash of SignedTxnInBlock for verifying proof.")]
        public string SignedTransactionHash;

        public bool Equals(MerkleProof other)
        {
            return TransactionIndex == other.TransactionIndex
                && StringComparer.Equals(Proof, other.Proof)
                && StringComparer.Equals(SignedTransactionHash, other.SignedTransactionHash)
                ;
        }
    }
}

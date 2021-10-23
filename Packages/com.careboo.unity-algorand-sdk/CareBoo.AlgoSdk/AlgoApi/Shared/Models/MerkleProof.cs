using System;

namespace AlgoSdk
{
    /// <summary>
    /// Proof of transaction in a block.
    /// </summary>
    [AlgoApiObject]
    public struct MerkleProof
        : IEquatable<MerkleProof>
    {
        /// <summary>
        /// Index of the transaction in the block's payset.
        /// </summary>
        [AlgoApiField("idx", "idx")]
        public ulong TransactionIndex;

        /// <summary>
        /// Merkle proof of transaction membership.
        /// </summary>
        [AlgoApiField("proof", "proof")]
        public string Proof;

        /// <summary>
        /// Hash of SignedTxnInBlock for verifying proof.
        /// </summary>
        [AlgoApiField("stibhash", "stibhash")]
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

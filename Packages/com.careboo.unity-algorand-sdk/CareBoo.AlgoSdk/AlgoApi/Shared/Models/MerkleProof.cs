using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct MerkleProof
        : IEquatable<MerkleProof>
    {
        [AlgoApiField("idx", "idx")]
        public ulong TransactionIndex;

        [AlgoApiField("proof", "proof")]
        public string Proof;

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

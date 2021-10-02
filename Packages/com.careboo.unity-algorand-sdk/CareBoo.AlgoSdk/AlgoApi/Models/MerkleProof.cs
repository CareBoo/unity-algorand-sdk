using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct MerkleProof
        : IEquatable<MerkleProof>
    {
        [AlgoApiField("idx", null)]
        public ulong TransactionIndex;
        [AlgoApiField("proof", null)]
        public string Proof;
        [AlgoApiField("stibhash", null)]
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

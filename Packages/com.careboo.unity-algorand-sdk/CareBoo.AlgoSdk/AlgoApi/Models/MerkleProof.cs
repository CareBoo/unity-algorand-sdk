using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct MerkleProof
        : IEquatable<MerkleProof>
    {
        [AlgoApiKey("idx", null)]
        public ulong TransactionIndex;
        [AlgoApiKey("proof", null)]
        public string Proof;
        [AlgoApiKey("stibhash", null)]
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

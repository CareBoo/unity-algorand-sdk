using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct MerkleProof
        : IMessagePackObject
    {
        public ulong TransactionIndex;
        public string Proof;
        public string SignedTransactionHash;
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<MerkleProof>.Map merkleProofFields =
            new Field<MerkleProof>.Map()
                .Assign("idx", (ref MerkleProof x) => ref x.TransactionIndex)
                .Assign("proof", (ref MerkleProof x) => ref x.Proof, StringComparer.Instance)
                .Assign("stibhash", (ref MerkleProof x) => ref x.SignedTransactionHash, StringComparer.Instance)
                ;
    }
}

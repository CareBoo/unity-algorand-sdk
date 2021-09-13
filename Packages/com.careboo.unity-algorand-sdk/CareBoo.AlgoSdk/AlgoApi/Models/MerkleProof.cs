using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct MerkleProof
        : IMessagePackObject
    {
        [AlgoApiKey("idx")]
        public ulong TransactionIndex;
        [AlgoApiKey("proof")]
        public string Proof;
        [AlgoApiKey("stibhash")]
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

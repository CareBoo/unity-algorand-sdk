//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlgoSdk
{
    
    
    public partial struct MerkleProof
    {
        
        private static bool @__generated__IsValid = MerkleProof.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.MerkleProof>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.MerkleProof>().Assign("idx", "idx", (AlgoSdk.MerkleProof x) => x.TransactionIndex, (ref AlgoSdk.MerkleProof x, System.UInt64 value) => x.TransactionIndex = value, false).Assign("proof", "proof", (AlgoSdk.MerkleProof x) => x.Proof, (ref AlgoSdk.MerkleProof x, System.String value) => x.Proof = value, AlgoSdk.StringComparer.Instance, false).Assign("stibhash", "stibhash", (AlgoSdk.MerkleProof x) => x.SignedTransactionHash, (ref AlgoSdk.MerkleProof x, System.String value) => x.SignedTransactionHash = value, AlgoSdk.StringComparer.Instance, false));
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.MerkleProof[]>(AlgoSdk.Formatters.ArrayFormatter<AlgoSdk.MerkleProof>.Instance);
            return true;
        }
    }
}

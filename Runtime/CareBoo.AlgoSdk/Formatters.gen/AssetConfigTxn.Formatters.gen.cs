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
    
    
    public partial struct AssetConfigTxn
    {
        
        private static bool @__generated__IsValid = AssetConfigTxn.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.AssetConfigTxn>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.AssetConfigTxn>(false).Assign("fee", (AlgoSdk.AssetConfigTxn x) => x.Fee, (ref AlgoSdk.AssetConfigTxn x, AlgoSdk.MicroAlgos value) => x.Fee = value).Assign("fv", (AlgoSdk.AssetConfigTxn x) => x.FirstValidRound, (ref AlgoSdk.AssetConfigTxn x, System.UInt64 value) => x.FirstValidRound = value).Assign("gh", (AlgoSdk.AssetConfigTxn x) => x.GenesisHash, (ref AlgoSdk.AssetConfigTxn x, AlgoSdk.GenesisHash value) => x.GenesisHash = value).Assign("lv", (AlgoSdk.AssetConfigTxn x) => x.LastValidRound, (ref AlgoSdk.AssetConfigTxn x, System.UInt64 value) => x.LastValidRound = value).Assign("snd", (AlgoSdk.AssetConfigTxn x) => x.Sender, (ref AlgoSdk.AssetConfigTxn x, AlgoSdk.Address value) => x.Sender = value).Assign("type", (AlgoSdk.AssetConfigTxn x) => x.TransactionType, (ref AlgoSdk.AssetConfigTxn x, AlgoSdk.TransactionType value) => x.TransactionType = value, AlgoSdk.ByteEnumComparer<AlgoSdk.TransactionType>.Instance).Assign("gen", (AlgoSdk.AssetConfigTxn x) => x.GenesisId, (ref AlgoSdk.AssetConfigTxn x, Unity.Collections.FixedString32Bytes value) => x.GenesisId = value).Assign("grp", (AlgoSdk.AssetConfigTxn x) => x.Group, (ref AlgoSdk.AssetConfigTxn x, AlgoSdk.TransactionId value) => x.Group = value).Assign("lx", (AlgoSdk.AssetConfigTxn x) => x.Lease, (ref AlgoSdk.AssetConfigTxn x, AlgoSdk.TransactionId value) => x.Lease = value).Assign("note", (AlgoSdk.AssetConfigTxn x) => x.Note, (ref AlgoSdk.AssetConfigTxn x, System.Byte[] value) => x.Note = value, AlgoSdk.ArrayComparer<System.Byte>.Instance).Assign("rekey", (AlgoSdk.AssetConfigTxn x) => x.RekeyTo, (ref AlgoSdk.AssetConfigTxn x, AlgoSdk.Address value) => x.RekeyTo = value).Assign("caid", (AlgoSdk.AssetConfigTxn x) => x.ConfigAsset, (ref AlgoSdk.AssetConfigTxn x, AlgoSdk.AssetIndex value) => x.ConfigAsset = value).Assign("apar", (AlgoSdk.AssetConfigTxn x) => x.AssetParams, (ref AlgoSdk.AssetConfigTxn x, AlgoSdk.AssetParams value) => x.AssetParams = value));
            return true;
        }
        
        public partial struct Params
        {
            
            private static bool @__generated__IsValid = Params.@__generated__InitializeAlgoApiFormatters();
            
            private static bool @__generated__InitializeAlgoApiFormatters()
            {
                AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.AssetConfigTxn.Params>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.AssetConfigTxn.Params>(false).Assign("xaid", (AlgoSdk.AssetConfigTxn.Params x) => x.ConfigAsset, (ref AlgoSdk.AssetConfigTxn.Params x, AlgoSdk.AssetIndex value) => x.ConfigAsset = value).Assign("params", (AlgoSdk.AssetConfigTxn.Params x) => x.AssetParams, (ref AlgoSdk.AssetConfigTxn.Params x, AlgoSdk.AssetParams value) => x.AssetParams = value));
                return true;
            }
        }
    }
}

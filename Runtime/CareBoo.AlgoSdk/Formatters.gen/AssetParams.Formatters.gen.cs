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
    
    
    public partial struct AssetParams
    {
        
        private static bool @__generated__IsValid = AssetParams.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.AssetParams>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.AssetParams>(false).Assign("c", (AlgoSdk.AssetParams x) => x.Clawback, (ref AlgoSdk.AssetParams x, AlgoSdk.Address value) => x.Clawback = value).Assign("dc", (AlgoSdk.AssetParams x) => x.Decimals, (ref AlgoSdk.AssetParams x, System.UInt32 value) => x.Decimals = value).Assign("df", (AlgoSdk.AssetParams x) => x.DefaultFrozen, (ref AlgoSdk.AssetParams x, System.Boolean value) => x.DefaultFrozen = value).Assign("f", (AlgoSdk.AssetParams x) => x.Freeze, (ref AlgoSdk.AssetParams x, AlgoSdk.Address value) => x.Freeze = value).Assign("m", (AlgoSdk.AssetParams x) => x.Manager, (ref AlgoSdk.AssetParams x, AlgoSdk.Address value) => x.Manager = value).Assign("am", (AlgoSdk.AssetParams x) => x.MetadataHash, (ref AlgoSdk.AssetParams x, AlgoSdk.Crypto.Sha512_256_Hash value) => x.MetadataHash = value).Assign("an", (AlgoSdk.AssetParams x) => x.Name, (ref AlgoSdk.AssetParams x, Unity.Collections.FixedString64Bytes value) => x.Name = value).Assign("r", (AlgoSdk.AssetParams x) => x.Reserve, (ref AlgoSdk.AssetParams x, AlgoSdk.Address value) => x.Reserve = value).Assign("t", (AlgoSdk.AssetParams x) => x.Total, (ref AlgoSdk.AssetParams x, System.UInt64 value) => x.Total = value).Assign("un", (AlgoSdk.AssetParams x) => x.UnitName, (ref AlgoSdk.AssetParams x, Unity.Collections.FixedString32Bytes value) => x.UnitName = value).Assign("au", (AlgoSdk.AssetParams x) => x.Url, (ref AlgoSdk.AssetParams x, Unity.Collections.FixedString128Bytes value) => x.Url = value));
            return true;
        }
    }
}

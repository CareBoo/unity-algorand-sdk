//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlgoSdk.Kmd
{
    
    
    public partial struct VersionsResponse
    {
        
        private static bool @__generated__IsValid = VersionsResponse.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Kmd.VersionsResponse>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Kmd.VersionsResponse>(false).Assign("versions", (AlgoSdk.Kmd.VersionsResponse x) => x.Versions, (ref AlgoSdk.Kmd.VersionsResponse x, Unity.Collections.FixedString64Bytes[] value) => x.Versions = value, AlgoSdk.ArrayComparer<Unity.Collections.FixedString64Bytes>.Instance));
            return true;
        }
    }
}

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
    
    
    public partial struct ImportKeyResponse
    {
        
        private static bool @__generated__IsValid = ImportKeyResponse.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Kmd.ImportKeyResponse>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Kmd.ImportKeyResponse>(false).Assign("address", (AlgoSdk.Kmd.ImportKeyResponse x) => x.Address, (ref AlgoSdk.Kmd.ImportKeyResponse x, AlgoSdk.Address value) => x.Address = value).Assign("error", (AlgoSdk.Kmd.ImportKeyResponse x) => x.Error, (ref AlgoSdk.Kmd.ImportKeyResponse x, AlgoSdk.Optional<System.Boolean> value) => x.Error = value).Assign("message", (AlgoSdk.Kmd.ImportKeyResponse x) => x.Message, (ref AlgoSdk.Kmd.ImportKeyResponse x, System.String value) => x.Message = value, AlgoSdk.StringComparer.Instance));
            return true;
        }
    }
}
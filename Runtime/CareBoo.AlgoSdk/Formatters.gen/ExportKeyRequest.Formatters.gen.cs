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
    
    
    public partial struct ExportKeyRequest
    {
        
        private static bool @__generated__IsValid = ExportKeyRequest.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Kmd.ExportKeyRequest>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Kmd.ExportKeyRequest>(false).Assign("address", (AlgoSdk.Kmd.ExportKeyRequest x) => x.Address, (ref AlgoSdk.Kmd.ExportKeyRequest x, AlgoSdk.Address value) => x.Address = value).Assign("wallet_handle_token", (AlgoSdk.Kmd.ExportKeyRequest x) => x.WalletHandleToken, (ref AlgoSdk.Kmd.ExportKeyRequest x, Unity.Collections.FixedString128Bytes value) => x.WalletHandleToken = value).Assign("wallet_password", (AlgoSdk.Kmd.ExportKeyRequest x) => x.WalletPassword, (ref AlgoSdk.Kmd.ExportKeyRequest x, Unity.Collections.FixedString128Bytes value) => x.WalletPassword = value));
            return true;
        }
    }
}

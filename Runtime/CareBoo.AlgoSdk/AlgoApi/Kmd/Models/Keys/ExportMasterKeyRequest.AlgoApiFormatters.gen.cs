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
    
    
    public partial struct ExportMasterKeyRequest
    {
        
        private static bool @__generated__IsValid = ExportMasterKeyRequest.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.ExportMasterKeyRequest>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.ExportMasterKeyRequest>().Assign("wallet_handle_token", null, (AlgoSdk.ExportMasterKeyRequest x) => x.WalletHandleToken, (ref AlgoSdk.ExportMasterKeyRequest x, Unity.Collections.FixedString128Bytes value) => x.WalletHandleToken = value, false).Assign("wallet_password", null, (AlgoSdk.ExportMasterKeyRequest x) => x.WalletPassword, (ref AlgoSdk.ExportMasterKeyRequest x, Unity.Collections.FixedString128Bytes value) => x.WalletPassword = value, false));
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.ExportMasterKeyRequest[]>(AlgoSdk.Formatters.ArrayFormatter<AlgoSdk.ExportMasterKeyRequest>.Instance);
            return true;
        }
    }
}
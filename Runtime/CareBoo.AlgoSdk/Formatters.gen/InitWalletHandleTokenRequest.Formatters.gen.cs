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
    
    
    public partial struct InitWalletHandleTokenRequest
    {
        
        private static bool @__generated__IsValid = InitWalletHandleTokenRequest.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Kmd.InitWalletHandleTokenRequest>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Kmd.InitWalletHandleTokenRequest>(false).Assign("wallet_id", (AlgoSdk.Kmd.InitWalletHandleTokenRequest x) => x.WalletId, (ref AlgoSdk.Kmd.InitWalletHandleTokenRequest x, Unity.Collections.FixedString128Bytes value) => x.WalletId = value).Assign("wallet_password", (AlgoSdk.Kmd.InitWalletHandleTokenRequest x) => x.WalletPassword, (ref AlgoSdk.Kmd.InitWalletHandleTokenRequest x, Unity.Collections.FixedString128Bytes value) => x.WalletPassword = value));
            return true;
        }
    }
}

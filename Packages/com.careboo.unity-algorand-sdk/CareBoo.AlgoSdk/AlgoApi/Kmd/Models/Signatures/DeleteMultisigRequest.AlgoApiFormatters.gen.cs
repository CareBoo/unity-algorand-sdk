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
    
    
    public partial struct DeleteMultisigRequest
    {
        
        private static bool @__generated__IsValid = DeleteMultisigRequest.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.DeleteMultisigRequest>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.DeleteMultisigRequest>().Assign("address", null, (AlgoSdk.DeleteMultisigRequest x) => x.Address, (ref AlgoSdk.DeleteMultisigRequest x, AlgoSdk.Address value) => x.Address = value, false).Assign("wallet_handle_token", null, (AlgoSdk.DeleteMultisigRequest x) => x.WalletHandleToken, (ref AlgoSdk.DeleteMultisigRequest x, Unity.Collections.FixedString128Bytes value) => x.WalletHandleToken = value, false).Assign("wallet_password", null, (AlgoSdk.DeleteMultisigRequest x) => x.WalletPassword, (ref AlgoSdk.DeleteMultisigRequest x, Unity.Collections.FixedString128Bytes value) => x.WalletPassword = value, false));
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.DeleteMultisigRequest[]>(AlgoSdk.Formatters.ArrayFormatter<AlgoSdk.DeleteMultisigRequest>.Instance);
            return true;
        }
    }
}

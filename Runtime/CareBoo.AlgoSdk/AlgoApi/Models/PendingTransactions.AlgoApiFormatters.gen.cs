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
    
    
    public partial struct PendingTransactions
    {
        
        private static bool @__generated__IsValid = PendingTransactions.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.PendingTransactions>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.PendingTransactions>().Assign("top-transactions", "top-transactions", (AlgoSdk.PendingTransactions x) => x.TopTransactions, (ref AlgoSdk.PendingTransactions x, AlgoSdk.SignedTxn[] value) => x.TopTransactions = value, AlgoSdk.ArrayComparer<AlgoSdk.SignedTxn>.Instance, false).Assign("total-transactions", "total-transactions", (AlgoSdk.PendingTransactions x) => x.TotalTransactions, (ref AlgoSdk.PendingTransactions x, System.UInt64 value) => x.TotalTransactions = value, false));
            return true;
        }
    }
}

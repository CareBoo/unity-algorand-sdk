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
    
    
    public partial struct TransactionGroup
    {
        
        private static bool @__generated__IsValid = TransactionGroup.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.TransactionGroup>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.TransactionGroup>().Assign("txlist", "txlist", (AlgoSdk.TransactionGroup x) => x.Txns, (ref AlgoSdk.TransactionGroup x, AlgoSdk.TransactionId[] value) => x.Txns = value, AlgoSdk.ArrayComparer<AlgoSdk.TransactionId>.Instance, false));
            return true;
        }
    }
}
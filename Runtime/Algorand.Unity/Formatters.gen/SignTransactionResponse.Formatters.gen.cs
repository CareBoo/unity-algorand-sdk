//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using UnityCollections = Unity.Collections;


namespace Algorand.Unity.Kmd
{
    
    
    public partial struct SignTransactionResponse
    {
        
        private static bool @__generated__IsValid = SignTransactionResponse.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            Algorand.Unity.AlgoApiFormatterLookup.Add<Algorand.Unity.Kmd.SignTransactionResponse>(new Algorand.Unity.AlgoApiObjectFormatter<Algorand.Unity.Kmd.SignTransactionResponse>(false).Assign("signed_transaction", (Algorand.Unity.Kmd.SignTransactionResponse x) => x.SignedTransaction, (ref Algorand.Unity.Kmd.SignTransactionResponse x, System.Byte[] value) => x.SignedTransaction = value, Algorand.Unity.ArrayComparer<System.Byte>.Instance).Assign("error", (Algorand.Unity.Kmd.SignTransactionResponse x) => x.Error, (ref Algorand.Unity.Kmd.SignTransactionResponse x, Algorand.Unity.Optional<System.Boolean> value) => x.Error = value).Assign("message", (Algorand.Unity.Kmd.SignTransactionResponse x) => x.Message, (ref Algorand.Unity.Kmd.SignTransactionResponse x, System.String value) => x.Message = value, Algorand.Unity.StringComparer.Instance));
            return true;
        }
    }
}
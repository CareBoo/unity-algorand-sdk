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


namespace Algorand.Unity.JsonRpc
{
    
    
    public partial struct JsonRpcError
    {
        
        private static bool @__generated__IsValid = JsonRpcError.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            Algorand.Unity.AlgoApiFormatterLookup.Add<Algorand.Unity.JsonRpc.JsonRpcError>(new Algorand.Unity.AlgoApiObjectFormatter<Algorand.Unity.JsonRpc.JsonRpcError>(false).Assign("code", (Algorand.Unity.JsonRpc.JsonRpcError x) => x.Code, (ref Algorand.Unity.JsonRpc.JsonRpcError x, System.Int32 value) => x.Code = value).Assign("message", (Algorand.Unity.JsonRpc.JsonRpcError x) => x.Message, (ref Algorand.Unity.JsonRpc.JsonRpcError x, System.String value) => x.Message = value, Algorand.Unity.StringComparer.Instance).Assign("data", (Algorand.Unity.JsonRpc.JsonRpcError x) => x.Data, (ref Algorand.Unity.JsonRpc.JsonRpcError x, Algorand.Unity.AlgoApiObject value) => x.Data = value));
            return true;
        }
    }
}
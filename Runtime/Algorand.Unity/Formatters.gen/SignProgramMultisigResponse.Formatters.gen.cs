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
    
    
    public partial struct SignProgramMultisigResponse
    {
        
        private static bool @__generated__IsValid = SignProgramMultisigResponse.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            Algorand.Unity.AlgoApiFormatterLookup.Add<Algorand.Unity.Kmd.SignProgramMultisigResponse>(new Algorand.Unity.AlgoApiObjectFormatter<Algorand.Unity.Kmd.SignProgramMultisigResponse>(false).Assign("multisig", (Algorand.Unity.Kmd.SignProgramMultisigResponse x) => x.SignedProgram, (ref Algorand.Unity.Kmd.SignProgramMultisigResponse x, System.Byte[] value) => x.SignedProgram = value, Algorand.Unity.ArrayComparer<System.Byte>.Instance).Assign("error", (Algorand.Unity.Kmd.SignProgramMultisigResponse x) => x.Error, (ref Algorand.Unity.Kmd.SignProgramMultisigResponse x, Algorand.Unity.Optional<System.Boolean> value) => x.Error = value).Assign("message", (Algorand.Unity.Kmd.SignProgramMultisigResponse x) => x.Message, (ref Algorand.Unity.Kmd.SignProgramMultisigResponse x, System.String value) => x.Message = value, Algorand.Unity.StringComparer.Instance));
            return true;
        }
    }
}
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
    
    
    public partial struct SignProgramResponse
    {
        
        private static bool @__generated__IsValid = SignProgramResponse.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Kmd.SignProgramResponse>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Kmd.SignProgramResponse>(false).Assign("sig", (AlgoSdk.Kmd.SignProgramResponse x) => x.SignedProgram, (ref AlgoSdk.Kmd.SignProgramResponse x, AlgoSdk.Sig value) => x.SignedProgram = value).Assign("error", (AlgoSdk.Kmd.SignProgramResponse x) => x.Error, (ref AlgoSdk.Kmd.SignProgramResponse x, AlgoSdk.Optional<System.Boolean> value) => x.Error = value).Assign("message", (AlgoSdk.Kmd.SignProgramResponse x) => x.Message, (ref AlgoSdk.Kmd.SignProgramResponse x, System.String value) => x.Message = value, AlgoSdk.StringComparer.Instance));
            return true;
        }
    }
}

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
    
    
    public partial struct AppEvalDelta
    {
        
        private static bool @__generated__IsValid = AppEvalDelta.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.AppEvalDelta>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.AppEvalDelta>().Assign("gd", "gd", (AlgoSdk.AppEvalDelta x) => x.GlobalDelta, (ref AlgoSdk.AppEvalDelta x, AlgoSdk.AppStateDelta value) => x.GlobalDelta = value, false).Assign("ld", "ld", (AlgoSdk.AppEvalDelta x) => x.LocalDeltas, (ref AlgoSdk.AppEvalDelta x, AlgoSdk.AppStateDelta[] value) => x.LocalDeltas = value, AlgoSdk.ArrayComparer<AlgoSdk.AppStateDelta>.Instance, false).Assign("lg", "lg", (AlgoSdk.AppEvalDelta x) => x.Logs, (ref AlgoSdk.AppEvalDelta x, System.String[] value) => x.Logs = value, AlgoSdk.ArrayComparer<string, AlgoSdk.StringComparer>.Instance, false).Assign("itx", "itx", (AlgoSdk.AppEvalDelta x) => x.InnerTxns, (ref AlgoSdk.AppEvalDelta x, AlgoSdk.AppliedSignedTxn[] value) => x.InnerTxns = value, AlgoSdk.ArrayComparer<AlgoSdk.AppliedSignedTxn>.Instance, false));
            return true;
        }
    }
}
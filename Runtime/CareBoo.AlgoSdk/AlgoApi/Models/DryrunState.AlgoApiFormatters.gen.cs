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
    
    
    public partial struct DryrunState
    {
        
        private static bool @__generated__IsValid = DryrunState.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.DryrunState>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.DryrunState>().Assign("error", null, (AlgoSdk.DryrunState x) => x.Error, (ref AlgoSdk.DryrunState x, Unity.Collections.FixedString128Bytes value) => x.Error = value, false).Assign("line", null, (AlgoSdk.DryrunState x) => x.Line, (ref AlgoSdk.DryrunState x, System.UInt64 value) => x.Line = value, false).Assign("pc", null, (AlgoSdk.DryrunState x) => x.ProgramCounter, (ref AlgoSdk.DryrunState x, System.UInt64 value) => x.ProgramCounter = value, false).Assign("scratch", null, (AlgoSdk.DryrunState x) => x.Scratch, (ref AlgoSdk.DryrunState x, AlgoSdk.TealValue[] value) => x.Scratch = value, AlgoSdk.ArrayComparer<AlgoSdk.TealValue>.Instance, false).Assign("stack", null, (AlgoSdk.DryrunState x) => x.Stack, (ref AlgoSdk.DryrunState x, AlgoSdk.TealValue[] value) => x.Stack = value, AlgoSdk.ArrayComparer<AlgoSdk.TealValue>.Instance, false));
            return true;
        }
    }
}
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
    
    
    public partial struct CatchupMessage
    {
        
        private static bool @__generated__IsValid = CatchupMessage.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.CatchupMessage>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.CatchupMessage>().Assign("catchup-message", null, (AlgoSdk.CatchupMessage x) => x.Message, (ref AlgoSdk.CatchupMessage x, Unity.Collections.FixedString512Bytes value) => x.Message = value, false));
            return true;
        }
    }
}
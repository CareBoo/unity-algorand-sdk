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
    
    
    public partial struct Address
    {
        
        private static bool @__generated__IsValid = Address.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Address>(new AlgoSdk.Formatters.AddressFormatter());
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Address[]>(AlgoSdk.Formatters.ArrayFormatter<AlgoSdk.Address>.Instance);
            return true;
        }
        
        public partial struct CheckSum
        {
            
            private static bool @__generated__IsValid = CheckSum.@__generated__InitializeAlgoApiFormatters();
            
            private static bool @__generated__InitializeAlgoApiFormatters()
            {
                return false;
            }
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlgoSdk.Abi
{
    
    
    public partial struct Method
    {
        
        private static bool @__generated__IsValid = Method.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Abi.Method>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Abi.Method>(false).Assign("name", (AlgoSdk.Abi.Method x) => x.Name, (ref AlgoSdk.Abi.Method x, System.String value) => x.Name = value, AlgoSdk.StringComparer.Instance).Assign("desc", (AlgoSdk.Abi.Method x) => x.Description, (ref AlgoSdk.Abi.Method x, System.String value) => x.Description = value, AlgoSdk.StringComparer.Instance).Assign("args", (AlgoSdk.Abi.Method x) => x.Arguments, (ref AlgoSdk.Abi.Method x, AlgoSdk.Abi.Method.Arg[] value) => x.Arguments = value, AlgoSdk.ArrayComparer<AlgoSdk.Abi.Method.Arg>.Instance).Assign("returns", (AlgoSdk.Abi.Method x) => x.Returns, (ref AlgoSdk.Abi.Method x, AlgoSdk.Abi.Method.Return value) => x.Returns = value));
            return true;
        }
        
        public partial struct Arg
        {
            
            private static bool @__generated__IsValid = Arg.@__generated__InitializeAlgoApiFormatters();
            
            private static bool @__generated__InitializeAlgoApiFormatters()
            {
                AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Abi.Method.Arg>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Abi.Method.Arg>(false).Assign("type", (AlgoSdk.Abi.Method.Arg x) => x.Type, (ref AlgoSdk.Abi.Method.Arg x, System.String value) => x.Type = value, AlgoSdk.StringComparer.Instance).Assign("name", (AlgoSdk.Abi.Method.Arg x) => x.Name, (ref AlgoSdk.Abi.Method.Arg x, System.String value) => x.Name = value, AlgoSdk.StringComparer.Instance).Assign("desc", (AlgoSdk.Abi.Method.Arg x) => x.Description, (ref AlgoSdk.Abi.Method.Arg x, System.String value) => x.Description = value, AlgoSdk.StringComparer.Instance));
                return true;
            }
        }
        
        public partial struct Return
        {
            
            private static bool @__generated__IsValid = Return.@__generated__InitializeAlgoApiFormatters();
            
            private static bool @__generated__InitializeAlgoApiFormatters()
            {
                AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Abi.Method.Return>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Abi.Method.Return>(false).Assign("type", (AlgoSdk.Abi.Method.Return x) => x.Type, (ref AlgoSdk.Abi.Method.Return x, System.String value) => x.Type = value, AlgoSdk.StringComparer.Instance).Assign("desc", (AlgoSdk.Abi.Method.Return x) => x.Description, (ref AlgoSdk.Abi.Method.Return x, System.String value) => x.Description = value, AlgoSdk.StringComparer.Instance));
                return true;
            }
        }
    }
}

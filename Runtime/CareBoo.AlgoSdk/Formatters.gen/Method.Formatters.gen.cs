//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlgoSdk.Experimental.Abi
{
    
    
    public partial struct Method
    {
        
        private static bool @__generated__IsValid = Method.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Experimental.Abi.Method>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Experimental.Abi.Method>(false).Assign("name", (AlgoSdk.Experimental.Abi.Method x) => x.Name, (ref AlgoSdk.Experimental.Abi.Method x, System.String value) => x.Name = value, AlgoSdk.StringComparer.Instance).Assign("desc", (AlgoSdk.Experimental.Abi.Method x) => x.Description, (ref AlgoSdk.Experimental.Abi.Method x, System.String value) => x.Description = value, AlgoSdk.StringComparer.Instance).Assign("args", (AlgoSdk.Experimental.Abi.Method x) => x.Arguments, (ref AlgoSdk.Experimental.Abi.Method x, AlgoSdk.Experimental.Abi.Method.Arg[] value) => x.Arguments = value, AlgoSdk.ArrayComparer<AlgoSdk.Experimental.Abi.Method.Arg>.Instance).Assign("returns", (AlgoSdk.Experimental.Abi.Method x) => x.Returns, (ref AlgoSdk.Experimental.Abi.Method x, AlgoSdk.Experimental.Abi.Method.Return value) => x.Returns = value));
            return true;
        }
        
        public partial struct Arg
        {
            
            private static bool @__generated__IsValid = Arg.@__generated__InitializeAlgoApiFormatters();
            
            private static bool @__generated__InitializeAlgoApiFormatters()
            {
                AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Experimental.Abi.Method.Arg>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Experimental.Abi.Method.Arg>(false).Assign("type", (AlgoSdk.Experimental.Abi.Method.Arg x) => x.Type, (ref AlgoSdk.Experimental.Abi.Method.Arg x, AlgoSdk.Experimental.Abi.IAbiType value) => x.Type = value, AlgoSdk.IAbiTypeComparer.Instance).Assign("name", (AlgoSdk.Experimental.Abi.Method.Arg x) => x.Name, (ref AlgoSdk.Experimental.Abi.Method.Arg x, System.String value) => x.Name = value, AlgoSdk.StringComparer.Instance).Assign("desc", (AlgoSdk.Experimental.Abi.Method.Arg x) => x.Description, (ref AlgoSdk.Experimental.Abi.Method.Arg x, System.String value) => x.Description = value, AlgoSdk.StringComparer.Instance));
                return true;
            }
        }
        
        public partial struct Return
        {
            
            private static bool @__generated__IsValid = Return.@__generated__InitializeAlgoApiFormatters();
            
            private static bool @__generated__InitializeAlgoApiFormatters()
            {
                AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.Experimental.Abi.Method.Return>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.Experimental.Abi.Method.Return>(false).Assign("type", (AlgoSdk.Experimental.Abi.Method.Return x) => x.Type, (ref AlgoSdk.Experimental.Abi.Method.Return x, AlgoSdk.Experimental.Abi.IAbiType value) => x.Type = value, AlgoSdk.IAbiTypeComparer.Instance).Assign("desc", (AlgoSdk.Experimental.Abi.Method.Return x) => x.Description, (ref AlgoSdk.Experimental.Abi.Method.Return x, System.String value) => x.Description = value, AlgoSdk.StringComparer.Instance));
                return true;
            }
        }
    }
}

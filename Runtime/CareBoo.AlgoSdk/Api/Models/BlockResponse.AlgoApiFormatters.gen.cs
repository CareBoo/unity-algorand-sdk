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


    public partial struct BlockResponse
    {

        private static bool @__generated__IsValid = BlockResponse.@__generated__InitializeAlgoApiFormatters();

        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.BlockResponse>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.BlockResponse>(false).Assign("block", (AlgoSdk.BlockResponse x) => x.BlockHeader, (ref AlgoSdk.BlockResponse x, AlgoSdk.BlockHeader value) => x.BlockHeader = value).Assign("cert", (AlgoSdk.BlockResponse x) => x.Cert, (ref AlgoSdk.BlockResponse x, AlgoSdk.AlgoApiObject value) => x.Cert = value));
            return true;
        }
    }
}

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


namespace Algorand.Unity.WalletConnect.Core
{
    
    
    public partial struct Participant
    {
        
        private static bool @__generated__IsValid = Participant.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            Algorand.Unity.AlgoApiFormatterLookup.Add<Algorand.Unity.WalletConnect.Core.Participant>(new Algorand.Unity.AlgoApiObjectFormatter<Algorand.Unity.WalletConnect.Core.Participant>(false).Assign("publicKey", (Algorand.Unity.WalletConnect.Core.Participant x) => x.PublicKey, (ref Algorand.Unity.WalletConnect.Core.Participant x, System.String value) => x.PublicKey = value, Algorand.Unity.StringComparer.Instance).Assign("metadata", (Algorand.Unity.WalletConnect.Core.Participant x) => x.Metadata, (ref Algorand.Unity.WalletConnect.Core.Participant x, Algorand.Unity.WalletConnect.Core.Metadata value) => x.Metadata = value));
            return true;
        }
    }
}
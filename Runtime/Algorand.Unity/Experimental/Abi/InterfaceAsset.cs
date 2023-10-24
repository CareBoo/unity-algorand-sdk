using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    [HelpURL(DocUrl.Api + "Algorand.Unity.Experimental.Abi.InterfaceAsset.html")]
    [CreateAssetMenu(menuName = "Algorand/Experimental/Abi/Interface")]
    public class InterfaceAsset : ScriptableObject
    {
        public Interface @interface;

        public static implicit operator Interface(InterfaceAsset asset)
        {
            return asset.@interface;
        }
    }
}

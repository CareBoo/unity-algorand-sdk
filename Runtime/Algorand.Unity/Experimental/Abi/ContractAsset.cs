using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    [HelpURL(DocUrl.Api + "Algorand.Unity.Experimental.Abi.ContractAsset.html")]
    [CreateAssetMenu(menuName = "Algorand/Experimental/Abi Contract")]
    public class ContractAsset : ScriptableObject
    {
        public Contract contract;

        public static implicit operator Contract(ContractAsset asset)
        {
            return asset.contract;
        }
    }
}

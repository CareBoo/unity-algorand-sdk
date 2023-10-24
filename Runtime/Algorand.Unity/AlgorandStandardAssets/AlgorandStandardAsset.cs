using UnityEngine;

namespace Algorand.Unity
{
    [HelpURL(DocUrl.Api + "Algorand.Unity.AlgorandStandardAsset.html")]
    [CreateAssetMenu(menuName = "Algorand/ASA")]
    public class AlgorandStandardAsset : ScriptableObject
    {
        public AssetIndex index;
        public AlgorandNetwork network;
        public AssetParams assetParams;
    }
}

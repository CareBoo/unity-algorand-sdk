using UnityEngine;

namespace Algorand.Unity.Samples.CreatingAsas
{
    [CreateAssetMenu]
    public class AssetObject : ScriptableObject
    {
        public AssetIndex Index;
        public AlgorandNetwork Network;
        public AssetParams Params;
    }
}
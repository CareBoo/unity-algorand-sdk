using UnityEngine;

namespace Algorand.Unity
{
    [CreateAssetMenu(menuName = "Algorand/Algod Client")]
    public class AlgodClientAsset : ScriptableObject
    {
        public AlgodClient client;
    }
}

using UnityEngine;

namespace Algorand.Unity
{
    [CreateAssetMenu(menuName = "Algorand/Kmd Client")]
    public class KmdClientAsset : ScriptableObject
    {
        public KmdClient client;
    }
}

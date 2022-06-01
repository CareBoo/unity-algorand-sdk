using UnityEngine;

namespace AlgoSdk
{
    [CreateAssetMenu(fileName = "NewKmdClient", menuName = "AlgoSdk/Clients/KmdClient")]
    public class KmdClientAsset : ScriptableObject
    {
        public KmdClient Client;
    }
}

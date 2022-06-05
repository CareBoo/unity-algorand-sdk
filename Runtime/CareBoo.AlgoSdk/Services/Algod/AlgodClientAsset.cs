using UnityEngine;

namespace AlgoSdk
{
    [CreateAssetMenu(fileName = "NewAlgodClient", menuName = "AlgoSdk/Clients/AlgodClient")]
    public class AlgodClientAsset : ScriptableObject
    {
        public AlgodClient Client;
    }
}

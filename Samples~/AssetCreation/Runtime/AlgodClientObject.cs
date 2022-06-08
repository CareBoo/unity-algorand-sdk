using UnityEngine;

namespace AlgoSdk
{
    [CreateAssetMenu(fileName = "NewAlgodClient", menuName = "AlgoSdk/Clients/AlgodClient")]
    public class AlgodClientObject : ScriptableObject
    {
        public AlgodClient Client;

        public AlgorandNetwork Network;
    }
}

using UnityEngine;

namespace AlgoSdk
{
    [CreateAssetMenu(fileName = "NewIndexerClient", menuName = "AlgoSdk/Clients/IndexerClient")]
    public class IndexerClientAsset : ScriptableObject
    {
        public IndexerClient Client;
    }
}

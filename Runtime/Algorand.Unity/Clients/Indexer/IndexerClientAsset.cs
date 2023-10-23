using UnityEngine;

namespace Algorand.Unity
{
    [CreateAssetMenu(menuName = "Algorand/Indexer Client")]
    public class IndexerClientAsset : ScriptableObject
    {
        public IndexerClient client;
    }
}

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Algorand.Unity
{
    [HelpURL(DocUrl.Api + "Algorand.Unity.IndexerClientAsset.html")]
    [CreateAssetMenu(menuName = "Algorand/Indexer Client")]
    public class IndexerClientAsset : ScriptableObject
    {
        public AlgorandNetwork network;

        [ContextMenuItem("Test Connection", nameof(TestConnection))]
        public IndexerClient client;

        private void TestConnection()
        {
            TestConnectionAsync().Forget();
        }

        private async UniTaskVoid TestConnectionAsync()
        {
            var (error, resultWrapped) = await client.MakeHealthCheck();
            var result = resultWrapped.WrappedValue;

            if (error) Debug.LogError(error);
            else
            {
                Debug.Log($"Indexer connection healthy. Version: {result.Version}");
                if (result.Errors != null && result.Errors.Length > 0)
                {
                    for (var i = 0; i < result.Errors.Length; i++)
                    {
                        Debug.LogWarning(result.Errors[i]);
                    }
                    Debug.LogWarning($"Got {result.Errors.Length} errors from the indexer.");
                }
            }
        }
    }
}

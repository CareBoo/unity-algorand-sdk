using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Algorand.Unity
{
    [HelpURL(DocUrl.Api + "Algorand.Unity.AlgodClientAsset.html")]
    [CreateAssetMenu(menuName = "Algorand/Algod Client")]
    public class AlgodClientAsset : ScriptableObject
    {
        public AlgorandNetwork network;

        [ContextMenuItem("Test Connection", nameof(TestConnection))]
        public AlgodClient client;

        private void TestConnection()
        {
            TestConnectionAsync().Forget();
        }

        private async UniTaskVoid TestConnectionAsync()
        {
            var result = await client.HealthCheck();
            if (result.Error) Debug.LogError(result.Error);
            else Debug.Log("Algod connection healthy.");
        }
    }
}

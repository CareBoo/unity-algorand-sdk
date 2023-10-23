using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Algorand.Unity
{
    [HelpURL(DocUrl.Api + "Algorand.Unity.KmdClientAsset.html")]
    [CreateAssetMenu(menuName = "Algorand/Kmd Client")]
    public class KmdClientAsset : ScriptableObject
    {
        [ContextMenuItem("Test Connection", nameof(TestConnection))]
        public KmdClient client;

        private void TestConnection()
        {
            TestConnectionAsync().Forget();
        }

        private async UniTaskVoid TestConnectionAsync()
        {
            var (error, result) = await client.Versions();

            if (error) Debug.LogError(error);
            else Debug.Log($"Kmd connection healthy. Versions: [{string.Join(", ", result.Versions)}]");
        }
    }
}

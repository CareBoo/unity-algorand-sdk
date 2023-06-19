using Algorand.Unity.Indexer;
using Algorand.Unity.NftViewer;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Algorand.Unity.Samples.NftViewer
{
    public class NftViewer : MonoBehaviour
    {
        public string algoClientNetworkAddress = "https://testnet-api.algonode.cloud";

        public string indexerNetworkAddress = "https://testnet-idx.algonode.cloud";

        public Transform contentTransform;

        [FormerlySerializedAs("NftDisplayBoxPrefab")]
        public GameObject nftDisplayBoxPrefab;

        private AlgodClient algod;

        private string algodHealth;

        private IndexerClient indexer;

        private string indexerHealth;

        private string textureStatus;

        public string PublicKey { get; set; } = "HAE4ZMZI2TKTFNES33PNWC5RIK7MUOISQDRQ7LMWLKNNGMA7V5NZOGHJVY";

        private void Start()
        {
            algod = new AlgodClient(algoClientNetworkAddress);
            indexer = new IndexerClient(indexerNetworkAddress);

            CheckAlgodStatus().Forget();
            CheckIndexerStatus().Forget();
        }

        public async UniTaskVoid CheckAlgodStatus()
        {
            var response = await algod.HealthCheck();
            if (response.Error) algodHealth = response.Error;
            else algodHealth = "Connected";
        }

        public async UniTaskVoid CheckIndexerStatus()
        {
            var response = await indexer.MakeHealthCheck();
            indexerHealth = response.Error ? response.Error : "Connected";
        }

        public void LoadNFTs()
        {
            Debug.Log("loading NFTs");
            if (algodHealth == "Connected" && indexerHealth == "Connected")
                GetNFTs().Forget();
        }

        public async UniTaskVoid GetNFTs()
        {
            //clear the box
            foreach (Transform t in contentTransform) Destroy(t.gameObject);

            var (err, resp) = await indexer.SearchForAssets(creator: PublicKey);
            if (err)
            {
                Debug.LogError(err);
                return;
            }

            Debug.Log("getting ARC-0069 tokens");
            using var disposeWebRequests = ListPool<(UnityWebRequestAsyncOperation, Asset)>.Get(out var webRequests);
            foreach (var asset in resp.Assets)
            {
                var url = asset.Params.Url;
                if (string.IsNullOrEmpty(url) || !IsIpfsUrl(asset.Params.Url))
                {
                    Debug.LogWarning(
                        $"asset id: {asset.Index} does not have an IPFS url which is required to view the asset in this sample.");
                    continue;
                }

                if (asset.IsArc0003())
                {
                    Debug.LogWarning(
                        $"asset id: {asset.Index} is an arc3 asset which is not supported in this sample.");
                    continue;
                }

                url = FormatIpfsUrl(url);
                var sent = UnityWebRequestTexture.GetTexture(url, true).SendWebRequest();
                webRequests.Add((sent, asset));
            }

            foreach (var (sent, asset) in webRequests)
            {
                var webRequest = sent.webRequest;
                try
                {
                    await sent;
                }
                catch (UnityWebRequestException)
                {
                    if (webRequest.downloadHandler.error == "Failed to create texture from downloaded bytes")
                        Debug.LogError(
                            $"Asset {asset.Index} does not have a valid image located at its url, {webRequest.url}. Please make sure it is either a png or jpeg file.");
                    else
                        Debug.LogError(webRequest.downloadHandler.error);
                    continue;
                }

                if (webRequest.result == UnityWebRequest.Result.Success &&
                    webRequest.downloadHandler is DownloadHandlerTexture textureDownload)
                {
                    Texture nftTexture = textureDownload.texture;

                    var displayBox = Instantiate(nftDisplayBoxPrefab, contentTransform);
                    displayBox.GetComponent<NftDisplayBox>().SetFields(
                        nftTexture,
                        $"Name: {asset.Params.Name}",
                        $"ID: {asset.Index}");
                }
                else
                {
                    Debug.LogError(webRequest.error);
                }
            }
        }

        private static bool IsIpfsUrl(string url)
        {
            return url.StartsWith("ipfs://") || url.StartsWith("https://ifps.io/ipfs/");
        }

        private static string FormatIpfsUrl(string url)
        {
            return url.Replace("ipfs://", "https://ipfs.io/ipfs/");
        }
    }
}

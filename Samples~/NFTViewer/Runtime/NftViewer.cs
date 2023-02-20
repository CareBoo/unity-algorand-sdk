using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace Algorand.Unity.Samples.NftViewer
{
    public class NftViewer : MonoBehaviour
    {
        public string algoClientNetworkAddress = "https://node.testnet.algoexplorerapi.io";

        public string indexerNetworkAddress = "https://algoindexer.testnet.algoexplorerapi.io";

        public Transform contentTransform;

        [FormerlySerializedAs("NftDisplayBoxPrefab")]
        public GameObject nftDisplayBoxPrefab;

        private AlgodClient algod;

        private IndexerClient indexer;

        private string algodHealth;

        private string indexerHealth;

        private string textureStatus;

        public string PublicKey { get; set; } = "HAE4ZMZI2TKTFNES33PNWC5RIK7MUOISQDRQ7LMWLKNNGMA7V5NZOGHJVY";

        public List<Texture> NftTextures = new List<Texture>();

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
            foreach (Transform t in contentTransform)
            {
                Destroy(t.gameObject);
            }

            var (err, resp) = await indexer.SearchForAssets(creator: PublicKey);
            Debug.Log("getting NFTs");
            foreach (Indexer.Asset asset in resp.Assets)
            {
                if (!asset.Params.Url.Contains("ipfs"))
                    continue;
                else
                    Debug.Log("This is an NFT");

                string url = asset.Params.Url;

                if (url == "")
                    continue;

                url = FormatNftURL(url);

                UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

                Debug.Log("sending a web request");
                await www.SendWebRequest();
                Debug.Log("request sent");
                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Texture nftTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    Debug.Log("The Texture is loaded");
                    NftTextures.Add(nftTexture);

                    GameObject displayBox = Instantiate(nftDisplayBoxPrefab, contentTransform);
                    displayBox.GetComponent<NftDisplayBox>().SetFields(
                        nftTexture,
                        $"Name: {asset.Params.Name}",
                        $"ID: {asset.Index}");
                }

                Debug.Log("Done loading");
            }
        }

        private string FormatNftURL(string url)
        {
            return url.Replace("ipfs://", "https://ipfs.io/ipfs/");
        }
    }
}
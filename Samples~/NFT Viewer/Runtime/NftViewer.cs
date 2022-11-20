using AlgoSdk;
using AlgoSdk.Crypto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AlgoSdk.LowLevel;
using UnityEngine.Networking;

namespace AlgoSdk.Samples.NftViewer
{
    public class NftViewer : MonoBehaviour
    {
        [SerializeField] private string algoClientNetworkAddress = "https://node.testnet.algoexplorerapi.io";
        [SerializeField] private string indexerNetworkAddress = "https://algoindexer.testnet.algoexplorerapi.io";

        private AlgodClient algod;

        private IndexerClient indexer;
        private string algodHealth;
        private string indexerHealth;
        private string textureStatus;

        private Address address;
        public string Publickey { get { return publicKey; } set { publicKey = value; } }
        private string publicKey = "HAE4ZMZI2TKTFNES33PNWC5RIK7MUOISQDRQ7LMWLKNNGMA7V5NZOGHJVY";

        public List<Texture> NftTextures = new List<Texture>();

        [SerializeField] private Transform contentTransform;
        [SerializeField] private GameObject NftDisplayBoxPrefab;

        private void Start()
        {
            algod = new AlgodClient(algoClientNetworkAddress);
            indexer = new IndexerClient(indexerNetworkAddress);

            CheckAlgodStatus().Forget();
            CheckIndexerStatus().Forget();

            address = publicKey;
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
            if (response.Error) indexerHealth = response.Error;
            else indexerHealth = "Connected";
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

            var (err, resp) = await indexer.SearchForAssets(creator: address);
            Debug.Log("getting NFTs");
            foreach (AlgoSdk.Indexer.Asset asset in resp.Assets)
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
                    Texture NftTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    Debug.Log("The Texture is loaded");
                    NftTextures.Add(NftTexture);

                    GameObject displayBox = Instantiate(NftDisplayBoxPrefab, contentTransform);
                    displayBox.GetComponent<NftDisplayBox>().SetFields(NftTexture, "Name: " + asset.Params.Name.ToString(), "ID: " + asset.Index.ToString());
                }

                Debug.Log("Done loading");
            }
        }

        private string FormatNftURL(string url)
        {
            //remove the ipfs extension
            url = url.Replace("ipfs://", "");

            // add https call so that it can be loaded with a web request
            url = "https://ipfs.io/ipfs/" + url;

            return url;
        }
    }
}

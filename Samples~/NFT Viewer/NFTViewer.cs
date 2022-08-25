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

public class NFTViewer : MonoBehaviour
{

    [SerializeField] string algoClientNetworkAddress = "https://node.testnet.algoexplorerapi.io";
    [SerializeField] string indexerNetworkAddress = "https://algoindexer.testnet.algoexplorerapi.io";

    AlgodClient algod;

    IndexerClient indexer;
    string algodHealth;
    string indexerHealth;
    string textureStatus;
    

    Address address;
    public string Publickey { get { return publicKey; } set { publicKey = value; } }
    string publicKey = "HAE4ZMZI2TKTFNES33PNWC5RIK7MUOISQDRQ7LMWLKNNGMA7V5NZOGHJVY";

    public List<Texture> NftTextures = new List<Texture>();


    [SerializeField] Transform contentTransform;
    [SerializeField] GameObject NftDisplayBoxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        algod = new AlgodClient(algoClientNetworkAddress);
        indexer = new IndexerClient(indexerNetworkAddress);

        CheckAlgodStatus().Forget();
        CheckIndexerStatus().Forget();


        //byte[] bytes = Encoding.ASCII.GetBytes(publicKey);

        //Ed25519.PublicKey publicKy = new Ed25519.PublicKey();
        //int i = 0;
        
        
        

        //foreach (byte b in bytes)
        //{
        //    publicKy.SetByteAt(i, b);
        //    i++;
        //}



        //address = Address.FromPublicKey(publicKy);

        address = publicKey;
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void addToTextureQue()
    {

    }

    public void loadNFTs()
    {
        Debug.Log("loading NFTs");
        if (algodHealth == "Connected" && indexerHealth == "Connected")
            getNFTs().Forget();
    }


    public async UniTaskVoid getNFTs()
    {
        //clear the box
        foreach(Transform t in contentTransform)
        {
            Destroy(t.gameObject);
        }    

        var (err, resp) = await indexer.GetAssets(creator: address);
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

            url = formatNftURL(url);

            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

            Debug.Log("sending a web request");
            await www.SendWebRequest();
            Debug.Log("request sent");
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                //nftLoadError = true;
                Debug.Log(www.error);
            }
            else
            {
                Texture NftTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Debug.Log("The Texture is loaded");
                NftTextures.Add(NftTexture);

                GameObject displayBox = Instantiate(NftDisplayBoxPrefab, contentTransform);
                displayBox.GetComponent<NftDisplayBox>().setFields(NftTexture, "Name: " + asset.Params.Name.ToString(), "ID: " + asset.Index.ToString());

            }


            //await (asset.getNftTexture());

            //while (!asset.NftTextureLoaded)// || !asset.NftLoadError)
            //    await UniTask.Delay(100);

            //if (asset.NftTextureLoaded)
            //{
            //    Debug.Log("Setting NFT");
            //    NftTextures.Add(asset.NftTexture);
            //    Debug.Log("Finished Setting NFT");
            //}
            //else
            //{
            //    Debug.Log("NFT not loaded");
            //}


            Debug.Log("Done loading");

        }
        // add an is nft helper function
        
        //look into a web view for wallet connect
    }

    string formatNftURL(string url)
    {

        //remove the ipfs extension
        url = url.Replace("ipfs://", "");

        // add https call so that it can be loaded with a web request
        url = "https://ipfs.io/ipfs/" + url;


        return url;


    }

}

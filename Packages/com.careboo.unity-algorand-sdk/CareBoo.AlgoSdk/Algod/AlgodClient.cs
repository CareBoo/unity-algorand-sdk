using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace AlgoSdk
{
    public partial struct AlgodClient
    {
        public async UniTask<string> GetAsync(string url)
        {
            var downloadHandler = new DownloadHandlerBuffer();
            var webrequest = new UnityWebRequest()
            {
                url = url,
                downloadHandler = downloadHandler
            };
            await webrequest.SendWebRequest();
            return downloadHandler.text;
        }
    }
}

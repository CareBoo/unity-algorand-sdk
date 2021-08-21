using System;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine.Networking;
using static Cysharp.Threading.Tasks.UnityAsyncExtensions;

namespace AlgoSdk
{
    public partial struct AlgodClient
    {
        readonly string address;
        readonly string token;

        const string TokenHeader = "X-Algo-API-Token";

        public AlgodClient(string address, string token)
        {
            this.address = address;
            this.token = token;
        }

        private async UniTask<UnityWebRequest> Request(string endpoint)
        {

            var downloadHandler = new DownloadHandlerBuffer();
            var webRequest = new UnityWebRequest()
            {
                url = $"{address}/{endpoint}",
                downloadHandler = downloadHandler
            };
            webRequest.SetRequestHeader(TokenHeader, token);
            return await webRequest.SendWebRequest();
        }

        public async UniTask<Response> GetAsync(string endpoint)
        {
            var unityWebRequest = new UnityWebRequest()
            {
                method = UnityWebRequest.kHttpVerbGET,
                url = $"{address}/{endpoint}",
                downloadHandler = new DownloadHandlerBuffer(),
            };
            var request = new Request(unityWebRequest);
            return await request.Send();
        }
    }
}

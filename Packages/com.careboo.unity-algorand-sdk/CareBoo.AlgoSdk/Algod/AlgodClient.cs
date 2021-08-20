using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

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

        public async UniTask<string> GetTextAsync(string endpoint)
        {
            using var webRequest = await Request(endpoint);
            return webRequest.downloadHandler.text;
        }

        public async UniTask<byte[]> GetRawAsync(string endpoint)
        {
            using var webRequest = await Request(endpoint);
            return webRequest.downloadHandler.data;
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
            try
            {
                var request = await Request(endpoint);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log(ex);
            }
            throw new NotImplementedException();
        }
    }
}

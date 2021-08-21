using Cysharp.Threading.Tasks;
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

        public async UniTask<Response> GetAsync(string endpoint)
        {
            var unityWebRequest = UnityWebRequest.Get(endpoint);
            var request = new Request(unityWebRequest);
            return await request.Send();
        }
    }
}

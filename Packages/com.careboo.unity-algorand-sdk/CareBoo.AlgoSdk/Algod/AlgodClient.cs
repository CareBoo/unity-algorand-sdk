using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace AlgoSdk
{
    public partial struct AlgodClient
    {
        readonly string address;
        readonly string token;

        public AlgodClient(string address, string token)
        {
            this.address = address;
            this.token = token;
        }

        public async UniTask<AlgoApiResponse> GetAsync(string endpoint)
        {
            return await AlgoApiRequest.Get(token, GetUrl(endpoint)).Send();
        }

        public async UniTask<AlgoApiResponse> PostAsync(string endpoint, string postData)
        {
            return await AlgoApiRequest.Post(token, GetUrl(endpoint), postData).Send();
        }

        public async UniTask<AlgoApiResponse> DeleteAsync(string endpoint)
        {
            return await AlgoApiRequest.Delete(token, GetUrl(endpoint)).Send();
        }

        public string GetUrl(string endpoint)
        {
            return $"{address}/{endpoint}";
        }
    }
}

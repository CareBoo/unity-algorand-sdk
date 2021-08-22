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

        public async UniTask<AlgoApiResponse> GetGenesisInformation()
        {
            return await GetAsync("/genesis");
        }

        public async UniTask<AlgoApiResponse> GetHealth()
        {
            return await GetAsync("/health");
        }

        public async UniTask<AlgoApiResponse> GetMetrics()
        {
            return await GetAsync("/metrics");
        }

        public async UniTask<AlgoApiResponse> GetSwaggerSpec()
        {
            return await GetAsync("/swagger.json");
        }

        public async UniTask<AlgoApiResponse<Account>> GetAccountInformation(Address accountAddress)
        {
            return await GetAsync($"/v2/accounts/{accountAddress}");
        }

        public async UniTask<AlgoApiResponse<PendingTransactions>> GetPendingTransactions(Address accountAddress)
        {
            return await GetAsync($"/v2/accounts/{accountAddress}/transactions/pending");
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
            return address + endpoint;
        }
    }
}

using Cysharp.Threading.Tasks;

namespace AlgoSdk
{
    public struct IndexerClient : IIndexerClient
    {
        readonly string address;
        readonly string token;

        public IndexerClient(string address, string token)
        {
            this.address = address.TrimEnd('/');
            this.token = token;
        }

        public string Address => address;

        public string Token => token;

        public async UniTask<AlgoApiResponse<AccountResponse>> GetAccount(ulong accountId, AccountQuery query = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<AccountsResponse>> GetAccounts(AccountsQuery query = default)
        {
            return await this.GetAsync("/v2/accounts", query);
        }

        public async UniTask<AlgoApiResponse<TransactionsResponse>> GetAccountTransactions(ulong accountId, TransactionsQuery query = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<ApplicationResponse>> GetApplication(ulong applicationId, ApplicationsQuery query = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<ApplicationsResponse>> GetApplications(ApplicationsQuery query = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<AssetResponse>> GetAsset(ulong assetId, AssetQuery query = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<BalancesResponse>> GetAssetBalances(ulong assetId, BalancesQuery query = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<AssetsResponse>> GetAssets(AssetsQuery query = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<TransactionsResponse>> GetAssetTransactions(ulong assetId, TransactionsQuery query = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<Block>> GetBlock(ulong round)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<HealthCheck>> GetHealth()
        {
            return await this.GetAsync("/health");
        }

        public async UniTask<AlgoApiResponse<TransactionResponse>> GetTransaction(TransactionId txid)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<TransactionsResponse>> GetTransactions(TransactionsQuery query = default)
        {
            throw new System.NotImplementedException();
        }
    }
}

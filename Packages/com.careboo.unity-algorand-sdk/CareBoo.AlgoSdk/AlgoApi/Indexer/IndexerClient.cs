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
            return await this.GetAsync($"/v2/accounts/{accountId}", query);
        }

        public async UniTask<AlgoApiResponse<AccountsResponse>> GetAccounts(AccountsQuery query = default)
        {
            return await this.GetAsync("/v2/accounts", query);
        }

        public async UniTask<AlgoApiResponse<TransactionsResponse>> GetAccountTransactions(ulong accountId, TransactionsQuery query = default)
        {
            return await this.GetAsync($"/v2/accounts/{accountId}/transactions", query);
        }

        public async UniTask<AlgoApiResponse<ApplicationResponse>> GetApplication(ulong applicationId, ApplicationsQuery query = default)
        {
            return await this.GetAsync($"/v2/applications/{applicationId}", query);
        }

        public async UniTask<AlgoApiResponse<ApplicationsResponse>> GetApplications(ApplicationsQuery query = default)
        {
            return await this.GetAsync("/v2/applications", query);
        }

        public async UniTask<AlgoApiResponse<AssetResponse>> GetAsset(ulong assetId, AssetQuery query = default)
        {
            return await this.GetAsync($"/v2/assets/{assetId}", query);
        }

        public async UniTask<AlgoApiResponse<BalancesResponse>> GetAssetBalances(ulong assetId, BalancesQuery query = default)
        {
            return await this.GetAsync($"/v2/assets/{assetId}/balances", query);
        }

        public async UniTask<AlgoApiResponse<AssetsResponse>> GetAssets(AssetsQuery query = default)
        {
            return await this.GetAsync("/v2/assets", query);
        }

        public async UniTask<AlgoApiResponse<TransactionsResponse>> GetAssetTransactions(ulong assetId, TransactionsQuery query = default)
        {
            return await this.GetAsync($"/v2/assets/{assetId}/transactions", query);
        }

        public async UniTask<AlgoApiResponse<Block>> GetBlock(ulong round)
        {
            return await this.GetAsync($"/v2/blocks/{round}");
        }

        public async UniTask<AlgoApiResponse<HealthCheck>> GetHealth()
        {
            return await this.GetAsync("/health");
        }

        public async UniTask<AlgoApiResponse<TransactionResponse>> GetTransaction(TransactionId txid)
        {
            return await this.GetAsync($"/v2/transactions/{txid}");
        }

        public async UniTask<AlgoApiResponse<TransactionsResponse>> GetTransactions(TransactionsQuery query = default)
        {
            return await this.GetAsync("/v2/transactions", query);
        }
    }
}

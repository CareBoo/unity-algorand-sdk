using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IIndexerClient : IAlgoApiClient
    {
        UniTask<AlgoApiResponse<HealthCheck>> GetHealth();
        UniTask<AlgoApiResponse<AccountsResponse>> GetAccounts(AccountsQuery query = default);
        UniTask<AlgoApiResponse<AccountResponse>> GetAccount(Address accountAddress, AccountQuery query = default);
        UniTask<AlgoApiResponse<TransactionsResponse>> GetAccountTransactions(Address accountAddress, TransactionsQuery query = default);
        UniTask<AlgoApiResponse<ApplicationsResponse>> GetApplications(ApplicationsQuery query = default);
        UniTask<AlgoApiResponse<ApplicationResponse>> GetApplication(ulong applicationId, ApplicationsQuery query = default);
        UniTask<AlgoApiResponse<AssetsResponse>> GetAssets(AssetsQuery query = default);
        UniTask<AlgoApiResponse<AssetResponse>> GetAsset(ulong assetId, AssetQuery query = default);
        UniTask<AlgoApiResponse<BalancesResponse>> GetAssetBalances(ulong assetId, BalancesQuery query = default);
        UniTask<AlgoApiResponse<TransactionsResponse>> GetAssetTransactions(ulong assetId, TransactionsQuery query = default);
        UniTask<AlgoApiResponse<Block>> GetBlock(ulong round);
        UniTask<AlgoApiResponse<TransactionsResponse>> GetTransactions(TransactionsQuery query = default);
        UniTask<AlgoApiResponse<TransactionResponse>> GetTransaction(FixedString64Bytes txid);
    }
}

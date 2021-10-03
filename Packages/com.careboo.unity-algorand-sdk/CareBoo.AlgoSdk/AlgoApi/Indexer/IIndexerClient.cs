using Cysharp.Threading.Tasks;

namespace AlgoSdk
{
    public interface IIndexerClient
    {
        UniTask<AlgoApiResponse<HealthCheck>> GetHealth();
        UniTask<AlgoApiResponse<AccountsResponse>> GetAccounts(AccountsQuery query = default);
    }
}

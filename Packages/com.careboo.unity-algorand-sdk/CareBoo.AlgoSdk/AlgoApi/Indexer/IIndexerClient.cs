using Cysharp.Threading.Tasks;

namespace AlgoSdk
{
    public interface IIndexerClient
    {
        UniTask<AlgoApiResponse<HealthCheck>> GetHealth();
    }
}

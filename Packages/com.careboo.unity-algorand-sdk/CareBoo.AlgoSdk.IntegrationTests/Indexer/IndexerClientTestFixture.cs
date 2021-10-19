using Cysharp.Threading.Tasks;

public abstract class IndexerClientTestFixture : AlgoApiClientTestFixture
{
    protected override AlgoServices RequiresServices => AlgoServices.Indexer | AlgoServices.Algod;

    protected override async UniTask SetUpAsync()
    {
        await CheckServices();
    }

    protected override UniTask TearDownAsync() => UniTask.CompletedTask;
}

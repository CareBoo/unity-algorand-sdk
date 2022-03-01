using Cysharp.Threading.Tasks;

public abstract class IndexerClientTestFixture : AlgoApiClientTestFixture
{
    protected override AlgoServices RequiresServices => AlgoServices.Indexer | AlgoServices.Algod;

    protected override UniTask SetUpAsync() => UniTask.CompletedTask;

    protected override UniTask TearDownAsync() => UniTask.CompletedTask;
}

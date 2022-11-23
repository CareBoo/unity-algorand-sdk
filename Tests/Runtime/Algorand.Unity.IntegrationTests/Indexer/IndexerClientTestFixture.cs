public abstract class IndexerClientTestFixture : KmdClientTestFixture
{
    protected override AlgoServices RequiresServices => AlgoServices.Indexer | AlgoServices.Algod | base.RequiresServices;
}

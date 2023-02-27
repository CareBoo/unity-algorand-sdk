using Algorand.Indexer;

namespace Algorand.Unity.Net
{
    public static class IndexerClientExtensions
    {
        public static CommonApi ToCommonApi(this IndexerClient indexer)
        {
            return new CommonApi(indexer.ToHttpClient());
        }

        public static SearchApi ToSearchApi(this IndexerClient indexer)
        {
            return new SearchApi(indexer.ToHttpClient());
        }

        public static LookupApi ToLookupApi(this IndexerClient indexer)
        {
            return new LookupApi(indexer.ToHttpClient());
        }
    }
}

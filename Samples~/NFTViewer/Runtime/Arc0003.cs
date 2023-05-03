using Algorand.Unity.Indexer;

namespace Algorand.Unity.NftViewer
{
    public static class Arc0003
    {
        public static bool IsArc0003(this Asset asset)
        {
            return IsArc0003(asset.Params.Name, asset.Params.Url);
        }

        private static bool IsArc0003(string assetName, string assetUrl)
        {
            return assetName == "arc3"
                   || assetName.EndsWith("@arc3")
                   || assetUrl.EndsWith("#arc3");
        }
    }
}
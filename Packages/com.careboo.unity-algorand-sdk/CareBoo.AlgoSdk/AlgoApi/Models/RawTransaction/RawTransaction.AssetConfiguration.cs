namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey("caid")]
        public ulong ConfigAsset
        {
            get => AssetConfigurationParams.ConfigAsset;
            set => AssetConfigurationParams.ConfigAsset = value;
        }

        [AlgoApiKey("apar")]
        public AssetParams AssetParams
        {
            get => AssetConfigurationParams.AssetParams;
            set => AssetConfigurationParams.AssetParams = value;
        }
    }
}

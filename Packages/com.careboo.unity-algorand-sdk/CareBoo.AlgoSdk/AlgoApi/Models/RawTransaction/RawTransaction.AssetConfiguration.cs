namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey(null, "caid")]
        public ulong ConfigAsset
        {
            get => AssetConfigurationParams.ConfigAsset;
            set => AssetConfigurationParams.ConfigAsset = value;
        }

        [AlgoApiKey(null, "apar")]
        public AssetParams AssetParams
        {
            get => AssetConfigurationParams.AssetParams;
            set => AssetConfigurationParams.AssetParams = value;
        }
    }
}

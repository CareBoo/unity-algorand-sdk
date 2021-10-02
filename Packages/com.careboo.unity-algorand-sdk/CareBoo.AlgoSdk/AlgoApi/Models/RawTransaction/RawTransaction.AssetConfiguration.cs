namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiField(null, "caid")]
        public ulong ConfigAsset
        {
            get => AssetConfigurationParams.ConfigAsset;
            set => AssetConfigurationParams.ConfigAsset = value;
        }

        [AlgoApiField(null, "apar")]
        public AssetParams AssetParams
        {
            get => AssetConfigurationParams.AssetParams;
            set => AssetConfigurationParams.AssetParams = value;
        }
    }
}

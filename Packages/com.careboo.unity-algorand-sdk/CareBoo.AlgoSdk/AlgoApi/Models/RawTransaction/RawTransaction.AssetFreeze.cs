namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey(null, "fadd")]
        public Address FreezeAccount
        {
            get => AssetFreezeParams.FreezeAccount;
            set => AssetFreezeParams.FreezeAccount = value;
        }

        [AlgoApiKey(null, "faid")]
        public ulong FreezeAsset
        {
            get => AssetFreezeParams.FreezeAsset;
            set => AssetFreezeParams.FreezeAsset = value;
        }

        [AlgoApiKey(null, "afrz")]
        public Optional<bool> AssetFrozen
        {
            get => AssetFreezeParams.AssetFrozen;
            set => AssetFreezeParams.AssetFrozen = value;
        }
    }
}

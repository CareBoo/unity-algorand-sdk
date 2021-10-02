namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiField(null, "fadd")]
        public Address FreezeAccount
        {
            get => AssetFreezeParams.FreezeAccount;
            set => AssetFreezeParams.FreezeAccount = value;
        }

        [AlgoApiField(null, "faid")]
        public ulong FreezeAsset
        {
            get => AssetFreezeParams.FreezeAsset;
            set => AssetFreezeParams.FreezeAsset = value;
        }

        [AlgoApiField(null, "afrz")]
        public Optional<bool> AssetFrozen
        {
            get => AssetFreezeParams.AssetFrozen;
            set => AssetFreezeParams.AssetFrozen = value;
        }
    }
}

namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey(null, "xaid")]
        public ulong XferAsset
        {
            get => AssetTransferParams.XferAsset;
            set => AssetTransferParams.XferAsset = value;
        }

        [AlgoApiKey(null, "aamt")]
        public ulong AssetAmount
        {
            get => AssetTransferParams.AssetAmount;
            set => AssetTransferParams.AssetAmount = value;
        }

        [AlgoApiKey(null, "asnd")]
        public Address AssetSender
        {
            get => AssetTransferParams.AssetSender;
            set => AssetTransferParams.AssetSender = value;
        }

        [AlgoApiKey(null, "arcv")]
        public Address AssetReceiver
        {
            get => AssetTransferParams.AssetReceiver;
            set => AssetTransferParams.AssetReceiver = value;
        }

        [AlgoApiKey(null, "aclose")]
        public Address AssetCloseTo
        {
            get => AssetTransferParams.AssetCloseTo;
            set => AssetTransferParams.AssetCloseTo = value;
        }
    }
}

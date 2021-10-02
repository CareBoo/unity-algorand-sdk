namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiField(null, "xaid")]
        public ulong XferAsset
        {
            get => AssetTransferParams.XferAsset;
            set => AssetTransferParams.XferAsset = value;
        }

        [AlgoApiField(null, "aamt")]
        public ulong AssetAmount
        {
            get => AssetTransferParams.AssetAmount;
            set => AssetTransferParams.AssetAmount = value;
        }

        [AlgoApiField(null, "asnd")]
        public Address AssetSender
        {
            get => AssetTransferParams.AssetSender;
            set => AssetTransferParams.AssetSender = value;
        }

        [AlgoApiField(null, "arcv")]
        public Address AssetReceiver
        {
            get => AssetTransferParams.AssetReceiver;
            set => AssetTransferParams.AssetReceiver = value;
        }

        [AlgoApiField(null, "aclose")]
        public Address AssetCloseTo
        {
            get => AssetTransferParams.AssetCloseTo;
            set => AssetTransferParams.AssetCloseTo = value;
        }
    }
}

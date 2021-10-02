using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct RawTransaction
        : IEquatable<RawTransaction>
    {
        public Transaction.Header Header;

        [AlgoApiKey("payment-transaction", null)]
        public Transaction.Payment.Params PaymentParams;

        [AlgoApiKey("asset-config-transaction", null)]
        public Transaction.AssetConfiguration.Params AssetConfigurationParams;

        [AlgoApiKey("asset-transfer-transaction", null)]
        public Transaction.AssetTransfer.Params AssetTransferParams;

        [AlgoApiKey("asset-freeze-transaction", null)]
        public Transaction.AssetFreeze.Params AssetFreezeParams;

        [AlgoApiKey("application-transaction", null)]
        public Transaction.ApplicationCall.Params ApplicationCallParams;

        [AlgoApiKey("keyreg-transaction", null)]
        public Transaction.KeyRegistration.Params KeyRegistrationParams;

        public static bool operator ==(in RawTransaction x, in RawTransaction y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(in RawTransaction x, in RawTransaction y)
        {
            return !x.Equals(y);
        }

        public bool Equals(RawTransaction other)
        {
            return Header.Equals(other.Header)
                && TransactionType switch
                {
                    TransactionType.Payment => PaymentParams.Equals(other.PaymentParams),
                    TransactionType.AssetConfiguration => AssetConfigurationParams.Equals(other.AssetConfigurationParams),
                    _ => true
                };
        }

        public override bool Equals(object obj)
        {
            if (obj is RawTransaction other)
                return Equals(other);
            return false;
        }

        public override int GetHashCode() => Header.GetHashCode();
    }
}

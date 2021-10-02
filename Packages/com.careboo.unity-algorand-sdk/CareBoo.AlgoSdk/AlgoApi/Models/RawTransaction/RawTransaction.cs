using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct RawTransaction
        : IEquatable<RawTransaction>
    {
        public Transaction.Header Header;

        [AlgoApiField("payment-transaction", null, readOnly: true)]
        public Transaction.Payment.Params PaymentParams;

        [AlgoApiField("asset-config-transaction", null, readOnly: true)]
        public Transaction.AssetConfiguration.Params AssetConfigurationParams;

        [AlgoApiField("asset-transfer-transaction", null, readOnly: true)]
        public Transaction.AssetTransfer.Params AssetTransferParams;

        [AlgoApiField("asset-freeze-transaction", null, readOnly: true)]
        public Transaction.AssetFreeze.Params AssetFreezeParams;

        [AlgoApiField("application-transaction", null, readOnly: true)]
        public Transaction.ApplicationCall.Params ApplicationCallParams;

        [AlgoApiField("keyreg-transaction", null, readOnly: true)]
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
                    TransactionType.ApplicationCall => ApplicationCallParams.Equals(other.ApplicationCallParams),
                    TransactionType.AssetFreeze => AssetFreezeParams.Equals(other.AssetFreezeParams),
                    TransactionType.AssetTransfer => AssetTransferParams.Equals(other.AssetTransferParams),
                    TransactionType.KeyRegistration => KeyRegistrationParams.Equals(other.KeyRegistrationParams),
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

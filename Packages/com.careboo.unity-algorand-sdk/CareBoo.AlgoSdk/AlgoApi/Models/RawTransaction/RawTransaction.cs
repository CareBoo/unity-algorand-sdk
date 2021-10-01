using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct RawTransaction
        : IEquatable<RawTransaction>
    {
        public Transaction.Header Header;
        public Transaction.Payment.Params PaymentParams;
        public Transaction.AssetConfiguration.Params AssetConfigurationParams;
        public Transaction.AssetTransfer.Params AssetTransferParams;
        public Transaction.AssetFreeze.Params AssetFreezeParams;
        public Transaction.ApplicationCall.Params ApplicationCallParams;
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

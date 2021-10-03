using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct Transaction
        : IEquatable<Transaction>
    {
        public Header HeaderParams;

        [AlgoApiField("payment-transaction", null, readOnly: true)]
        public Payment.Params PaymentParams;

        [AlgoApiField("asset-config-transaction", null, readOnly: true)]
        public AssetConfiguration.Params AssetConfigurationParams;

        [AlgoApiField("asset-transfer-transaction", null, readOnly: true)]
        public AssetTransfer.Params AssetTransferParams;

        [AlgoApiField("asset-freeze-transaction", null, readOnly: true)]
        public AssetFreeze.Params AssetFreezeParams;

        [AlgoApiField("application-transaction", null, readOnly: true)]
        public ApplicationCall.Params ApplicationCallParams;

        [AlgoApiField("keyreg-transaction", null, readOnly: true)]
        public KeyRegistration.Params KeyRegistrationParams;

        [AlgoApiField("signature", null, readOnly: true)]
        public TransactionSignature Signature;

        public bool Equals(Transaction other)
        {
            return HeaderParams.Equals(other.HeaderParams)
                && HeaderParams.TransactionType switch
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
    }
}

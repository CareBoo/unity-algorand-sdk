using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct Transaction
        : IEquatable<Transaction>
    {
        static readonly byte[] SignaturePrefix = Encoding.UTF8.GetBytes("TX");

        public TransactionHeader HeaderParams;

        [AlgoApiField("payment-transaction", null, readOnly: true)]
        public PaymentTxn.Params PaymentParams;

        [AlgoApiField("asset-config-transaction", null, readOnly: true)]
        public AssetConfigTxn.Params AssetConfigurationParams;

        [AlgoApiField("asset-transfer-transaction", null, readOnly: true)]
        public AssetTransferTxn.Params AssetTransferParams;

        [AlgoApiField("asset-freeze-transaction", null, readOnly: true)]
        public AssetFreezeTxn.Params AssetFreezeParams;

        [AlgoApiField("application-transaction", null, readOnly: true)]
        public AppCallTxn.Params ApplicationCallParams;

        [AlgoApiField("keyreg-transaction", null, readOnly: true)]
        public KeyRegTxn.Params KeyRegistrationParams;

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
                }
                && Signature.Equals(other.Signature)
                ;
        }

        public Sig Sign(Ed25519.SecretKeyHandle secretKey)
        {
            using var message = ToSignatureMessage(Allocator.Temp);
            return secretKey.Sign(message);
        }

        public NativeByteArray ToSignatureMessage(Allocator allocator)
        {
            using var data = AlgoApiSerializer.SerializeMessagePack(this, Allocator.Temp);
            var result = new NativeByteArray(SignaturePrefix.Length + data.Length, allocator);
            for (var i = 0; i < SignaturePrefix.Length; i++)
                result[i] = SignaturePrefix[i];
            for (var i = 0; i < data.Length; i++)
                result[i + SignaturePrefix.Length] = data[i];
            return result;
        }
    }
}

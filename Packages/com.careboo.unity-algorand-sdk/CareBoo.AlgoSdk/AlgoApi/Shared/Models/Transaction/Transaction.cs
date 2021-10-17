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
        public static readonly byte[] SignaturePrefix = Encoding.UTF8.GetBytes("TX");

        public const int MaxGroupSize = 16;

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

        public int EstimateBlockSizeBytes()
        {
            var rndSeed = AlgoSdk.Crypto.Random.Bytes<Ed25519.Seed>();
            using var keyPair = rndSeed.ToKeyPair();
            var signedTxn = Sign(keyPair.SecretKey);
            return AlgoApiSerializer.SerializeMessagePack(signedTxn).Length;
        }

        public SignedTransaction Sign(Ed25519.SecretKeyHandle secretKey)
        {
            Signature = GetSignature(secretKey);
            return new SignedTransaction { Transaction = this };
        }

        public Sig GetSignature(Ed25519.SecretKeyHandle secretKey)
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

        public Sha512_256_Hash GetId()
        {
            using var txnData = ToSignatureMessage(Allocator.Temp);
            return Sha512.Hash256Truncated(txnData);
        }

        public static Sha512_256_Hash GetGroupId(params Transaction[] txns)
        {
            if (txns == null || txns.Length == 0)
                throw new ArgumentException("Cannot get the group id of 0 transactions", nameof(txns));
            if (txns.Length > TransactionGroup.MaxSize)
                throw new ArgumentException($"Cannot get the group id of a group of more than {TransactionGroup.MaxSize} transactions", nameof(txns));
            for (var i = 0; i < txns.Length; i++)
                txns[i].Group = default;
            var txnMsgs = new Sha512_256_Hash[txns.Length];
            for (var i = 0; i < txns.Length; i++)
            {
                txns[i].Group = default;
                txnMsgs[i] = txns[i].GetId();
            }
            return GetGroupId(txnMsgs);
        }

        public static Sha512_256_Hash GetGroupId(params Sha512_256_Hash[] txns)
        {
            if (txns == null || txns.Length == 0)
                throw new ArgumentException("Cannot get the group id of 0 transactions", nameof(txns));
            if (txns.Length > TransactionGroup.MaxSize)
                throw new ArgumentException($"Cannot get the group id of a group of more than {TransactionGroup.MaxSize} transactions", nameof(txns));
            var group = new TransactionGroup { Txns = txns };
            using var msgpack = AlgoApiSerializer.SerializeMessagePack(group, Allocator.Temp);
            var data = new NativeByteArray(TransactionGroup.IdPrefix.Length + msgpack.Length, Allocator.Temp);
            try
            {
                data.CopyFrom(TransactionGroup.IdPrefix, 0);
                data.CopyFrom(msgpack.AsArray(), TransactionGroup.IdPrefix.Length);
                return Sha512.Hash256Truncated(data);
            }
            finally
            {
                data.Dispose();
            }
        }
    }
}

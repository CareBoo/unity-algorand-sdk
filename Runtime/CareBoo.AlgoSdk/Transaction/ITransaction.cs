using System;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Mathematics;

namespace AlgoSdk
{
    public interface ITransaction
    {
        /// <summary>
        /// Paid by the sender to the FeeSink to prevent denial-of-service. The minimum fee on Algorand is currently 1000 microAlgos.
        /// </summary>
        ulong Fee { get; }

        /// <summary>
        /// The first round for when the transaction is valid. If the transaction is sent prior to this round it will be rejected by the network.
        /// </summary>
        ulong FirstValidRound { get; }

        /// <summary>
        /// The hash of the genesis block of the network for which the transaction is valid.
        /// </summary>
        GenesisHash GenesisHash { get; }

        /// <summary>
        /// The ending round for which the transaction is valid. After this round, the transaction will be rejected by the network.
        /// </summary>
        ulong LastValidRound { get; }

        /// <summary>
        /// The address of the account that pays the fee and amount.
        /// </summary>
        Address Sender { get; }

        /// <summary>
        /// Specifies the type of transaction. This value is automatically generated using any of the developer tools.
        /// </summary>
        TransactionType TransactionType { get; }

        /// <summary>
        /// The human-readable string that identifies the network for the transaction. The genesis ID is found in the genesis block.
        /// </summary>
        FixedString32Bytes GenesisId { get; }

        /// <summary>
        /// The group specifies that the transaction is part of a group and, if so, specifies the hash of the transaction group. See <see cref="Transaction.GetGroupId"/>.
        /// </summary>
        Sha512_256_Hash Group { get; }

        /// <summary>
        /// A lease enforces mutual exclusion of transactions. If this field is nonzero, then once the transaction is confirmed, it acquires the lease identified by the (Sender, Lease) pair of the transaction until the LastValid round passes. While this transaction possesses the lease, no other transaction specifying this lease can be confirmed. A lease is often used in the context of Algorand Smart Contracts to prevent replay attacks.
        /// </summary>
        Sha512_256_Hash Lease { get; }

        /// <summary>
        /// Any data up to 1000 bytes.
        /// </summary>
        byte[] Note { get; }

        /// <summary>
        /// Specifies the authorized address. This address will be used to authorize all future transactions.
        /// </summary>
        Address RekeyTo { get; }

        /// <summary>
        /// Copy this transactions fields to a <see cref="Transaction"/> which contains all possible transaction fields.
        /// </summary>
        /// <param name="transaction">A raw transaction with all possible transaction fields.</param>
        void CopyTo(ref Transaction transaction);

        /// <summary>
        /// Copy relevant fields to this transaction.
        /// </summary>
        /// <param name="transaction">A raw transaction with all possible transaction fields.</param>
        void CopyFrom(Transaction transaction);
    }

    public static class TransactionExtensions
    {
        public static SignedTxn<T> Sign<T>(
            this T txn,
            Ed25519.SecretKeyHandle secretKey
            )
            where T : ITransaction, IEquatable<T>
        {
            var signature = txn.GetSignature(secretKey);
            return new SignedTxn<T>
            {
                Txn = txn,
                Sig = signature
            };
        }

        public static Sig GetSignature<T>(
            this T txn,
            Ed25519.SecretKeyHandle secretKey
            )
            where T : ITransaction
        {
            using var msg = txn.ToSignatureMessage(Allocator.Temp);
            return secretKey.Sign(msg);
        }

        public static byte[] ToSignatureMessage<T>(this T txn)
            where T : ITransaction
        {
            using var messageForSigning = txn.ToSignatureMessage(Allocator.Temp);
            return messageForSigning.ToArray();
        }

        public static NativeByteArray ToSignatureMessage<T>(
            this T txn,
            Allocator allocator
            )
            where T : ITransaction
        {
            using var data = AlgoApiSerializer.SerializeMessagePack(txn, Allocator.Temp);
            var result = new NativeByteArray(Transaction.SignaturePrefix.Length + data.Length, allocator);
            for (var i = 0; i < Transaction.SignaturePrefix.Length; i++)
                result[i] = Transaction.SignaturePrefix[i];
            for (var i = 0; i < data.Length; i++)
                result[i + Transaction.SignaturePrefix.Length] = data[i];
            return result;
        }

        public static ulong GetSuggestedFee<T>(
            this T txn,
            TransactionParams txnParams
            )
            where T : ITransaction, IEquatable<T>
        {
            var fee = txnParams.FlatFee ? txnParams.Fee : txnParams.Fee * (ulong)txn.EstimateBlockSizeBytes();
            return math.max(fee, txnParams.MinFee);
        }

        public static int EstimateBlockSizeBytes<T>(this T txn)
            where T : ITransaction, IEquatable<T>
        {
            var keyPair = AlgoSdk.Crypto.Random.Bytes<Ed25519.Seed>().ToKeyPair();
            var signedTxn = txn.Sign(keyPair.SecretKey);
            using var signedBytes = AlgoApiSerializer.SerializeMessagePack(signedTxn, Allocator.Temp);
            return signedBytes.Length;
        }

        public static TransactionId GetId<T>(this T txn)
            where T : ITransaction
        {
            using var txnData = txn.ToSignatureMessage(Allocator.Temp);
            return Sha512.Hash256Truncated(txnData);
        }
    }
}

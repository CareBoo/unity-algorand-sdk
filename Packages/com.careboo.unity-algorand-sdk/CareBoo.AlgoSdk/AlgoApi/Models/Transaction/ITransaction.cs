using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    public interface ITransaction
    {
        void CopyTo(ref Transaction transaction);
        void CopyFrom(Transaction transaction);
    }

    public static class TransactionExtensions
    {
        public static SignedTransaction Sign<TTransaction>(
            this ref TTransaction source,
            Ed25519.SecretKeyHandle secretKey
            )
            where TTransaction : struct, ITransaction, IEquatable<TTransaction>
        {
            Transaction txn = default;
            source.CopyTo(ref txn);
            return txn.Sign(secretKey);
        }
    }
}

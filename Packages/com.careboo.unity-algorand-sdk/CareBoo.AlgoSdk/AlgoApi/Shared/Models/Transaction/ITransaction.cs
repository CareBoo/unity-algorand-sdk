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
            this TTransaction source,
            Ed25519.SecretKeyHandle secretKey
            )
            where TTransaction : struct, ITransaction, IEquatable<TTransaction>
        {
            Transaction txn = source.ToRaw();
            txn.Signature = txn.Sign(secretKey);
            return new SignedTransaction { Transaction = txn };
        }

        public static Sig GetSignature<TTransaction>(
            this TTransaction source,
            Ed25519.SecretKeyHandle secretKey
            )
            where TTransaction : struct, ITransaction, IEquatable<TTransaction>
        {
            return source.ToRaw().Sign(secretKey);
        }

        public static Transaction ToRaw<TTransaction>(
            this TTransaction source
            )
            where TTransaction : struct, ITransaction, IEquatable<TTransaction>
        {
            Transaction raw = default;
            source.CopyTo(ref raw);
            return raw;
        }
    }
}

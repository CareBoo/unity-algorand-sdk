using System;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(SignedTransactionFormatter))]
    public struct SignedTransaction
        : IEquatable<SignedTransaction>
    {
        public Transaction Transaction;

        public TransactionSignature Signature
        {
            get => Transaction.Signature;
            set => Transaction.Signature = value;
        }

        public bool Equals(SignedTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }
    }

    [AlgoApiFormatter(typeof(SignedTransactionFormatter<AppCallTxn>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<AssetConfigTxn>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<AssetFreezeTxn>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<AssetTransferTxn>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<KeyRegTxn>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<PaymentTxn>))]
    public struct Signed<TTransaction>
        : IEquatable<Signed<TTransaction>>
        where TTransaction : struct, ITransaction, IEquatable<TTransaction>
    {
        public TTransaction Transaction;

        public TransactionSignature Signature;

        public bool Equals(Signed<TTransaction> other)
        {
            return Transaction.Equals(other.Transaction)
                && Signature.Equals(other.Signature)
                ;
        }
    }
}

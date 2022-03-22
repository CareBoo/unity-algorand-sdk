using System;
using Cysharp.Threading.Tasks;

namespace AlgoSdk
{
    public interface ISigner
    {
        /// <summary>
        /// Sign a single transaction.
        /// </summary>
        /// <param name="txn">The transaction to sign.</param>
        /// <typeparam name="T">The type of the transaction.</typeparam>
        /// <returns>Transaction with signature if it was signed.</returns>
        SignedTxn<T> SignTxn<T>(T txn) where T : ITransaction, IEquatable<T>;

        /// <summary>
        /// Sign a group of transactions.
        /// </summary>
        /// <param name="txns">The transactions to sign.</param>
        /// <typeparam name="T">The type of the transactions.</typeparam>
        /// <returns>An array of transactions with signatures. If the transaction at a given index was not signed, that signed transaction will have no signature.</returns>
        SignedTxn<T>[] SignTxns<T>(T[] txns) where T : ITransaction, IEquatable<T>;
    }

    public interface IAsyncSigner
    {
        /// <summary>
        /// Sign a single transaction.
        /// </summary>
        /// <param name="txn">The transaction to sign.</param>
        /// <typeparam name="T">The type of the transaction.</typeparam>
        /// <returns>Transaction with signature if it was signed.</returns>
        UniTask<SignedTxn<T>> SignTxnAsync<T>(T txn) where T : ITransaction, IEquatable<T>;

        /// <summary>
        /// Sign a group of transactions.
        /// </summary>
        /// <param name="txns">The transactions to sign.</param>
        /// <typeparam name="T">The type of the transactions.</typeparam>
        /// <returns>An array of transactions with signatures. If the transaction at a given index was not signed, that signed transaction will have no signature.</returns>
        UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(T[] txns) where T : ITransaction, IEquatable<T>;
    }
}

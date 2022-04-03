using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace AlgoSdk
{
    public interface ISigner
    {
        /// <summary>
        /// Sign a group of transactions.
        /// </summary>
        /// <remarks>
        /// Each transaction is expected to have a valid group id already set.
        /// </remarks>
        /// <param name="txns">The transactions to sign.</param>
        /// <param name="txnsToSign">Indexes of the transactions this signer should sign.</param>
        /// <typeparam name="T">The type of the transactions.</typeparam>
        /// <returns>An array of transactions with signatures. If the transaction at a given index was not signed, that signed transaction will have no signature.</returns>
        SignedTxn<T>[] SignTxns<T>(T[] txns, int[] txnsToSign) where T : ITransaction, IEquatable<T>;
    }

    public interface IAsyncSigner
    {
        /// <summary>
        /// Sign a group of transactions.
        /// </summary>
        /// <remarks>
        /// Each transaction is expected to have a valid group id already set.
        /// </remarks>
        /// <param name="txns">The transactions to sign.</param>
        /// <param name="txnsToSign">Indexes of the transactions this signer should sign.</param>
        /// <param name="cancellationToken">Provide an optional cancellation token to interrupt signing.</param>
        /// <typeparam name="T">The type of the transactions.</typeparam>
        /// <returns>An array of transactions with signatures. If the transaction at a given index was not signed, that signed transaction will have no signature.</returns>
        UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(
            T[] txns,
            int[] txnsToSign,
            CancellationToken cancellationToken = default
            )
            where T : ITransaction, IEquatable<T>;
    }

    public interface IAsyncSignerWithProgress : IAsyncSigner
    {
        /// <summary>
        /// Sign a group of transactions.
        /// </summary>
        /// <remarks>
        /// Each transaction is expected to have a valid group id already set.
        /// </remarks>
        /// <param name="txns">The transactions to sign.</param>
        /// <param name="txnsToSign">Indexes of the transactions this signer should sign.</param>
        /// <param name="progress">A progress token that can be used to periodically check the progress.</param>
        /// <param name="cancellationToken">Provide an optional cancellation token to interrupt signing.</param>
        /// <typeparam name="T">The type of the transactions.</typeparam>
        /// <returns>An array of transactions with signatures. If the transaction at a given index was not signed, that signed transaction will have no signature.</returns>
        UniTask<SignedTxn<T>[]> SignTxnsAsync<T, TProgress>(
            T[] txns,
            int[] txnsToSign,
            TProgress progress,
            CancellationToken cancellationToken = default
            )
            where T : ITransaction, IEquatable<T>
            where TProgress : IProgress<T>;
    }
}

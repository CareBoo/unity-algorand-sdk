using System;
using System.Threading;

namespace Algorand.Unity
{
    public static partial class AtomicTxn
    {
        public interface ISigning<T>
            where T : ISigning<T>
        {
            /// <summary>
            /// Transactions in this group.
            /// </summary>
            Transaction[] Txns { get; }

            /// <summary>
            /// Signatures associated with each transaction.
            /// </summary>
            TransactionSignature[] Sigs { get; }

            /// <summary>
            /// Indices of the transactions that have already been signed.
            /// </summary>
            TxnIndices SignedTxnIndices { get; set; }

            /// <summary>
            /// Represent this signing atomic txn group as an async signing group.
            /// </summary>
            /// <remarks>
            /// Async signing groups will require waiting for signatures signing is finished.
            /// </remarks>
            AsyncSigning AsAsync();
        }

        /// <summary>
        /// Sign transactions in this group where the <see cref="Transaction.Sender"/> equals the <see cref="IAccount.Address"/>.
        /// </summary>
        /// <param name="group">The signing atomic txn group.</param>
        /// <param name="signer">The account to sign with.</param>
        /// <typeparam name="TSigning">The type of the signing atomic txn group.</typeparam>
        /// <typeparam name="TSigner">The type of the account signer.</typeparam>
        /// <returns>This signing atomic txn group with new signatures added.</returns>
        public static TSigning SignWith<TSigning, TSigner>(
            this TSigning group,
            TSigner signer
            )
            where TSigning : ISigning<TSigning>
            where TSigner : IAccount, ISigner
        {
            var indices = group.GetTxnIndicesWithSender(signer.Address);
            return group.SignWith(signer, indices);
        }

        /// <summary>
        /// Sign transactions in this group indicated by the given indices with the given signer.
        /// </summary>
        /// <param name="group">The signing atomic txn group.</param>
        /// <param name="signer">The signer to sign these transactions.</param>
        /// <param name="txnsToSign">The indices of the transactions to sign.</param>
        /// <typeparam name="TSigning">The type of the signing atomic txn group.</typeparam>
        /// <typeparam name="TSigner">The type of the account signer.</typeparam>
        /// <returns>This signing atomic txn group with new signatures added.</returns>
        public static TSigning SignWith<TSigning, TSigner>(
            this TSigning group,
            TSigner signer,
            TxnIndices txnsToSign
            )
            where TSigning : ISigning<TSigning>
            where TSigner : ISigner
        {
            group.CheckTxnIndicesToSign(txnsToSign);
            group.SignedTxnIndices |= txnsToSign;
            var signedTxns = signer.SignTxns(group.Txns, txnsToSign);
            group.SetSignatures(signedTxns, txnsToSign);
            return group;
        }

        /// <summary>
        /// Sign transactions in this group where the <see cref="Transaction.Sender"/> equals the <see cref="IAccount.Address"/>.
        /// </summary>
        /// <param name="group">The signing atomic txn group.</param>
        /// <param name="signer">The async signer account to sign the transactions.</param>
        /// <param name="cancellationToken">An optional cancellation token for this signer.</param>
        /// <typeparam name="TSigning">The type of the signing atomic txn group.</typeparam>
        /// <typeparam name="TSigner">The type of the account signer.</typeparam>
        /// <returns>An Async Signing Atomic Txn Group with new signatures awaiting to be added.</returns>
        public static AsyncSigning SignWithAsync<TSigning, TSigner>(
            this TSigning group,
            TSigner signer,
            CancellationToken cancellationToken = default
            )
            where TSigning : ISigning<TSigning>
            where TSigner : IAccount, IAsyncSigner
        {
            var txnsToSign = group.GetTxnIndicesWithSender(signer.Address);
            return group
                .AsAsync()
                .SignWithAsync(signer, txnsToSign, cancellationToken);
        }

        /// <summary>
        /// Sign transactions in this group indicated by the given indices with the given signer.
        /// </summary>
        /// <param name="group">The signing atomic txn group.</param>
        /// <param name="signer">The async signer account to sign the transactions.</param>
        /// <param name="txnsToSign">The indices of the transactions to sign.</param>
        /// <param name="cancellationToken">An optional cancellation token for this signer.</param>
        /// <typeparam name="TSigning">The type of the signing atomic txn group.</typeparam>
        /// <typeparam name="TSigner">The type of the account signer.</typeparam>
        /// <returns>An Async Signing Atomic Txn Group with new signatures awaiting to be added.</returns>
        public static AsyncSigning SignWithAsync<TSigning, TSigner>(
            this TSigning group,
            TSigner signer,
            TxnIndices txnsToSign,
            CancellationToken cancellationToken = default
            )
            where TSigning : ISigning<TSigning>
            where TSigner : IAsyncSigner
        {
            return group
                .AsAsync()
                .SignWithAsync(signer, txnsToSign, cancellationToken);
        }

        /// <summary>
        /// Sign transactions in this group where the <see cref="Transaction.Sender"/> equals the <see cref="IAccount.Address"/>.
        /// </summary>
        /// <param name="group">The signing atomic txn group.</param>
        /// <param name="signer">The async signer account to sign the transactions.</param>
        /// <param name="progress">A progress token used to track progress</param>
        /// <param name="cancellationToken">An optional cancellation token for this signer.</param>
        /// <typeparam name="TSigning">The type of the signing atomic txn group.</typeparam>
        /// <typeparam name="TSigner">The type of the account signer.</typeparam>
        /// <returns>An Async Signing Atomic Txn Group with new signatures awaiting to be added.</returns>
        public static AsyncSigning SignWithAsync<TSigning, TSigner, TProgress>(
            this TSigning group,
            TSigner signer,
            TProgress progress,
            CancellationToken cancellationToken = default
            )
            where TSigning : ISigning<TSigning>
            where TSigner : IAccount, IAsyncSignerWithProgress
            where TProgress : IProgress<float>
        {
            var txnsToSign = group.GetTxnIndicesWithSender(signer.Address);
            return group
                .AsAsync()
                .SignWithAsync(signer, txnsToSign, progress, cancellationToken);
        }

        /// <summary>
        /// Sign transactions in this group indicated by the given indices with the given signer.
        /// </summary>
        /// <param name="group">The signing atomic txn group.</param>
        /// <param name="signer">The async signer account to sign the transactions.</param>
        /// <param name="txnsToSign">The indices of the transactions to sign.</param>
        /// <param name="progress">A progress token used to track progress</param>
        /// <param name="cancellationToken">An optional cancellation token for this signer.</param>
        /// <typeparam name="TSigning">The type of the signing atomic txn group.</typeparam>
        /// <typeparam name="TSigner">The type of the account signer.</typeparam>
        /// <returns>An Async Signing Atomic Txn Group with new signatures awaiting to be added.</returns>
        public static AsyncSigning SignWithAsync<TSigning, TSigner, TProgress>(
            this TSigning group,
            TSigner signer,
            TxnIndices txnsToSign,
            TProgress progress,
            CancellationToken cancellationToken = default
            )
            where TSigning : ISigning<TSigning>
            where TSigner : IAsyncSignerWithProgress
            where TProgress : IProgress<float>
        {
            return group
                .AsAsync()
                .SignWithAsync(signer, txnsToSign, progress, cancellationToken);
        }

        /// <summary>
        /// Get the transaction indices that have <see cref="Transaction.Sender"/> equal to the given address.
        /// </summary>
        /// <param name="group">The atomic transaction group to look for transactions.</param>
        /// <param name="sender">The address to compare to the transaction's <see cref="Transaction.Sender"/> address.</param>
        /// <typeparam name="TSigning">The type of the signing atomic txn group.</typeparam>
        /// <returns>Transaction indices flagged with the indices in the correct position.</returns>
        public static TxnIndices GetTxnIndicesWithSender<TSigning>(
            this TSigning group,
            Address sender
            )
            where TSigning : ISigning<TSigning>
        {
            var indices = TxnIndices.None;
            for (var i = 0; i < group.Txns.Length; i++)
                if (group.Txns[i].Sender.Equals(sender))
                    indices |= TxnIndices.Select(i);
            return indices;
        }

        /// <summary>
        /// Checks whether the given indices can be signed. Will throw exceptions if they cannot be signed.
        /// </summary>
        /// <remarks>
        /// Indices must be in range of the length of transactions and cannot be associated with
        /// any transactions that are already signed.
        /// </remarks>
        /// <param name="group">The signing atomic txn group.</param>
        /// <param name="txnsToSign">The txn indices to sign.</param>
        /// <typeparam name="TSigning">The type of the signing atomic txn group.</typeparam>
        public static void CheckTxnIndicesToSign<TSigning>(
            this TSigning group,
            TxnIndices txnsToSign
            )
            where TSigning : ISigning<TSigning>
        {
            var commonIndices = txnsToSign & group.SignedTxnIndices;
            if ((int)commonIndices != 0)
            {
                throw new System.ArgumentException(
                    $"The following indices are already signed: [{string.Join(", ", commonIndices)}]",
                    nameof(txnsToSign)
                );
            }

            if ((int)txnsToSign >= (1 << (group.Sigs.Length)))
            {
                throw new System.IndexOutOfRangeException(
                    $"Given indices to sign [{string.Join(", ", txnsToSign)}] are out of range of {group.Sigs.Length} transactions."
                );
            }
        }

        /// <summary>
        /// Utility function to set signatures given by signers to signatures in this signing atomic txn group.
        /// </summary>
        /// <param name="group">The signing atomic txn group.</param>
        /// <param name="signedTxns">The result from signers.</param>
        /// <param name="indices">The txn indices that were going to be signed.</param>
        /// <typeparam name="TSigning">The type of the signing atomic txn group.</typeparam>
        public static void SetSignatures<TSigning>(
            this TSigning group,
            SignedTxn<Transaction>[] signedTxns,
            TxnIndices indices
            )
            where TSigning : ISigning<TSigning>
        {

            if (signedTxns == null || signedTxns.Length != group.Txns.Length)
                throw new System.Exception(
                    $"Was expecting {group.Txns.Length} transactions, but the signer returned {signedTxns?.Length ?? 0} transactions."
                );

            var indexEnum = indices.GetEnumerator();
            while (indexEnum.MoveNext())
            {
                var index = indexEnum.Current;
                group.Sigs[index] = signedTxns[index].Signature;
            }
        }
    }
}

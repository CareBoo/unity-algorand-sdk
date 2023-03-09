using System;
using System.Collections.Generic;
using System.Threading;
using Algorand.Unity.Experimental.Abi;
using Algorand.Unity.MessagePack;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace Algorand.Unity
{
    public static partial class AtomicTxn
    {
        /// <summary>
        /// Represents an Atomic Txn that is being signed with asynchronous signers.
        /// </summary>
        /// <remarks>
        /// Once all signatures have been added, serialize this transaction
        /// group to msgpack with <see cref="FinishSigningAsync"/> or submit it to the network with
        /// <see cref="Submit"/>.
        /// </remarks>
        public partial struct AsyncSigning : ISigning<AsyncSigning>
        {
            private readonly Transaction[] txns;
            private readonly TransactionSignature[] sigs;
            private readonly List<UniTask> asyncSignings;
            private readonly (int, Method)[] methodIndices;

            /// <summary>
            /// Create a new Atomic Txn group that contains some asynchronous signers.
            /// </summary>
            /// <param name="txns">Transactions that are part of this group.</param>
            /// <param name="sigs">Existing signatures.</param>
            /// <param name="signedIndices">Indices of transactions with existing signatures.</param>
            public AsyncSigning(
                Transaction[] txns,
                TransactionSignature[] sigs,
                TxnIndices signedIndices,
                (int, Method)[] methodIndices
                )
            {
                this.txns = txns;
                this.sigs = sigs;
                this.SignedTxnIndices = signedIndices;
                this.asyncSignings = new List<UniTask>();
                this.methodIndices = methodIndices;
            }

            /// <inheritdoc />
            public Transaction[] Txns => txns;

            /// <inheritdoc />
            public TransactionSignature[] Sigs => sigs;

            /// <inheritdoc />
            public TxnIndices SignedTxnIndices { get; set; }

            /// <inheritdoc />
            public AsyncSigning AsAsync() => this;

            /// <inheritdoc cref="AtomicTxn.SignWithAsync{AsyncSigning,TSigner}(AsyncSigning,TSigner,TxnIndices,CancellationToken)" />
            public AsyncSigning SignWithAsync<T>(T signer, TxnIndices txnsToSign, CancellationToken cancellationToken = default)
                where T : IAsyncSigner
            {
                this.CheckTxnIndicesToSign(txnsToSign);

                SignedTxnIndices |= txnsToSign;
                var signing = signer.SignTxnsAsync(txns, txnsToSign, cancellationToken);
                var settingSignatures = SetSignaturesAsync(signing, txnsToSign);
                asyncSignings.Add(settingSignatures);

                return this;
            }

            /// <inheritdoc cref="AtomicTxn.SignWithAsync{AsyncSigning,TSigner,TProgress}(AsyncSigning,TSigner,TxnIndices,TProgress,CancellationToken)" />
            public AsyncSigning SignWithAsync<T, TProgress>(
                T signer,
                TxnIndices txnsToSign,
                TProgress progress,
                CancellationToken cancellationToken = default
                )
                where T : IAsyncSignerWithProgress
                where TProgress : IProgress<float>
            {
                this.CheckTxnIndicesToSign(txnsToSign);

                SignedTxnIndices |= txnsToSign;
                var signing = signer.SignTxnsAsync(txns, txnsToSign, progress, cancellationToken);
                var settingSignatures = SetSignaturesAsync(signing, txnsToSign);
                asyncSignings.Add(settingSignatures);

                return this;
            }

            /// <summary>
            /// Finish signing this group and return the raw msgpack, ready to submit.
            /// </summary>
            /// <returns>Msgpack encoding of the transaction group.</returns>
            public async UniTask<byte[]> FinishSigningAsync()
            {
                using var nativeBytes = await FinishSigningAsync(Allocator.Temp);
                return nativeBytes.AsArray().ToArray();
            }

            /// <summary>
            /// Finish signing this group and return the msgpack of the group, ready to send to the network.
            /// </summary>
            /// <param name="allocator">Allocator to use in the returned NativeList (which must be disposed).</param>
            /// <returns>A natively-allocated, msgpack-encoded transaction group, ready to send to the network.</returns>
            public async UniTask<NativeList<byte>> FinishSigningAsync(Allocator allocator)
            {
                var sigCount = SignedTxnIndices.Count();
                if (sigCount != txns.Length)
                    throw new InvalidOperationException($"There are still {txns.Length - sigCount} transactions that need to be signed.");

                await UniTask.WhenAll(asyncSignings);
                var writer = new MessagePackWriter(allocator);
                for (var i = 0; i < txns.Length; i++)
                {
                    var signedTxn = new SignedTxn { Txn = txns[i], Signature = sigs[i] };
                    AlgoApiFormatterCache<SignedTxn>.Formatter.Serialize(ref writer, signedTxn);
                }
                return writer.Data;
            }

            /// <summary>
            /// Submit the atomic transaction to the network.
            /// </summary>
            /// <param name="algod">The algod client to use</param>
            /// <param name="cancellationToken">An optional cancellationToken to cancel the request early.</param>
            /// <returns>A <see cref="Submitted"/> atomic transaction, ready to be confirmed.</returns>
            public async UniTask<Submitted> Submit(AlgodClient algod, CancellationToken cancellationToken = default)
            {
                var signedBytes = await FinishSigningAsync();
                var (submitErr, submitResponse) = await algod.RawTransaction(signedBytes)
                    .WithCancellation(cancellationToken)
                    ;
                submitErr.ThrowIfError();

                var txnIds = new string[txns.Length];
                for (var i = 0; i < txns.Length; i++)
                {
                    txnIds[i] = txns[i].GetId();
                }

                return new Submitted(algod, txnIds, methodIndices);
            }

            private async UniTask SetSignaturesAsync(UniTask<SignedTxn<Transaction>[]> signing, TxnIndices indices)
            {
                var signedTxns = await signing;
                this.SetSignatures(signedTxns, indices);
            }
        }
    }
}

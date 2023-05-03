using System;
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
        /// Represents an Atomic Txn that is currently being signed.
        /// </summary>
        /// <remarks>
        /// Once all signatures have been added, serialize this transaction
        /// group to msgpack with <see cref="FinishSigning"/> or submit it to the network with
        /// <see cref="Submit"/>.
        /// </remarks>
        public partial struct Signing : ISigning<Signing>
        {
            private readonly Transaction[] txns;
            private readonly TransactionSignature[] sigs;
            private readonly (int, Method)[] methodIndices;

            /// <summary>
            /// Create a new AtomicTransaction that is ready for signing.
            /// This assumes the given transactions already have the correct group id set.
            /// </summary>
            /// <param name="txns">The transactions making up this transaction group.</param>
            public Signing(Transaction[] txns, (int, Method)[] methodIndices)
            {
                this.txns = txns ?? throw new ArgumentNullException(nameof(txns));
                this.sigs = new TransactionSignature[txns.Length];
                this.SignedTxnIndices = TxnIndices.None;
                this.methodIndices = methodIndices;
            }

            /// <inheritdoc />
            public Transaction[] Txns => txns;

            /// <inheritdoc />
            public TransactionSignature[] Sigs => sigs;

            /// <inheritdoc />
            public TxnIndices SignedTxnIndices { get; set; }

            /// <inheritdoc />
            public AsyncSigning AsAsync() => new AsyncSigning(txns, sigs, SignedTxnIndices, methodIndices);

            /// <summary>
            /// Finish signing this group and return the raw msgpack, ready to submit.
            /// </summary>
            /// <returns>Msgpack encoding of the transaction group.</returns>
            public byte[] FinishSigning()
            {
                using var signed = FinishSigning(Allocator.Temp);
                return signed.AsArray().ToArray();
            }

            /// <summary>
            /// Finish signing this group and return the msgpack of the group, ready to send to the network.
            /// </summary>
            /// <param name="allocator">Allocator to use in the returned NativeList (which must be disposed).</param>
            /// <returns>A natively-allocated, msgpack-encoded transaction group, ready to send to the network.</returns>
            public NativeList<byte> FinishSigning(Allocator allocator)
            {
                var sigCount = SignedTxnIndices.Count();
                if (sigCount != txns.Length)
                    throw new InvalidOperationException($"There are still {txns.Length - sigCount} transactions that need to be signed.");

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
                var signedBytes = FinishSigning();
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
        }
    }
}

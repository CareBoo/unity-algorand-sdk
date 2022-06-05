using System;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class AtomicTxn
    {
        /// <summary>
        /// Represents an Atomic Txn that is currently being signed.
        /// </summary>
        /// <remarks>
        /// Once all signatures have been added, serialize this transaction
        /// group to msgpack with <see cref="FinishSigning"/>.
        /// </remarks>
        public partial struct Signing : ISigning<Signing>
        {
            readonly Transaction[] txns;
            readonly TransactionSignature[] sigs;

            /// <summary>
            /// Create a new AtomicTransaction that is ready for signing.
            /// This assumes the given transactions already have the correct group id set.
            /// </summary>
            /// <param name="txns">The transactions making up this transaction group.</param>
            public Signing(Transaction[] txns)
            {
                this.txns = txns ?? throw new ArgumentNullException(nameof(txns));
                this.sigs = new TransactionSignature[txns.Length];
                this.SignedTxnIndices = TxnIndices.None;
            }

            /// <inheritdoc />
            public Transaction[] Txns => txns;

            /// <inheritdoc />
            public TransactionSignature[] Sigs => sigs;

            /// <inheritdoc />
            public TxnIndices SignedTxnIndices { get; set; }

            /// <inheritdoc />
            public AsyncSigning AsAsync() => new AsyncSigning(txns, sigs, SignedTxnIndices);

            /// <summary>
            /// Finish signing this group and return the raw msgpack, ready to submit.
            /// </summary>
            /// <returns>Msgpack encoding of the transaction group.</returns>
            public byte[] FinishSigning()
            {
                using var signed = FinishSigning(Allocator.Persistent);
                return signed.ToArray();
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
        }
    }
}

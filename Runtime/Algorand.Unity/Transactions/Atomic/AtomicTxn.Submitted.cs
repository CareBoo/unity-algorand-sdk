using System.Threading;
using Algorand.Unity.Algod;
using Algorand.Unity.Experimental.Abi;
using Cysharp.Threading.Tasks;

namespace Algorand.Unity
{
    public static partial class AtomicTxn
    {
        /// <summary>
        /// A submitted Atomic Transaction.
        /// </summary>
        public readonly struct Submitted
        {
            private readonly AlgodClient client;
            private readonly (int, Method)[] methodIndices;
            private readonly string[] txnIds;

            /// <summary>
            /// The algod client used to submit these transactions.
            /// </summary>
            public AlgodClient Client => client;

            /// <summary>
            /// Indices of this transaction group that were method calls.
            /// </summary>
            public (int, Method)[] MethodIndices => methodIndices;

            /// <summary>
            /// The txn ids of the transactions in this Atomic Transaction group.
            /// </summary>
            public string[] TxnIds => txnIds;

            public Submitted(AlgodClient client, string[] txnIds, (int, Method)[] methodIndices)
            {
                this.client = client;
                this.methodIndices = methodIndices;
                this.txnIds = txnIds;
            }

            /// <summary>
            /// Wait for confirmation of this atomic transaction group.
            /// </summary>
            /// <param name="maxWaitRounds">Max rounds to wait, if set to 0 will wait Algorand's default wait rounds of 1000</param>
            /// <param name="cancellationToken">An optional cancellationToken to cancel waiting early.</param>
            /// <returns>A <see cref="Confirmed"/> atomic transaction.</returns>
            public async UniTask<Confirmed> Confirm(uint maxWaitRounds = 0, CancellationToken cancellationToken = default)
            {
                var (confirmationError, confirmationResult) = await client.WaitForConfirmation(TxnIds[0], maxWaitRounds, cancellationToken);
                confirmationError.ThrowIfError();
                if (methodIndices == null || methodIndices.Length == 0)
                {
                    return new Confirmed(TxnIds, null, confirmationResult.ConfirmedRound);
                }

                var methodResults = new MethodCallResult[methodIndices.Length];
                var gettingMethodCallInfo = new UniTask<AlgoApiResponse<PendingTransactionResponse>>[methodIndices.Length];

                for (var i = 0; i < methodIndices.Length; i++)
                {
                    var (txnIndex, _) = methodIndices[i];
                    gettingMethodCallInfo[i] = client.PendingTransactionInformation(TxnIds[txnIndex])
                        .WithCancellation(cancellationToken)
                        .ToUniTask();
                }

                for (var i = 0; i < methodIndices.Length; i++)
                {
                    var (txnIndex, method) = methodIndices[i];
                    var (txnInfoErr, txnInfo) = await gettingMethodCallInfo[i];
                    txnInfoErr.ThrowIfError();

                    methodResults[i] = new MethodCallResult(TxnIds[txnIndex], txnInfo, method);
                }

                return new Confirmed(TxnIds, methodResults, confirmationResult.ConfirmedRound);
            }
        }
    }
}

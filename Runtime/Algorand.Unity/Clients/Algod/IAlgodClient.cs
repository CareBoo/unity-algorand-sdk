using System.Threading;
using Algorand.Unity.Algod;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace Algorand.Unity
{
    public static class IAlgodClientExtensions
    {
        /// <summary>
        /// Send a signed transaction struct
        /// </summary>
        /// <param name="client">The type of the client.</param>
        /// <param name="txn">The signed transaction struct to send</param>
        /// <typeparam name="TClient">The type of the client sending the transaction</typeparam>
        /// <typeparam name="TTxn">The type of the signed transaction.</typeparam>
        /// <returns>A response from the algod service.</returns>
        public static AlgoApiRequest.Sent<PostTransactionsResponse> SendTransaction<TClient, TTxn>(
            this TClient client,
            SignedTxn<TTxn> txn
            )
            where TClient : IAlgodClient
            where TTxn : struct, ITransaction, System.IEquatable<TTxn>
        {
            using var data = AlgoApiSerializer.SerializeMessagePack(txn, Allocator.Temp);
            return client.RawTransaction(data.AsArray().ToArray());
        }

        /// <summary>
        /// Utility method to wait for a transaction to be confirmed given a transaction id.
        /// </summary>
        /// <param name="client">The type of the client.</param>
        /// <param name="txid">The transaction id to wait for.</param>
        /// <param name="maxWaitRounds">How many rounds should this method wait for confirmation before cancelling early?</param>
        /// <param name="cancellationToken">An optional token for cancelling this task early.</param>
        /// <typeparam name="TClient">The type of the client sending the transaction</typeparam>
        /// <returns>The algod response that either caused an error or showed a confirmed round.</returns>
        public static async UniTask<AlgoApiResponse<PendingTransactionResponse>> WaitForConfirmation<TClient>(
            this TClient client,
            string txid,
            uint maxWaitRounds = default,
            CancellationToken cancellationToken = default
            )
            where TClient : IAlgodClient
        {
            if (maxWaitRounds == 0)
            {
                maxWaitRounds = 1000;
            }

            var statusResponse = await client.GetStatus();
            if (statusResponse.Error)
            {
                return statusResponse.Cast<PendingTransactionResponse>();
            }
            var lastRound = statusResponse.Payload.LastRound;
            var currentRound = lastRound + 1;

            while (currentRound < lastRound + maxWaitRounds)
            {
                var txnInfoResponse = await client.PendingTransactionInformation(txid);
                var (txnInfoError, txnInfo) = txnInfoResponse;
                if (txnInfoError || !string.IsNullOrEmpty(txnInfo.PoolError) || txnInfo.ConfirmedRound > 0)
                {
                    return txnInfoResponse;
                }

                await client.WaitForBlock(currentRound);
                currentRound++;
            }
            return new AlgoApiResponse(new ErrorResponse { Message = $"Waiting for transaction id {txid} timed out" });
        }
    }
}

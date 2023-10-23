using System;
using System.Threading;
using Algorand.Unity.Algod;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// A client for accessing the algod service
    /// </summary>
    /// <remarks>
    /// The algod service is responsible for handling information
    /// required to create and send transactions.
    /// </remarks>
    [Serializable]
    public partial struct AlgodClient
    {
        [SerializeField] private string address;

        [SerializeField] private string token;

        [SerializeField] private Header[] headers;

        /// <summary>
        /// Create a new algod client with a token set to <see cref="TokenHeader"/>.
        /// </summary>
        /// <param name="address">url of the algod service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="token">token used in authenticating to the algod service</param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key, my-api-key")</c></param>
        public AlgodClient(string address, string token, params Header[] headers)
        {
            this.address = address.TrimEnd('/');
            this.token = token;
            this.headers = headers;
        }

        /// <summary>
        /// Create a new algod client without using a token.
        /// </summary>
        /// <param name="address">url of the algod service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key, my-api-key")</c></param>
        public AlgodClient(string address, params Header[] headers) : this(address, null, headers)
        {
        }

        /// <inheritdoc />
        public string Address => address;

        /// <inheritdoc />
        public string Token => token;

        /// <inheritdoc />
        public string TokenHeader => "X-Algo-API-Token";

        /// <inheritdoc />
        public Header[] Headers => headers;

        /// <summary>
        /// Send a signed transaction struct
        /// </summary>
        /// <param name="txn">The signed transaction struct to send</param>
        /// <typeparam name="T">The type of the signed transaction.</typeparam>
        /// <returns>A response from the algod service.</returns>
        public AlgoApiRequest.Sent<PostTransactionsResponse> SendTransaction<T>(SignedTxn<T> txn)
            where T : struct, ITransaction, IEquatable<T>
        {
            using var data = AlgoApiSerializer.SerializeMessagePack(txn, Allocator.Temp);
            return RawTransaction(data.AsArray().ToArray());
        }

        /// <summary>
        /// Utility method to wait for a transaction to be confirmed given a transaction id.
        /// </summary>
        /// <param name="txid">The transaction id to wait for.</param>
        /// <param name="maxWaitRounds">How many rounds should this method wait for confirmation before cancelling early?</param>
        /// <param name="cancellationToken">An optional token for cancelling this task early.</param>
        /// <returns>The algod response that either caused an error or showed a confirmed round.</returns>
        public async UniTask<AlgoApiResponse<PendingTransactionResponse>> WaitForConfirmation(
            string txid,
            uint maxWaitRounds = default,
            CancellationToken cancellationToken = default
        )
        {
            if (maxWaitRounds == 0)
            {
                maxWaitRounds = 1000;
            }

            var statusResponse = await GetStatus();
            if (statusResponse.Error)
            {
                return statusResponse.Cast<PendingTransactionResponse>();
            }

            var lastRound = statusResponse.Payload.LastRound;
            var currentRound = statusResponse.Payload.LastRound + 1;

            while (currentRound < lastRound + maxWaitRounds)
            {
                var txnInfoResponse = await PendingTransactionInformation(txid);
                var (txnInfoError, txnInfo) = txnInfoResponse;
                if (txnInfoError || !string.IsNullOrEmpty(txnInfo.PoolError) || txnInfo.ConfirmedRound > 0)
                {
                    return txnInfoResponse;
                }

                await WaitForBlock(currentRound).WithCancellation(cancellationToken);
                currentRound++;
            }

            return new AlgoApiResponse(
                new ErrorResponse
                {
                    Message = $"Waiting for transaction id {txid} timed out"
                });
        }
    }
}

using System;
using System.Threading;
using AlgoSdk.Algod;
using AlgoSdk.LowLevel;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// A client for accessing the algod service
    /// </summary>
    /// <remarks>
    /// The algod service is responsible for handling information
    /// required to create and send transactions.
    /// </remarks>
    [Serializable]
    [Obsolete("Please use Algorand.Algod.DefaultApi instead.")]
    public partial struct AlgodClient : IAlgodClient
    {
        [SerializeField]
        string address;

        [SerializeField]
        string token;

        [SerializeField]
        Header[] headers;

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

        public string Address => address;

        public string Token => token;

        public string TokenHeader => "X-Algo-API-Token";

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
            using var data = AlgoApiSerializer.SerializeMessagePack(txn, Allocator.Persistent);
            return RawTransaction(data.ToArray());
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

                await WaitForBlock(currentRound);
                currentRound++;
            }
            return new AlgoApiResponse(new ErrorResponse { Message = $"Waiting for transaction id {txid} timed out" });
        }

        [Obsolete("Call AlgodClient.GetGenesis instead.")]
        public AlgoApiRequest.Sent<AlgoApiObject> GetGenesisInformation()
        {
            return GetGenesis();
        }

        [Obsolete("Call HealthCheck instead.")]
        public AlgoApiRequest.Sent GetHealth()
        {
            return HealthCheck();
        }

        [Obsolete("Call Metrics instead.")]
        public AlgoApiRequest.Sent GetMetrics()
        {
            return Metrics();
        }

        [Obsolete("Call SwaggerJSON instead.")]
        public AlgoApiRequest.Sent<AlgoApiObject> GetSwaggerSpec()
        {
            return SwaggerJSON();
        }

        [Obsolete("Call AccountInformation instead.")]
        public AlgoApiRequest.Sent<AccountResponse> GetAccountInformation(Address accountAddress)
        {
            return AccountInformation(accountAddress);
        }

        [Obsolete("Call GetPendingTransactionsByAddress instead.")]
        public AlgoApiRequest.Sent<PendingTransactionsResponse> GetPendingTransactionsByAccount(Address accountAddress, ulong max = 0)
        {
            Optional<ulong> optionalMax = default;
            if (max != 0)
                optionalMax = max;
            return GetPendingTransactionsByAddress(accountAddress, optionalMax);
        }

        [Obsolete("Call PendingTransactionInformation instead.")]
        public AlgoApiRequest.Sent<PendingTransactionResponse> GetPendingTransaction(TransactionId txid)
        {
            return PendingTransactionInformation(txid.ToString());
        }

        [Obsolete("Call GetApplicationByID instead.")]
        public AlgoApiRequest.Sent<ApplicationResponse> GetApplication(ulong applicationId)
        {
            return GetApplicationByID(applicationId);
        }

        [Obsolete("Call GetAssetByID instead.")]
        public AlgoApiRequest.Sent<AssetResponse> GetAsset(ulong assetId)
        {
            return GetAssetByID(assetId);
        }


        [Obsolete("Call GetProof instead.")]
        public AlgoApiRequest.Sent<ProofResponse> GetMerkleProof(ulong round, TransactionId txid)
        {
            return GetProof(round, txid);
        }

        [Obsolete("Call GetSupply instead.")]
        public AlgoApiRequest.Sent<SupplyResponse> GetLedgerSupply()
        {
            return GetSupply();
        }

        [Obsolete("Call AddParticipationKey instead.", true)]
        public AlgoApiRequest.Sent<PostTransactionsResponse> RegisterParticipationKeys(
            string accountAddress,
            ulong fee = 1000,
            Optional<ulong> keyDilution = default,
            Optional<bool> noWait = default,
            Optional<bool> roundLastValid = default)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Call ShutdownNode instead.")]
        public AlgoApiRequest.Sent<AlgoApiObject> ShutDown(Optional<ulong> timeout = default)
        {
            return ShutdownNode(timeout);
        }

        [Obsolete("Call GetStatus instead.")]
        public AlgoApiRequest.Sent<NodeStatusResponse> GetCurrentStatus()
        {
            return GetStatus();
        }

        [Obsolete("Call WaitForBlock instead.")]
        public AlgoApiRequest.Sent<NodeStatusResponse> GetStatusAfterWaitingForRound(ulong round)
        {
            return WaitForBlock(round);
        }

        [Obsolete("Call RawTransaction instead.")]
        public AlgoApiRequest.Sent<PostTransactionsResponse> SendTransaction(byte[] txn)
        {
            using var data = new NativeArray<byte>(txn, Allocator.Persistent);
            return RawTransaction(data.ToArray());
        }

        [Obsolete("Call RawTransaction instead.")]
        public AlgoApiRequest.Sent<PostTransactionsResponse> SendTransactions(
            params SignedTxn[] txns
        )
        {
            using var bytes = new NativeList<byte>(Allocator.Persistent);
            for (var i = 0; i < txns.Length; i++)
            {
                using var data = AlgoApiSerializer.SerializeMessagePack(txns[i], Allocator.Persistent);
                bytes.AddRange(data);
            }
            return SendTransactions(bytes.AsArray().AsReadOnly());
        }

        [Obsolete("Call RawTransaction instead.")]
        public AlgoApiRequest.Sent<PostTransactionsResponse> SendTransactions(params byte[][] signedTxns)
        {
            using var bytes = NativeArrayUtil.ConcatAll(signedTxns, Allocator.Persistent);
            return SendTransactions(bytes.AsReadOnly());
        }

        [Obsolete("Call TransactionParams instead.")]
        public AlgoApiRequest.Sent<TransactionParametersResponse> GetSuggestedParams()
        {
            return TransactionParams();
        }

        [Obsolete("Call GetVersion instead.")]
        public AlgoApiRequest.Sent<VersionsResponse> GetVersions()
        {
            return GetVersion();
        }

        [Obsolete("Call RawTransaction instead.")]
        AlgoApiRequest.Sent<PostTransactionsResponse> SendTransactions(NativeArray<byte>.ReadOnly rawTxnData)
        {
            return RawTransaction(rawTxnData.ToArray());
        }
    }
}

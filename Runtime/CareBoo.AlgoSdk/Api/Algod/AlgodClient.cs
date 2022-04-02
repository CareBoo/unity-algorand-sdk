using System;
using AlgoSdk.LowLevel;
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

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AlgoApiObject> GetGenesisInformation()
        {
            return this
                .Get("/genesis")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent GetHealth()
        {
            return this
                .Get("/health")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent GetMetrics()
        {
            return this
                .Get("/metrics")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AlgoApiObject> GetSwaggerSpec()
        {
            return this
                .Get("/swagger.json")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AccountInfo> GetAccountInformation(Address accountAddress)
        {
            return this
                .Get($"/v2/accounts/{accountAddress}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<PendingTransactions> GetPendingTransactionsByAccount(Address accountAddress, ulong max = 0)
        {
            return this
                .Get($"/v2/accounts/{accountAddress}/transactions/pending?max={max}&format=msgpack")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<PendingTransaction> GetPendingTransaction(TransactionId txid)
        {
            return this
                .Get($"/v2/transactions/pending/{txid}?format=msgpack")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<Application> GetApplication(ulong applicationId)
        {
            return this
                .Get($"/v2/applications/{applicationId}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<Asset> GetAsset(ulong assetId)
        {
            return this
                .Get($"/v2/assets/{assetId}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<BlockResponse> GetBlock(ulong round)
        {
            return this
                .Get($"/v2/blocks/{round}?format=msgpack")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<MerkleProof> GetMerkleProof(ulong round, TransactionId txid)
        {
            return this
                .Get($"/v2/blocks/{round}/transactions/{txid}/proof")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<CatchupMessage> AbortCatchup(string catchpoint)
        {
            return this
                .Delete($"/v2/catchup/{catchpoint}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<LedgerSupply> GetLedgerSupply()
        {
            return this
                .Get("/v2/ledger/supply")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionIdResponse> RegisterParticipationKeys(
            string accountAddress,
            ulong fee = 1000,
            Optional<ulong> keyDilution = default,
            Optional<bool> noWait = default,
            Optional<bool> roundLastValid = default)
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("fee", fee, (ulong)1000)
                .Add("key-dilution", keyDilution)
                .Add("no-wait", noWait)
                .Add("round-last-valid", roundLastValid)
                ;
            var endpoint = $"/v2/register-participation-keys/{accountAddress}{queryBuilder}";
            return AlgoApiRequest.Post(this.GetUrl(endpoint))
                .SetHeaders(Headers)
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent ShutDown(Optional<ulong> timeout = default)
        {
            return this
                .Post("/v2/shutdown")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<Status> GetCurrentStatus()
        {
            return this
                .Get("/v2/status")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<Status> GetStatusAfterWaitingForRound(ulong round)
        {
            return this
                .Get($"/v2/status/wait-for-block-after/{round}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TealCompilationResult> TealCompile(string source)
        {
            return this
                .Post("/v2/teal/compile")
                .SetPlainTextBody(source)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<DryrunResults> TealDryrun(Optional<DryrunRequest> dryrunRequest = default)
        {
            const string endpoint = "/v2/teal/dryrun";
            using var data = AlgoApiSerializer.SerializeMessagePack(dryrunRequest.Value, Allocator.Persistent);
            return dryrunRequest.HasValue
                ? this
                    .Post(endpoint)
                    .SetMessagePackBody(data.AsArray().AsReadOnly())
                    .Send()
                : this
                    .Post(endpoint)
                    .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionIdResponse> SendTransaction<T>(SignedTxn<T> txn)
            where T : struct, ITransaction, IEquatable<T>
        {
            using var data = AlgoApiSerializer.SerializeMessagePack(txn, Allocator.Persistent);
            return SendTransactions(data.AsArray().AsReadOnly());
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionIdResponse> SendTransaction(byte[] txn)
        {
            using var data = new NativeArray<byte>(txn, Allocator.Persistent);
            return SendTransactions(data.AsReadOnly());
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionIdResponse> SendTransactions(
            params SignedTxn[] txns
        )
        {
            using var bytes = new NativeList<byte>(Allocator.Persistent);
            for (var i = 0; i < txns.Length; i++)
            {
                using var data = AlgoApiSerializer.SerializeMessagePack(txns[i], Allocator.Temp);
                bytes.AddRange(data);
            }
            return SendTransactions(bytes.AsArray().AsReadOnly());
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionIdResponse> SendTransactions(params byte[][] signedTxns)
        {
            using var bytes = NativeArrayUtil.ConcatAll(signedTxns, Allocator.Persistent);
            return SendTransactions(bytes.AsReadOnly());
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionParams> GetSuggestedParams()
        {
            return this
                .Get("/v2/transactions/params")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<Version> GetVersions()
        {
            return this
                .Get("/versions")
                .Send();
        }

        /// <inheritdoc />
        AlgoApiRequest.Sent<TransactionIdResponse> SendTransactions(NativeArray<byte>.ReadOnly rawTxnData)
        {
            return this
                .Post("/v2/transactions")
                .SetMessagePackBody(rawTxnData)
                .Send();
        }
    }
}

using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// A client for accessing the indexer service
    /// </summary>
    /// <remarks>
    /// The indexer service is responsible for querying the blockchain
    /// </remarks>
    [Serializable]
    public struct IndexerClient : IIndexerClient
    {
        [SerializeField]
        string address;

        [SerializeField]
        string token;

        [SerializeField]
        Header[] headers;

        /// <summary>
        /// Create a new indexer client with a token set for <see cref="TokenHeader"/>.
        /// </summary>
        /// <param name="address">url of the service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="token">token used in authenticating to the service</param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key", "my-private-key")</c></param>
        public IndexerClient(string address, string token = null, params Header[] headers)
        {
            this.address = address.TrimEnd('/');
            this.token = token;
            this.headers = headers;
        }

        /// <summary>
        /// Create a new indexer client
        /// </summary>
        /// <param name="address">url of the service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key", "my-private-key")</c></param>
        public IndexerClient(string address, params Header[] headers) : this(address, null, headers)
        {
        }

        public string Address => address;

        public string Token => token;

        public string TokenHeader => "X-Indexer-API-Token";

        public Header[] Headers => headers;

        /// <inheritdoc />
        public AlgoApiRequest.Sent<HealthCheck> GetHealth()
        {
            return this
                .Get("/health")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AccountsResponse> GetAccounts(
            Optional<ulong> applicationId = default,
            Optional<ulong> assetId = default,
            Address authAddr = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default,
            Optional<ulong> round = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder
                .Add("application-id", applicationId)
                .Add("asset-id", assetId)
                .Add("auth-addr", authAddr)
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("include-all", includeAll)
                .Add("limit", limit)
                .Add("next", next)
                .Add("round", round)
                .ToString()
                ;
            return this
                .Get("/v2/accounts" + query)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AccountResponse> GetAccount(
            Address accountAddress,
            Optional<bool> includeAll = default,
            Optional<ulong> round = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder
                .Add("include-all", includeAll)
                .Add("round", round)
                .ToString()
                ;
            return this
                .Get($"/v2/accounts/{accountAddress}{query}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionsResponse> GetAccountTransactions(
            Address accountAddress,
            DateTime afterTime = default,
            Optional<ulong> assetId = default,
            DateTime beforeTime = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<ulong> limit = default,
            Optional<ulong> maxRound = default,
            Optional<ulong> minRound = default,
            Optional<FixedString128Bytes> next = default,
            string notePrefix = default,
            Optional<bool> rekeyTo = default,
            Optional<ulong> round = default,
            SignatureType sigType = default,
            TransactionType txType = default,
            TransactionId txid = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder
                .Add("after-time", afterTime)
                .Add("asset-id", assetId)
                .Add("before-time", beforeTime)
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("limit", limit)
                .Add("max-round", maxRound)
                .Add("min-round", minRound)
                .Add("next", next)
                .Add("note-prefix", notePrefix)
                .Add("rekey-to", rekeyTo)
                .Add("round", round)
                .Add("sig-type", sigType.ToOptionalFixedString())
                .Add("tx-type", txType.ToOptionalFixedString())
                .Add("txid", txid)
                .ToString()
                ;
            return this
                .Get($"/v2/accounts/{accountAddress}/transactions{query}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ApplicationsResponse> GetApplications(
            Optional<ulong> applicationId = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder
                .Add("application-id", applicationId)
                .Add("include-all", includeAll)
                .Add("limit", limit)
                .Add("next", next)
                .ToString()
                ;
            return this
                .Get("/v2/applications" + query)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ApplicationResponse> GetApplication(
            ulong applicationId,
            Optional<bool> includeAll = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder
                .Add("include-all", includeAll)
                ;
            return this
                .Get($"/v2/applications/{applicationId}{query}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AssetsResponse> GetAssets(
            Optional<ulong> assetId = default,
            Address creator = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            string name = default,
            Optional<FixedString128Bytes> next = default,
            string unit = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder.Add("asset-id", assetId)
                .Add("creator", creator)
                .Add("include-all", includeAll)
                .Add("limit", limit)
                .Add("name", name)
                .Add("next", next)
                .Add("unit", unit)
                .ToString()
                ;
            return this
                .Get($"/v2/assets{query}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AssetResponse> GetAsset(
            ulong assetId,
            Optional<bool> includeAll = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder
                .Add("include-all", includeAll)
                ;
            return this
                .Get($"/v2/assets/{assetId}{query}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<BalancesResponse> GetAssetBalances(
            ulong assetId,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default,
            Optional<ulong> round = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("include-all", includeAll)
                .Add("limit", limit)
                .Add("next", next)
                .Add("round", round)
                .ToString()
                ;
            return this
                .Get($"/v2/assets/{assetId}/balances{query}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionsResponse> GetAssetTransactions(
            ulong assetId,
            Address address = default,
            AddressRole addressRole = default,
            DateTime afterTime = default,
            DateTime beforeTime = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> excludeCloseTo = default,
            Optional<ulong> limit = default,
            Optional<ulong> maxRound = default,
            Optional<ulong> minRound = default,
            Optional<FixedString128Bytes> next = default,
            string notePrefix = default,
            Optional<bool> rekeyTo = default,
            Optional<ulong> round = default,
            SignatureType sigType = default,
            TransactionType txType = default,
            TransactionId txid = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder
                .Add("address", address)
                .Add("address-role", addressRole.ToOptionalFixedString())
                .Add("after-time", afterTime)
                .Add("before-time", beforeTime)
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("exclude-close-to", excludeCloseTo)
                .Add("limit", limit)
                .Add("max-round", maxRound)
                .Add("min-round", minRound)
                .Add("next", next)
                .Add("note-prefix", notePrefix)
                .Add("rekey-to", rekeyTo)
                .Add("round", round)
                .Add("sig-type", sigType.ToOptionalFixedString())
                .Add("tx-type", txType.ToOptionalFixedString())
                .Add("txid", txid)
                .ToString()
                ;
            return this
                .Get($"/v2/assets/{assetId}/transactions{query}")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<BlockHeader> GetBlock(ulong round)
        {
            return this.Get($"/v2/blocks/{round}").Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionsResponse> GetTransactions(
            Address address = default,
            AddressRole addressRole = default,
            DateTime afterTime = default,
            Optional<ulong> applicationId = default,
            Optional<ulong> assetId = default,
            DateTime beforeTime = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> excludeCloseTo = default,
            Optional<ulong> limit = default,
            Optional<ulong> maxRound = default,
            Optional<ulong> minRound = default,
            Optional<FixedString128Bytes> next = default,
            string notePrefix = default,
            Optional<bool> rekeyTo = default,
            Optional<ulong> round = default,
            SignatureType sigType = default,
            TransactionType txType = default,
            TransactionId txid = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            var query = queryBuilder
                .Add("address", address)
                .Add("address-role", addressRole.ToOptionalFixedString())
                .Add("after-time", afterTime)
                .Add("application-id", applicationId)
                .Add("asset-id", assetId)
                .Add("before-time", beforeTime)
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("exclude-close-to", excludeCloseTo)
                .Add("limit", limit)
                .Add("max-round", maxRound)
                .Add("min-round", minRound)
                .Add("next", next)
                .Add("note-prefix", notePrefix)
                .Add("rekey-to", rekeyTo)
                .Add("round", round)
                .Add("sig-type", sigType.ToOptionalFixedString())
                .Add("tx-type", txType.ToOptionalFixedString())
                .Add("txid", txid)
                .ToString()
                ;
            return this
                .Get("/v2/transactions" + query)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionResponse> GetTransaction(TransactionId txid)
        {
            return this
                .Get($"/v2/transactions/{txid}")
                .Send();
        }
    }
}

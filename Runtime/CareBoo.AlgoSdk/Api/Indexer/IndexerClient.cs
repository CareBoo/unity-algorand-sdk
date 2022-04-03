using System;
using System.Text;
using AlgoSdk.Indexer;
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
    public partial struct IndexerClient
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



        [Obsolete("Call MakeHealthCheck instead.")]
        public AlgoApiRequest.Sent<HealthCheckResponse> GetHealth()
        {
            return MakeHealthCheck();
        }

        [Obsolete("Call SearchForAccounts instead.")]
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
            return SearchForAccounts(
                applicationId: applicationId,
                assetId: assetId,
                authAddr: authAddr,
                currencyGreaterThan: currencyGreaterThan,
                currencyLessThan: currencyLessThan,
                includeAll: includeAll,
                limit: limit,
                next: next.HasValue ? next.Value.ToString() : null,
                round: round
            );
        }

        [Obsolete("Call LookupAccountByID instead.")]
        public AlgoApiRequest.Sent<AccountResponse> GetAccount(
            Address accountAddress,
            Optional<bool> includeAll = default,
            Optional<ulong> round = default
        )
        {
            return LookupAccountByID(accountAddress, round, includeAll);
        }

        [Obsolete("Call LookupAccountTransactions instead.")]
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
            return LookupAccountTransactions(
                accountAddress,
                afterTime: afterTime,
                assetId: assetId,
                beforeTime: beforeTime,
                currencyGreaterThan: currencyGreaterThan,
                currencyLessThan: currencyLessThan,
                limit: limit,
                maxRound: maxRound,
                minRound: minRound,
                next: next.HasValue ? next.Value.ToString() : null,
                notePrefix: notePrefix != null ? Encoding.UTF8.GetBytes(notePrefix) : null,
                rekeyTo: rekeyTo,
                round: round,
                txType: txType,
                txid: txid.Equals(default) ? null : txid
            );
        }

        [Obsolete("Call SearchForApplications instead.")]
        public AlgoApiRequest.Sent<ApplicationsResponse> GetApplications(
            Optional<ulong> applicationId = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default
        )
        {
            return SearchForApplications(
                applicationId: applicationId,
                includeAll: includeAll,
                limit: limit,
                next: next.HasValue ? next.Value.ToString() : null
            );
        }

        [Obsolete("Call LookupApplicationByID instead.")]
        public AlgoApiRequest.Sent<ApplicationResponse> GetApplication(
            ulong applicationId,
            Optional<bool> includeAll = default
        )
        {
            return LookupApplicationByID(applicationId, includeAll);
        }

        [Obsolete("Call SearchForAssets instead.")]
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
            return SearchForAssets(
                assetId: assetId,
                creator: creator.Equals(default) ? null : creator.ToString(),
                includeAll: includeAll,
                limit: limit,
                name: name,
                next: next.HasValue ? next.Value.ToString() : null,
                unit: unit
            );
        }

        [Obsolete("Call LookupAssetByID instead.")]
        public AlgoApiRequest.Sent<AssetResponse> GetAsset(
            ulong assetId,
            Optional<bool> includeAll = default
        )
        {
            return LookupAssetByID(assetId, includeAll);
        }

        [Obsolete("Call LookupAssetBalances instead.")]
        public AlgoApiRequest.Sent<AssetBalancesResponse> GetAssetBalances(
            ulong assetId,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default,
            Optional<ulong> round = default
        )
        {
            return LookupAssetBalances(
                assetId,
                currencyGreaterThan: currencyGreaterThan,
                currencyLessThan: currencyLessThan,
                includeAll: includeAll,
                limit: limit,
                next: next.HasValue ? next.Value.ToString() : null
            );
        }

        [Obsolete("Call LookupAssetTransactions instead.")]
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
            return LookupAssetTransactions(
                assetId,
                address: address.Equals(default) ? null : address.ToString(),
                addressRole: addressRole,
                afterTime: afterTime,
                beforeTime: beforeTime,
                currencyGreaterThan: currencyGreaterThan,
                currencyLessThan: currencyLessThan,
                excludeCloseTo: excludeCloseTo,
                limit: limit,
                maxRound: maxRound,
                minRound: minRound,
                next: next.HasValue ? next.Value.ToString() : null,
                notePrefix: notePrefix != null ? Encoding.UTF8.GetBytes(notePrefix) : null,
                rekeyTo: rekeyTo,
                round: round,
                sigType: sigType,
                txType: txType,
                txid: txid.Equals(default) ? null : txid.ToString()
            );
        }

        [Obsolete("Call LookupBlock instead.")]
        public AlgoApiRequest.Sent<BlockResponse> GetBlock(ulong round)
        {
            return LookupBlock(round);
        }

        [Obsolete("Call SearchForTransactions instead.")]
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
            return SearchForTransactions(
                address: address.Equals(default) ? null : address.ToString(),
                addressRole: addressRole,
                afterTime: afterTime,
                beforeTime: beforeTime,
                currencyGreaterThan: currencyGreaterThan,
                currencyLessThan: currencyLessThan,
                excludeCloseTo: excludeCloseTo,
                limit: limit,
                maxRound: maxRound,
                minRound: minRound,
                next: next.HasValue ? next.Value.ToString() : null,
                notePrefix: notePrefix != null ? Encoding.UTF8.GetBytes(notePrefix) : null,
                rekeyTo: rekeyTo,
                round: round,
                sigType: sigType,
                txType: txType,
                txid: txid.Equals(default) ? null : txid.ToString()
            );
        }

        [Obsolete("Call LookupTransaction instead.")]
        public AlgoApiRequest.Sent<TransactionResponse> GetTransaction(TransactionId txid)
        {
            return LookupTransaction(txid);
        }
    }
}

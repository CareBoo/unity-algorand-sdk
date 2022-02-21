using System;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IIndexerClient : IAlgoApiClient
    {
        /// <summary>
        /// Get a health status of the indexer service.
        /// </summary>
        /// <returns>a <see cref="HealthCheck"/> response detailing the status of the health</returns>
        UniTask<AlgoApiResponse<HealthCheck>> GetHealth();

        /// <summary>
        /// Search for accounts.
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <param name="assetId">Asset ID</param>
        /// <param name="authAddr">Include accounts configured to use this spending key.</param>
        /// <param name="currencyGreaterThan">
        /// Results should have an amount greater than this value.
        /// MicroAlgos are the default currency unless an asset-id is
        /// provided, in which case the asset will be used.
        /// </param>
        /// <param name="currencyLessThan">
        /// Results should have an amount less than this value. MicroAlgos
        /// are the default currency unless an asset-id is provided, in which
        /// case the asset will be used.
        /// </param>
        /// <param name="includeAll">
        /// Include all items including closed accounts, deleted
        /// applications, destroyed assets, opted-out asset holdings, and
        /// closed-out application localstates.
        /// </param>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">
        /// The next page of results. Use the next token provided by the
        /// previous results.
        /// </param>
        /// <param name="round">
        /// Include results for the specified round. For performance
        /// reasons, this parameter may be disabled on some
        /// configurations.
        /// </param>
        UniTask<AlgoApiResponse<AccountsResponse>> GetAccounts(
            Optional<ulong> applicationId = default,
            Optional<ulong> assetId = default,
            Address authAddr = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default,
            Optional<ulong> round = default
        );

        /// <summary>
        /// Lookup account information.
        /// </summary>
        /// <param name="accountAddress">account address</param>
        /// <param name="includeAll">
        /// Include all items including closed accounts, deleted applications,
        /// destroyed assets, opted-out asset holdings, and closed-out
        /// application localstates.
        /// </param>
        /// <param name="round">Include results for the specified round.</param>
        UniTask<AlgoApiResponse<AccountResponse>> GetAccount(
            Address accountAddress,
            Optional<bool> includeAll = default,
            Optional<ulong> round = default
        );

        /// <summary>
        /// Lookup account transactions.
        /// </summary>
        /// <param name="accountAddress">account address</param>
        /// <param name="afterTime">Include results after the given time.</param>
        /// <param name="assetId">Asset ID</param>
        /// <param name="beforeTime">Include results before the given time.</param>
        /// <param name="currencyGreaterThan">
        /// Results should have an amount greater than this value.
        /// MicroAlgos are the default currency unless an asset-id
        /// is provided, in which case the asset will be used.
        /// </param>
        /// <param name="currencyLessThan">
        /// Results should have an amount less than this value.
        /// MicroAlgos are the default currency unless an asset-id
        /// is provided, in which case the asset will be used.
        /// </param>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="maxRound">Include results at or before the specified max-round.</param>
        /// <param name="minRound">Include results at or after the specified min-round.</param>
        /// <param name="next">
        /// The next page of results. Use the next token provided
        /// by the previous results.
        /// </param>
        /// <param name="notePrefix">
        /// Specifies a prefix which must be contained in the note
        /// field.
        /// </param>
        /// <param name="rekeyTo">Include results which include the rekey-to field.</param>
        /// <param name="round">Include results for the specified round.</param>
        /// <param name="sigType">Filter transactions using the given <see cref="SignatureType"/></param>
        /// <param name="txType"></param>
        /// <param name="txid">Lookup the specific transaction by ID.</param>
        UniTask<AlgoApiResponse<TransactionsResponse>> GetAccountTransactions(
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
        );

        /// <summary>
        /// Search for applications
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <param name="includeAll">
        /// Include all items including closed accounts, deleted applications,
        /// destroyed assets, opted-out asset holdings, and closed-out
        /// application localstates.
        /// </param>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">
        /// The next page of results. Use the next token provided by the
        /// previous results.
        /// </param>
        UniTask<AlgoApiResponse<ApplicationsResponse>> GetApplications(
            Optional<ulong> applicationId = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default
        );

        /// <summary>
        /// Lookup application.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="includeAll">
        /// Include all items including closed accounts, deleted applications,
        /// destroyed assets, opted-out asset holdings, and closed-out
        /// application localstates.
        /// </param>
        UniTask<AlgoApiResponse<ApplicationResponse>> GetApplication(
            ulong applicationId,
            Optional<bool> includeAll = default
        );

        /// <summary>
        /// Search for assets.
        /// </summary>
        /// <param name="assetId">Asset ID</param>
        /// <param name="creator">Filter just assets with the given creator address.</param>
        /// <param name="includeAll">
        /// Include all items including closed accounts, deleted applications,
        /// destroyed assets, opted-out asset holdings, and closed-out
        /// application localstates.
        /// </param>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="name">Filter just assets with the given name.</param>
        /// <param name="next">
        /// The next page of results. Use the next token provided by the
        /// previous results.
        /// </param>
        /// <param name="unit">Filter just assets with the given unit.</param>
        UniTask<AlgoApiResponse<AssetsResponse>> GetAssets(
            Optional<ulong> assetId = default,
            Address creator = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            string name = default,
            Optional<FixedString128Bytes> next = default,
            string unit = default
        );

        /// <summary>
        /// Lookup asset information.
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="includeAll">
        /// Include all items including closed accounts, deleted applications,
        /// destroyed assets, opted-out asset holdings, and closed-out
        /// application localstates.
        /// </param>
        UniTask<AlgoApiResponse<AssetResponse>> GetAsset(
            ulong assetId,
            Optional<bool> includeAll = default
        );

        /// <summary>
        /// Lookup the list of accounts who hold this asset
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="currencyGreaterThan">Results should have an amount greater than this value. MicroAlgos are the default currency unless an asset-id is provided, in which case the asset will be used.</param>
        /// <param name="currencyLessThan">Results should have an amount less than this value. MicroAlgos are the default currency unless an asset-id is provided, in which case the asset will be used.</param>
        /// <param name="includeAll">Include all items including closed accounts, deleted applications, destroyed assets, opted-out asset holdings, and closed-out application localstates.</param>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="round">Include results for the specified round.</param>
        UniTask<AlgoApiResponse<BalancesResponse>> GetAssetBalances(
            ulong assetId,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default,
            Optional<ulong> round = default
        );

        /// <summary>
        /// Lookup transactions for an asset.
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="address">Only include transactions with this address in one of the transaction fields.</param>
        /// <param name="addressRole">Combine with the address parameter to define what type of address to search for.</param>
        /// <param name="afterTime">Include results after the given time.</param>
        /// <param name="beforeTime">Include results before the given time.</param>
        /// <param name="currencyGreaterThan">Results should have an amount greater than this value. MicroAlgos are the default currency unless an asset-id is provided, in which case the asset will be used.</param>
        /// <param name="currencyLessThan">Results should have an amount less than this value. MicroAlgos are the default currency unless an asset-id is provided, in which case the asset will be used.</param>
        /// <param name="excludeCloseTo">Combine with address and address-role parameters to define what type of address to search for. The close to fields are normally treated as a receiver, if you would like to exclude them set this parameter to true.</param>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="maxRound">Include results at or before the specified max-round.</param>
        /// <param name="minRound">Include results at or after the specified min-round.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="notePrefix">Specifies a prefix which must be contained in the note field.</param>
        /// <param name="rekeyTo">Include results which include the rekey-to field.</param>
        /// <param name="round">Include results for the specified round.</param>
        /// <param name="sigType">SigType filters just results using the specified type of signature.</param>
        /// <param name="txType"></param>
        /// <param name="txid">Lookup the specific transaction by ID.</param>
        UniTask<AlgoApiResponse<TransactionsResponse>> GetAssetTransactions(
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
        );

        /// <summary>
        /// Lookup block.
        /// </summary>
        /// <param name="round">Round number</param>
        UniTask<AlgoApiResponse<Block>> GetBlock(ulong round);

        /// <summary>
        /// Search for transactions.
        /// </summary>
        /// <param name="address">Only include transactions with this address in one of the transaction fields.</param>
        /// <param name="addressRole">Combine with the address parameter to define what type of address to search for.</param>
        /// <param name="afterTime">Include results after the given time.</param>
        /// <param name="applicationId">Application Id</param>
        /// <param name="assetId">Asset Id</param>
        /// <param name="beforeTime">Include results before the given time.</param>
        /// <param name="currencyGreaterThan">Results should have an amount greater than this value. MicroAlgos are the default currency unless an asset-id is provided, in which case the asset will be used.</param>
        /// <param name="currencyLessThan">Results should have an amount less than this value. MicroAlgos are the default currency unless an asset-id is provided, in which case the asset will be used.</param>
        /// <param name="excludeCloseTo">Combine with address and address-role parameters to define what type of address to search for. The close to fields are normally treated as a receiver, if you would like to exclude them set this parameter to true.</param>
        /// <param name="limit">Maximum number of results to return.</param>
        /// <param name="maxRound">Include results at or before the specified max-round.</param>
        /// <param name="minRound">Include results at or after the specified min-round.</param>
        /// <param name="next">The next page of results. Use the next token provided by the previous results.</param>
        /// <param name="notePrefix">Specifies a prefix which must be contained in the note field.</param>
        /// <param name="rekeyTo">Include results which include the rekey-to field.</param>
        /// <param name="round">Include results for the specified round.</param>
        /// <param name="sigType">SigType filters just results using the specified type of signature.</param>
        /// <param name="txType"></param>
        /// <param name="txid">Lookup the specific transaction by ID.</param>
        UniTask<AlgoApiResponse<TransactionsResponse>> GetTransactions(
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
        );

        /// <summary>
        /// Lookup a single transaction.
        /// </summary>
        /// <param name="txid"></param>
        UniTask<AlgoApiResponse<TransactionResponse>> GetTransaction(TransactionId txid);
    }
}

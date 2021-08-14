using System;
using Cysharp.Threading.Tasks;

namespace AlgoSdk
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IAlgodClient
    {
        #region Synchronous Operations
        /// <summary>
        /// Get account information.
        /// </summary>
        /// <remarks>
        /// Given a specific account public key, this call returns the accounts status, balance and spendable amounts
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <returns>Account</returns>
        Account AccountInformation(string address);

        /// <summary>
        /// Get account information.
        /// </summary>
        /// <remarks>
        /// Given a specific account public key, this call returns the accounts status, balance and spendable amounts
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <returns>ApiResponse of Account</returns>
        Response<Account> AccountInformationWithHttpInfo(string address);
        /// <summary>
        /// Get asset information.
        /// </summary>
        /// <remarks>
        /// Given the asset&#x27;s unique index, this call returns the asset&#x27;s creator, manager, reserve, freeze, and clawback addresses
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="index">Asset index</param>
        /// <returns>AssetParams</returns>
        AssetParams AssetInformation(long? index);

        /// <summary>
        /// Get asset information.
        /// </summary>
        /// <remarks>
        /// Given the asset&#x27;s unique index, this call returns the asset&#x27;s creator, manager, reserve, freeze, and clawback addresses
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="index">Asset index</param>
        /// <returns>ApiResponse of AssetParams</returns>
        Response<AssetParams> AssetInformationWithHttpInfo(long? index);
        /// <summary>
        /// List assets
        /// </summary>
        /// <remarks>
        /// Returns list of up to &#x60;max&#x60; assets, where the maximum assetIdx is &lt;&#x3D; &#x60;assetIdx&#x60;
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="assetIdx">Fetch assets with asset index &lt;&#x3D; assetIdx. If zero, fetch most recent assets. (optional)</param>
        /// <param name="max">Fetch no more than this many assets (optional)</param>
        /// <returns>AssetList</returns>
        AssetList Assets(long? assetIdx = null, long? max = null);

        /// <summary>
        /// List assets
        /// </summary>
        /// <remarks>
        /// Returns list of up to &#x60;max&#x60; assets, where the maximum assetIdx is &lt;&#x3D; &#x60;assetIdx&#x60;
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="assetIdx">Fetch assets with asset index &lt;&#x3D; assetIdx. If zero, fetch most recent assets. (optional)</param>
        /// <param name="max">Fetch no more than this many assets (optional)</param>
        /// <returns>ApiResponse of AssetList</returns>
        Response<AssetList> AssetsWithHttpInfo(long? assetIdx = null, long? max = null);
        /// <summary>
        /// Get the block for the given round.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="round">The round from which to fetch block information.</param>
        /// <returns>Block</returns>
        Block GetBlock(long? round);

        /// <summary>
        /// Get the block for the given round.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="round">The round from which to fetch block information.</param>
        /// <returns>ApiResponse of Block</returns>
        Response<Block> GetBlockWithHttpInfo(long? round);
        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX &#x3D; 0, returns all pending transactions.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="max">Truncated number of transactions to display. If max&#x3D;0, returns all pending txns. (optional)</param>
        /// <returns>PendingTransactions</returns>
        PendingTransactions GetPendingTransactions(long? max = null);

        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX &#x3D; 0, returns all pending transactions.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="max">Truncated number of transactions to display. If max&#x3D;0, returns all pending txns. (optional)</param>
        /// <returns>ApiResponse of PendingTransactions</returns>
        Response<PendingTransactions> GetPendingTransactionsWithHttpInfo(long? max = null);
        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool by address.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions by address, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX &#x3D; 0, returns all pending transactions.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="addr">An account public key</param>
        /// <param name="max">Truncated number of transactions to display. If max&#x3D;0, returns all pending txns. (optional)</param>
        /// <returns>PendingTransactions</returns>
        PendingTransactions GetPendingTransactionsByAddress(string addr, long? max = null);

        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool by address.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions by address, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX &#x3D; 0, returns all pending transactions.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="addr">An account public key</param>
        /// <param name="max">Truncated number of transactions to display. If max&#x3D;0, returns all pending txns. (optional)</param>
        /// <returns>ApiResponse of PendingTransactions</returns>
        Response<PendingTransactions> GetPendingTransactionsByAddressWithHttpInfo(string addr, long? max = null);
        /// <summary>
        /// Gets the current node status.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>NodeStatus</returns>
        NodeStatus GetStatus();

        /// <summary>
        /// Gets the current node status.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of NodeStatus</returns>
        Response<NodeStatus> GetStatusWithHttpInfo();
        /// <summary>
        /// Get the current supply reported by the ledger.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Supply</returns>
        Supply GetSupply();

        /// <summary>
        /// Get the current supply reported by the ledger.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of Supply</returns>
        Response<Supply> GetSupplyWithHttpInfo();
        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// Retrieves the current version
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Version</returns>
        Version GetVersion();

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// Retrieves the current version
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of Version</returns>
        Response<Version> GetVersionWithHttpInfo();
        /// <summary>
        /// Returns OK if healthy.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns></returns>
        void HealthCheck();

        /// <summary>
        /// Returns OK if healthy.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of object(void)</returns>
        Response<object> HealthCheckWithHttpInfo();
        /// <summary>
        /// Return metrics about algod functioning.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns></returns>
        void Metrics();

        /// <summary>
        /// Return metrics about algod functioning.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of object(void)</returns>
        Response<object> MetricsWithHttpInfo();
        /// <summary>
        /// Get a specific pending transaction.
        /// </summary>
        /// <remarks>
        /// Given a transaction id of a recently submitted transaction, it returns information about it.  There are several cases when this might succeed: - transaction committed (committed round &gt; 0) - transaction still in the pool (committed round &#x3D; 0, pool error &#x3D; \&quot;\&quot;) - transaction removed from pool due to error (committed round &#x3D; 0, pool error !&#x3D; \&quot;\&quot;) Or the transaction may have happened sufficiently long ago that the node no longer remembers it, and this will return an error.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="txid">A transaction id</param>
        /// <returns>Transaction</returns>
        ITransaction PendingTransactionInformation(string txid);

        /// <summary>
        /// Get a specific pending transaction.
        /// </summary>
        /// <remarks>
        /// Given a transaction id of a recently submitted transaction, it returns information about it.  There are several cases when this might succeed: - transaction committed (committed round &gt; 0) - transaction still in the pool (committed round &#x3D; 0, pool error &#x3D; \&quot;\&quot;) - transaction removed from pool due to error (committed round &#x3D; 0, pool error !&#x3D; \&quot;\&quot;) Or the transaction may have happened sufficiently long ago that the node no longer remembers it, and this will return an error.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="txid">A transaction id</param>
        /// <returns>ApiResponse of Transaction</returns>
        Response<ITransaction> PendingTransactionInformationWithHttpInfo(string txid);
        /// <summary>
        /// Broadcasts a raw transaction to the network.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">The byte encoded signed transaction to broadcast to network</param>
        /// <returns>ulong</returns>
        ulong ITransaction(byte[] body);

        /// <summary>
        /// Broadcasts a raw transaction to the network.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">The byte encoded signed transaction to broadcast to network</param>
        /// <returns>ApiResponse of ulong</returns>
        Response<ulong> ITransactionWithHttpInfo(byte[] body);
        /// <summary>
        /// Get the suggested fee
        /// </summary>
        /// <remarks>
        /// Suggested Fee is returned in units of micro-Algos per byte. Suggested Fee may fall to zero but submitted transactions must still have a fee of at least MinTxnFee for the current network protocol.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ulong</returns>
        ulong SuggestedFee();

        /// <summary>
        /// Get the suggested fee
        /// </summary>
        /// <remarks>
        /// Suggested Fee is returned in units of micro-Algos per byte. Suggested Fee may fall to zero but submitted transactions must still have a fee of at least MinTxnFee for the current network protocol.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of ulong</returns>
        Response<ulong> SuggestedFeeWithHttpInfo();
        /// <summary>
        /// Gets the current swagger spec.
        /// </summary>
        /// <remarks>
        /// Returns the entire swagger spec in json.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>string</returns>
        string SwaggerJSON();

        /// <summary>
        /// Gets the current swagger spec.
        /// </summary>
        /// <remarks>
        /// Returns the entire swagger spec in json.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of string</returns>
        Response<string> SwaggerJSONWithHttpInfo();
        /// <summary>
        /// Get an information of a single transaction.
        /// </summary>
        /// <remarks>
        /// Returns the transaction information of the given txid. Works only if the indexer is enabled.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="txid">A transaction id</param>
        /// <returns>Transaction</returns>
        ITransaction Transaction(string txid);

        /// <summary>
        /// Get an information of a single transaction.
        /// </summary>
        /// <remarks>
        /// Returns the transaction information of the given txid. Works only if the indexer is enabled.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="txid">A transaction id</param>
        /// <returns>ApiResponse of Transaction</returns>
        Response<ITransaction> TransactionWithHttpInfo(string txid);
        /// <summary>
        /// Get a specific confirmed transaction.
        /// </summary>
        /// <remarks>
        /// Given a wallet address and a transaction id, it returns the confirmed transaction information. This call scans up to &lt;CurrentProtocol&gt;.MaxTxnLife blocks in the past.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <param name="txid">A transaction id</param>
        /// <returns>Transaction</returns>
        ITransaction TransactionInformation(string address, string txid);

        /// <summary>
        /// Get a specific confirmed transaction.
        /// </summary>
        /// <remarks>
        /// Given a wallet address and a transaction id, it returns the confirmed transaction information. This call scans up to &lt;CurrentProtocol&gt;.MaxTxnLife blocks in the past.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <param name="txid">A transaction id</param>
        /// <returns>ApiResponse of Transaction</returns>
        Response<ITransaction> TransactionInformationWithHttpInfo(string address, string txid);
        /// <summary>
        /// Get parameters for constructing a new transaction
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>TransactionParams</returns>
        TransactionParams TransactionParams();

        /// <summary>
        /// Get parameters for constructing a new transaction
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of TransactionParams</returns>
        Response<TransactionParams> TransactionParamsWithHttpInfo();
        /// <summary>
        /// Get a list of confirmed transactions.
        /// </summary>
        /// <remarks>
        /// Returns the list of confirmed transactions between within a date range. This call is available only when the indexer is running.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <param name="firstRound">Do not fetch any transactions before this round. (optional)</param>
        /// <param name="lastRound">Do not fetch any transactions after this round. (optional)</param>
        /// <param name="fromDate">Do not fetch any transactions before this date. (enabled only with indexer) (optional)</param>
        /// <param name="toDate">Do not fetch any transactions after this date. (enabled only with indexer) (optional)</param>
        /// <param name="max">maximum transactions to show (default to 100) (optional)</param>
        /// <returns>TransactionList</returns>
        TransactionList Transactions(string address, long? firstRound = null, long? lastRound = null, DateTime? fromDate = null, DateTime? toDate = null, long? max = null);

        /// <summary>
        /// Get a list of confirmed transactions.
        /// </summary>
        /// <remarks>
        /// Returns the list of confirmed transactions between within a date range. This call is available only when the indexer is running.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <param name="firstRound">Do not fetch any transactions before this round. (optional)</param>
        /// <param name="lastRound">Do not fetch any transactions after this round. (optional)</param>
        /// <param name="fromDate">Do not fetch any transactions before this date. (enabled only with indexer) (optional)</param>
        /// <param name="toDate">Do not fetch any transactions after this date. (enabled only with indexer) (optional)</param>
        /// <param name="max">maximum transactions to show (default to 100) (optional)</param>
        /// <returns>ApiResponse of TransactionList</returns>
        Response<TransactionList> TransactionsWithHttpInfo(string address, long? firstRound = null, long? lastRound = null, DateTime? fromDate = null, DateTime? toDate = null, long? max = null);
        /// <summary>
        /// Gets the node status after waiting for the given round.
        /// </summary>
        /// <remarks>
        /// Waits for a block to appear after round {round} and returns the node&#x27;s status at the time.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="round">The round to wait until returning status</param>
        /// <returns>NodeStatus</returns>
        NodeStatus WaitForBlock(long? round);

        /// <summary>
        /// Gets the node status after waiting for the given round.
        /// </summary>
        /// <remarks>
        /// Waits for a block to appear after round {round} and returns the node&#x27;s status at the time.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="round">The round to wait until returning status</param>
        /// <returns>ApiResponse of NodeStatus</returns>
        Response<NodeStatus> WaitForBlockWithHttpInfo(long? round);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Get account information.
        /// </summary>
        /// <remarks>
        /// Given a specific account public key, this call returns the accounts status, balance and spendable amounts
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <returns>Task of Account</returns>
        UniTask<Account> AccountInformationAsync(string address);

        /// <summary>
        /// Get account information.
        /// </summary>
        /// <remarks>
        /// Given a specific account public key, this call returns the accounts status, balance and spendable amounts
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <returns>Task of ApiResponse (Account)</returns>
        UniTask<Response<Account>> AccountInformationAsyncWithHttpInfo(string address);
        /// <summary>
        /// Get asset information.
        /// </summary>
        /// <remarks>
        /// Given the asset&#x27;s unique index, this call returns the asset&#x27;s creator, manager, reserve, freeze, and clawback addresses
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="index">Asset index</param>
        /// <returns>Task of AssetParams</returns>
        UniTask<AssetParams> AssetInformationAsync(long? index);

        /// <summary>
        /// Get asset information.
        /// </summary>
        /// <remarks>
        /// Given the asset&#x27;s unique index, this call returns the asset&#x27;s creator, manager, reserve, freeze, and clawback addresses
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="index">Asset index</param>
        /// <returns>Task of ApiResponse (AssetParams)</returns>
        UniTask<Response<AssetParams>> AssetInformationAsyncWithHttpInfo(long? index);
        /// <summary>
        /// List assets
        /// </summary>
        /// <remarks>
        /// Returns list of up to &#x60;max&#x60; assets, where the maximum assetIdx is &lt;&#x3D; &#x60;assetIdx&#x60;
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="assetIdx">Fetch assets with asset index &lt;&#x3D; assetIdx. If zero, fetch most recent assets. (optional)</param>
        /// <param name="max">Fetch no more than this many assets (optional)</param>
        /// <returns>Task of AssetList</returns>
        UniTask<AssetList> AssetsAsync(long? assetIdx = null, long? max = null);

        /// <summary>
        /// List assets
        /// </summary>
        /// <remarks>
        /// Returns list of up to &#x60;max&#x60; assets, where the maximum assetIdx is &lt;&#x3D; &#x60;assetIdx&#x60;
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="assetIdx">Fetch assets with asset index &lt;&#x3D; assetIdx. If zero, fetch most recent assets. (optional)</param>
        /// <param name="max">Fetch no more than this many assets (optional)</param>
        /// <returns>Task of ApiResponse (AssetList)</returns>
        UniTask<Response<AssetList>> AssetsAsyncWithHttpInfo(long? assetIdx = null, long? max = null);
        /// <summary>
        /// Get the block for the given round.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="round">The round from which to fetch block information.</param>
        /// <returns>Task of Block</returns>
        UniTask<Block> GetBlockAsync(long? round);

        /// <summary>
        /// Get the block for the given round.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="round">The round from which to fetch block information.</param>
        /// <returns>Task of ApiResponse (Block)</returns>
        UniTask<Response<Block>> GetBlockAsyncWithHttpInfo(long? round);
        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX &#x3D; 0, returns all pending transactions.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="max">Truncated number of transactions to display. If max&#x3D;0, returns all pending txns. (optional)</param>
        /// <returns>Task of PendingTransactions</returns>
        UniTask<PendingTransactions> GetPendingTransactionsAsync(long? max = null);

        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX &#x3D; 0, returns all pending transactions.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="max">Truncated number of transactions to display. If max&#x3D;0, returns all pending txns. (optional)</param>
        /// <returns>Task of ApiResponse (PendingTransactions)</returns>
        UniTask<Response<PendingTransactions>> GetPendingTransactionsAsyncWithHttpInfo(long? max = null);
        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool by address.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions by address, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX &#x3D; 0, returns all pending transactions.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="addr">An account public key</param>
        /// <param name="max">Truncated number of transactions to display. If max&#x3D;0, returns all pending txns. (optional)</param>
        /// <returns>Task of PendingTransactions</returns>
        UniTask<PendingTransactions> GetPendingTransactionsByAddressAsync(string addr, long? max = null);

        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool by address.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions by address, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX &#x3D; 0, returns all pending transactions.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="addr">An account public key</param>
        /// <param name="max">Truncated number of transactions to display. If max&#x3D;0, returns all pending txns. (optional)</param>
        /// <returns>Task of ApiResponse (PendingTransactions)</returns>
        UniTask<Response<PendingTransactions>> GetPendingTransactionsByAddressAsyncWithHttpInfo(string addr, long? max = null);
        /// <summary>
        /// Gets the current node status.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of NodeStatus</returns>
        UniTask<NodeStatus> GetStatusAsync();

        /// <summary>
        /// Gets the current node status.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (NodeStatus)</returns>
        UniTask<Response<NodeStatus>> GetStatusAsyncWithHttpInfo();
        /// <summary>
        /// Get the current supply reported by the ledger.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of Supply</returns>
        UniTask<Supply> GetSupplyAsync();

        /// <summary>
        /// Get the current supply reported by the ledger.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (Supply)</returns>
        UniTask<Response<Supply>> GetSupplyAsyncWithHttpInfo();
        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// Retrieves the current version
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of Version</returns>
        UniTask<Version> GetVersionAsync();

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// Retrieves the current version
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (Version)</returns>
        UniTask<Response<Version>> GetVersionAsyncWithHttpInfo();
        /// <summary>
        /// Returns OK if healthy.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of void</returns>
        UniTask HealthCheckAsync();

        /// <summary>
        /// Returns OK if healthy.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of Response</returns>
        UniTask<Response<object>> HealthCheckAsyncWithHttpInfo();
        /// <summary>
        /// Return metrics about algod functioning.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of void</returns>
        UniTask MetricsAsync();

        /// <summary>
        /// Return metrics about algod functioning.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of Response</returns>
        UniTask<Response<object>> MetricsAsyncWithHttpInfo();
        /// <summary>
        /// Get a specific pending transaction.
        /// </summary>
        /// <remarks>
        /// Given a transaction id of a recently submitted transaction, it returns information about it.  There are several cases when this might succeed: - transaction committed (committed round &gt; 0) - transaction still in the pool (committed round &#x3D; 0, pool error &#x3D; \&quot;\&quot;) - transaction removed from pool due to error (committed round &#x3D; 0, pool error !&#x3D; \&quot;\&quot;) Or the transaction may have happened sufficiently long ago that the node no longer remembers it, and this will return an error.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="txid">A transaction id</param>
        /// <returns>Task of Transaction</returns>
        UniTask<ITransaction> PendingTransactionInformationAsync(string txid);

        /// <summary>
        /// Get a specific pending transaction.
        /// </summary>
        /// <remarks>
        /// Given a transaction id of a recently submitted transaction, it returns information about it.  There are several cases when this might succeed: - transaction committed (committed round &gt; 0) - transaction still in the pool (committed round &#x3D; 0, pool error &#x3D; \&quot;\&quot;) - transaction removed from pool due to error (committed round &#x3D; 0, pool error !&#x3D; \&quot;\&quot;) Or the transaction may have happened sufficiently long ago that the node no longer remembers it, and this will return an error.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="txid">A transaction id</param>
        /// <returns>Task of ApiResponse (Transaction)</returns>
        UniTask<Response<ITransaction>> PendingTransactionInformationAsyncWithHttpInfo(string txid);
        /// <summary>
        /// Broadcasts a raw transaction to the network.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">The byte encoded signed transaction to broadcast to network</param>
        /// <returns>Task of ulong</returns>
        UniTask<ulong> ITransactionAsync(string body);

        /// <summary>
        /// Broadcasts a raw transaction to the network.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">The byte encoded signed transaction to broadcast to network</param>
        /// <returns>Task of ApiResponse (ulong)</returns>
        UniTask<Response<ulong>> ITransactionAsyncWithHttpInfo(string body);
        /// <summary>
        /// Get the suggested fee
        /// </summary>
        /// <remarks>
        /// Suggested Fee is returned in units of micro-Algos per byte. Suggested Fee may fall to zero but submitted transactions must still have a fee of at least MinTxnFee for the current network protocol.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ulong</returns>
        UniTask<ulong> SuggestedFeeAsync();

        /// <summary>
        /// Get the suggested fee
        /// </summary>
        /// <remarks>
        /// Suggested Fee is returned in units of micro-Algos per byte. Suggested Fee may fall to zero but submitted transactions must still have a fee of at least MinTxnFee for the current network protocol.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (ulong)</returns>
        UniTask<Response<ulong>> SuggestedFeeAsyncWithHttpInfo();
        /// <summary>
        /// Gets the current swagger spec.
        /// </summary>
        /// <remarks>
        /// Returns the entire swagger spec in json.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of string</returns>
        UniTask<string> SwaggerJSONAsync();

        /// <summary>
        /// Gets the current swagger spec.
        /// </summary>
        /// <remarks>
        /// Returns the entire swagger spec in json.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (string)</returns>
        UniTask<Response<string>> SwaggerJSONAsyncWithHttpInfo();
        /// <summary>
        /// Get an information of a single transaction.
        /// </summary>
        /// <remarks>
        /// Returns the transaction information of the given txid. Works only if the indexer is enabled.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="txid">A transaction id</param>
        /// <returns>Task of Transaction</returns>
        UniTask<ITransaction> TransactionAsync(string txid);

        /// <summary>
        /// Get an information of a single transaction.
        /// </summary>
        /// <remarks>
        /// Returns the transaction information of the given txid. Works only if the indexer is enabled.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="txid">A transaction id</param>
        /// <returns>Task of ApiResponse (Transaction)</returns>
        UniTask<Response<ITransaction>> TransactionAsyncWithHttpInfo(string txid);
        /// <summary>
        /// Get a specific confirmed transaction.
        /// </summary>
        /// <remarks>
        /// Given a wallet address and a transaction id, it returns the confirmed transaction information. This call scans up to &lt;CurrentProtocol&gt;.MaxTxnLife blocks in the past.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <param name="txid">A transaction id</param>
        /// <returns>Task of Transaction</returns>
        UniTask<ITransaction> TransactionInformationAsync(string address, string txid);

        /// <summary>
        /// Get a specific confirmed transaction.
        /// </summary>
        /// <remarks>
        /// Given a wallet address and a transaction id, it returns the confirmed transaction information. This call scans up to &lt;CurrentProtocol&gt;.MaxTxnLife blocks in the past.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <param name="txid">A transaction id</param>
        /// <returns>Task of ApiResponse (Transaction)</returns>
        UniTask<Response<ITransaction>> TransactionInformationAsyncWithHttpInfo(string address, string txid);
        /// <summary>
        /// Get parameters for constructing a new transaction
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of TransactionParams</returns>
        UniTask<TransactionParams> TransactionParamsAsync();

        /// <summary>
        /// Get parameters for constructing a new transaction
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (TransactionParams)</returns>
        UniTask<Response<TransactionParams>> TransactionParamsAsyncWithHttpInfo();
        /// <summary>
        /// Get a list of confirmed transactions.
        /// </summary>
        /// <remarks>
        /// Returns the list of confirmed transactions between within a date range. This call is available only when the indexer is running.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <param name="firstRound">Do not fetch any transactions before this round. (optional)</param>
        /// <param name="lastRound">Do not fetch any transactions after this round. (optional)</param>
        /// <param name="fromDate">Do not fetch any transactions before this date. (enabled only with indexer) (optional)</param>
        /// <param name="toDate">Do not fetch any transactions after this date. (enabled only with indexer) (optional)</param>
        /// <param name="max">maximum transactions to show (default to 100) (optional)</param>
        /// <returns>Task of TransactionList</returns>
        UniTask<TransactionList> TransactionsAsync(string address, long? firstRound = null, long? lastRound = null, DateTime? fromDate = null, DateTime? toDate = null, long? max = null);

        /// <summary>
        /// Get a list of confirmed transactions.
        /// </summary>
        /// <remarks>
        /// Returns the list of confirmed transactions between within a date range. This call is available only when the indexer is running.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="address">An account public key</param>
        /// <param name="firstRound">Do not fetch any transactions before this round. (optional)</param>
        /// <param name="lastRound">Do not fetch any transactions after this round. (optional)</param>
        /// <param name="fromDate">Do not fetch any transactions before this date. (enabled only with indexer) (optional)</param>
        /// <param name="toDate">Do not fetch any transactions after this date. (enabled only with indexer) (optional)</param>
        /// <param name="max">maximum transactions to show (default to 100) (optional)</param>
        /// <returns>Task of ApiResponse (TransactionList)</returns>
        UniTask<Response<TransactionList>> TransactionsAsyncWithHttpInfo(string address, long? firstRound = null, long? lastRound = null, DateTime? fromDate = null, DateTime? toDate = null, long? max = null);
        /// <summary>
        /// Gets the node status after waiting for the given round.
        /// </summary>
        /// <remarks>
        /// Waits for a block to appear after round {round} and returns the node&#x27;s status at the time.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="round">The round to wait until returning status</param>
        /// <returns>Task of NodeStatus</returns>
        UniTask<NodeStatus> WaitForBlockAsync(long? round);

        /// <summary>
        /// Gets the node status after waiting for the given round.
        /// </summary>
        /// <remarks>
        /// Waits for a block to appear after round {round} and returns the node&#x27;s status at the time.
        /// </remarks>
        /// <exception cref="Algorand.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="round">The round to wait until returning status</param>
        /// <returns>Task of ApiResponse (NodeStatus)</returns>
        UniTask<Response<NodeStatus>> WaitForBlockAsyncWithHttpInfo(long? round);
        #endregion Asynchronous Operations
    }
}

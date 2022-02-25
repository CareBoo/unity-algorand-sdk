using System;
using Cysharp.Threading.Tasks;
using static UnityEngine.Networking.UnityWebRequest;

namespace AlgoSdk
{
    public interface IAlgodClient : IAlgoApiClient
    {
        /// <summary>
        /// Get information about the genesis block.
        /// </summary>
        /// <returns>The entire genesis file in JSON</returns>
        AlgoApiRequest.Sent<AlgoApiObject> GetGenesisInformation();

        /// <summary>
        /// Check the health of the algod service.
        /// </summary>
        /// <returns><see cref="Result.Success"/> if healthy</returns>
        AlgoApiRequest.Sent GetHealth();

        /// <summary>
        /// Return metrics about algod functioning.
        /// </summary>
        /// <returns>text with #-comments and key:value lines.</returns>
        AlgoApiRequest.Sent GetMetrics();

        /// <summary>
        /// Gets the current swagger spec.
        /// </summary>
        /// <returns>The entire swagger spec in JSON.</returns>
        AlgoApiRequest.Sent<AlgoApiObject> GetSwaggerSpec();

        /// <summary>
        /// Get account information.
        /// </summary>
        /// <remarks>
        /// Given a specific account public key, this call returns the accounts status, balance and spendable amounts.
        /// </remarks>
        /// <param name="accountAddress">An account public key (address)</param>
        /// <returns>an <see cref="AccountInfo"/> if everything is okay</returns>
        AlgoApiRequest.Sent<AccountInfo> GetAccountInformation(Address accountAddress);

        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool by address.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions by address, sorted by priority, in decreasing order, truncated at the end at MAX.
        /// If MAX = 0, returns all pending transactions.
        /// </remarks>
        /// <param name="accountAddress">An account public key (address)</param>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <returns>
        /// A potentially truncated list of transactions currently in the node's transaction pool.
        /// You can compute whether or not the list is truncated if the number of elements in the top-transactions
        /// array is fewer than total-transactions.
        /// </returns>
        AlgoApiRequest.Sent<PendingTransactions> GetPendingTransactionsByAccount(Address accountAddress, ulong max = 0);

        /// <summary>
        /// Get application information.
        /// </summary>
        /// <remarks>
        /// Given a application id, it returns application information including creator, approval and clear programs,
        /// global and local schemas, and global state.
        /// </remarks>
        /// <param name="applicationId">An application identifier (app index)</param>
        /// <returns><see cref="Application"/> information</returns>
        AlgoApiRequest.Sent<Application> GetApplication(ulong applicationId);

        /// <summary>
        /// Get asset information.
        /// </summary>
        /// <remarks>
        /// Given an asset id, it returns asset information including creator, name, total supply and special addresses.
        /// </remarks>
        /// <param name="assetId">An asset identifier (asset index)</param>
        /// <returns><see cref="Asset"/> information</returns>
        AlgoApiRequest.Sent<Asset> GetAsset(ulong assetId);

        /// <summary>
        /// Get the block for the given round.
        /// </summary>
        /// <param name="round">The round from which to fetch block information.</param>
        /// <returns>Encoded block object <see cref="BlockResponse"/></returns>
        AlgoApiRequest.Sent<BlockResponse> GetBlock(ulong round);

        /// <summary>
        /// Get a Merkle proof for a transaction in a block.
        /// </summary>
        /// <param name="round">The round in which the transaction appears.</param>
        /// <param name="txid">The transaction ID for which to generate a proof.</param>
        /// <returns>Proof of transaction in a block <see cref="MerkleProof"/>.</returns>
        AlgoApiRequest.Sent<MerkleProof> GetMerkleProof(ulong round, TransactionId txid);

        /// <summary>
        /// Starts a catchpoint catchup.
        /// </summary>
        /// <param name="catchpoint">A catch point</param>
        /// <returns>Catchup start response string</returns>
        AlgoApiRequest.Sent<CatchupMessage> StartCatchup(string catchpoint);

        /// <summary>
        /// Aborts a catchpoint catchup.
        /// </summary>
        /// <param name="catchpoint">A catch point</param>
        /// <returns>Catchup abort response string</returns>
        AlgoApiRequest.Sent<CatchupMessage> AbortCatchup(string catchpoint);

        /// <summary>
        /// Get the current supply reported by the ledger.
        /// </summary>
        /// <returns>Supply represents the current supply of MicroAlgos in the system.</returns>
        AlgoApiRequest.Sent<LedgerSupply> GetLedgerSupply();

        /// <summary>
        /// Generate (or renew) and register participation keys on the node for a given account address.
        /// </summary>
        /// <param name="accountAddress">The account-id to update, or "all" to update all accounts.</param>
        /// <param name="fee">The fee to use when submitting key registration transactions. Defaults to the suggested fee.</param>
        /// <param name="keyDilution">value to use for two-level participation key.</param>
        /// <param name="noWait">Don't wait for transaction to commit before returning response.</param>
        /// <param name="roundLastValid">The last round for which the generated participation keys will be valid.</param>
        /// <returns>Transaction ID of the submission.</returns>
        AlgoApiRequest.Sent<TransactionIdResponse> RegisterParticipationKeys(
            string accountAddress,
            ulong fee = 1000,
            Optional<ulong> keyDilution = default,
            Optional<bool> noWait = default,
            Optional<bool> roundLastValid = default);

        /// <summary>
        /// Special management endpoint to shutdown the node.
        /// </summary>
        /// <remarks>
        /// Optionally provide a timeout parameter to indicate that the node should begin shutting down after a number of seconds.
        /// </remarks>
        /// <param name="timeout">shutdown timeout</param>
        /// <returns>Success if request was successful</returns>
        AlgoApiRequest.Sent ShutDown(Optional<ulong> timeout = default);

        /// <summary>
        /// Gets the current node status.
        /// </summary>
        /// <returns><see cref="Status"/></returns>
        AlgoApiRequest.Sent<Status> GetCurrentStatus();

        /// <summary>
        /// Gets the node status after waiting for the given round.
        /// </summary>
        /// <remarks>
        /// Waits for a block to appear after round {round} and returns the node's status at the time.
        /// </remarks>
        /// <param name="round">The round to wait until returning status</param>
        /// <returns><see cref="Status"/></returns>
        AlgoApiRequest.Sent<Status> GetStatusAfterWaitingForRound(ulong round);

        /// <summary>
        /// Compile TEAL source code to binary, produce its hash
        /// </summary>
        /// <remarks>
        /// Given TEAL source code in plain text, return base64 encoded program bytes and base32 SHA512_256 hash of program bytes (Address style).
        /// This endpoint is only enabled when a node's configureation file sets EnableDeveloperAPI to true.
        /// </remarks>
        /// <param name="source">TEAL source code to be compiled</param>
        /// <returns>Teal compile Result</returns>
        AlgoApiRequest.Sent<TealCompilationResult> TealCompile(string source);

        /// <summary>
        /// Provide debugging information for a transaction (or group).
        /// </summary>
        /// <remarks>
        /// Executes TEAL program(s) in context and returns debugging information about the execution.
        /// This endpoint is only enabled when a node's configureation file sets EnableDeveloperAPI to true.
        /// </remarks>
        /// <param name="request">Transaction (or group) and any accompanying state-simulation data.</param>
        /// <returns>DryrunResponse contains per-txn debug information from a dryrun.</returns>
        AlgoApiRequest.Sent<DryrunResults> TealDryrun(Optional<DryrunRequest> request = default);

        /// <summary>
        /// Broadcasts a raw transaction to the network.
        /// </summary>
        /// <typeparam name="T">The type of the transaction; must implement <see cref="ITransaction"/></typeparam>
        /// <param name="txn">The byte encoded signed transaction to broadcast to network</param>
        /// <returns>Transaction ID of the submission.</returns>
        AlgoApiRequest.Sent<TransactionIdResponse> SendTransaction<T>(Signed<T> txn)
            where T : struct, ITransaction, IEquatable<T>;

        /// <summary>
        /// Broadcasts a group of transactions to the network.
        /// </summary>
        /// <param name="signedTxns">The signed transactions in the same order as they were when using <see cref="Transaction.GetGroupId(TransactionId[])"/></param>
        /// <returns>Transaction ID of the submission.</returns>
        AlgoApiRequest.Sent<TransactionIdResponse> SendTransactions(params SignedTransaction[] signedTxns);

        /// <summary>
        /// Get parameters for constructing a new transaction
        /// </summary>
        /// <returns><see cref="TransactionParams"/> contains the parameters that help a client construct a new transaction.</returns>
        AlgoApiRequest.Sent<TransactionParams> GetSuggestedParams();

        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions, sorted by priority, in decreasing order, truncated at the end at MAX.
        /// If MAX = 0, returns all pending transactions.
        /// </remarks>
        /// <param name="max">Truncated number of transactions to display. If max=0, returns all pending txns.</param>
        /// <returns>
        /// A potentially truncated list of transactions currently in the node's transaction pool.
        /// You can compute whether or not the list is truncated if the number of elements in the top-transactions array is fewer than total-transactions.
        /// </returns>
        AlgoApiRequest.Sent<PendingTransactions> GetPendingTransactions(ulong max = 0);

        /// <summary>
        /// Get a specific pending transaction.
        /// </summary>
        /// <param name="txId">A transaction id</param>
        /// <returns>
        /// Given a transaction id of a recently submitted transaction, it returns information about it. There are several cases when this might succeed:
        /// - transaction committed (committed round > 0)
        /// - transaction still in the pool (committed round = 0, pool error = "")
        /// - transaction removed from pool due to error (committed round = 0, pool error != "")
        /// Or the transaction may have happened sufficiently long ago that the node no longer remembers it, and this will return an error.
        /// </returns>
        AlgoApiRequest.Sent<PendingTransaction> GetPendingTransaction(TransactionId txId);

        /// <summary>
        /// Retrieves the supported API versions, binary build versions, and genesis information.
        /// </summary>
        /// <returns><see cref="Version"/> is the response to 'GET /versions'</returns>
        AlgoApiRequest.Sent<Version> GetVersions();
    }
}

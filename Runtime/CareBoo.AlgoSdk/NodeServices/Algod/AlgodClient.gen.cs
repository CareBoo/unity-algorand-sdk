//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using AlgoSdk.Algod;
using AlgoSdk.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    public interface IAlgodClient : IAlgoApiClient
    {
        /// <summary>
        /// Get a proof for a transaction in a block.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="round">
        /// The round in which the transaction appears.
        /// </param>
        /// <param name="txid">
        /// The transaction ID for which to generate a proof.
        /// </param>
        /// <param name="hashtype">
        /// The type of hash function used to create the proof, must be one of: 
        /// * sha512_256 
        /// * sha256
        /// </param>
        /// <param name="format">
        /// Configures whether the response object is JSON or MessagePack encoded.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<TransactionProofResponse> GetTransactionProof(
            ulong round,
        
            string txid,
        
            string hashtype = default,
        
            ResponseFormat format = default
        );

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Special management endpoint to shutdown the node. Optionally provide a timeout parameter to indicate that the node should begin shutting down after a number of seconds.
        /// </remarks>
        /// <param name="timeout">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<AlgoApiObject> ShutdownNode(
            Optional<ulong> timeout = default
        );

        /// <summary>
        /// Starts a catchpoint catchup.
        /// </summary>
        /// <remarks>
        /// Given a catchpoint, it starts catching up to this catchpoint
        /// </remarks>
        /// <param name="catchpoint">
        /// A catch point
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<CatchpointStartResponse> StartCatchup(
            string catchpoint
        );

        /// <summary>
        /// Aborts a catchpoint catchup.
        /// </summary>
        /// <remarks>
        /// Given a catchpoint, it aborts catching up to this catchpoint
        /// </remarks>
        /// <param name="catchpoint">
        /// A catch point
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<CatchpointAbortResponse> AbortCatchup(
            string catchpoint
        );

        /// <summary>
        /// Get the block for the given round.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="round">
        /// The round from which to fetch block information.
        /// </param>
        /// <param name="format">
        /// Configures whether the response object is JSON or MessagePack encoded.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<BlockResponse> GetBlock(
            ulong round,
        
            ResponseFormat format = default
        );

        /// <summary>
        /// Get parameters for constructing a new transaction
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<TransactionParametersResponse> TransactionParams(
             
        );

        /// <summary>
        /// Gets a proof for a given light block header inside a state proof commitment
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="round">
        /// The round to which the light block header belongs.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<LightBlockHeaderProofResponse> GetLightBlockHeaderProof(
            ulong round
        );

        /// <summary>
        /// Provide debugging information for a transaction (or group).
        /// </summary>
        /// <remarks>
        /// Executes TEAL program(s) in context and returns debugging information about the execution. This endpoint is only enabled when a node's configuration file sets EnableDeveloperAPI to true.
        /// </remarks>
        /// <param name="request">
        /// Transaction (or group) and any accompanying state-simulation data.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<DryrunResponse> TealDryrun(
            DryrunRequest request = default
        );

        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool by address.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions by address, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX = 0, returns all pending transactions.
        /// </remarks>
        /// <param name="address">
        /// An account public key
        /// </param>
        /// <param name="max">
        /// Truncated number of transactions to display. If max=0, returns all pending txns.
        /// </param>
        /// <param name="format">
        /// Configures whether the response object is JSON or MessagePack encoded.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<PendingTransactionsResponse> GetPendingTransactionsByAddress(
            string address,
        
            Optional<ulong> max = default,
        
            ResponseFormat format = default
        );

        /// <summary>
        /// Get a state proof that covers a given round
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="round">
        /// The round for which a state proof is desired.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<StateProofResponse> GetStateProof(
            ulong round
        );

        /// <summary>
        /// Broadcasts a raw transaction to the network.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="rawtxn">
        /// The byte encoded signed transaction to broadcast to network
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<PostTransactionsResponse> RawTransaction(
            byte[] rawtxn
        );

        /// <summary>
        /// Get asset information.
        /// </summary>
        /// <remarks>
        /// Given a asset ID, it returns asset information including creator, name, total supply and special addresses.
        /// </remarks>
        /// <param name="assetId">
        /// An asset identifier
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<AssetResponse> GetAssetByID(
            ulong assetId
        );

        /// <summary>
        /// Get a list of unconfirmed transactions currently in the transaction pool.
        /// </summary>
        /// <remarks>
        /// Get the list of pending transactions, sorted by priority, in decreasing order, truncated at the end at MAX. If MAX = 0, returns all pending transactions.
        /// </remarks>
        /// <param name="max">
        /// Truncated number of transactions to display. If max=0, returns all pending txns.
        /// </param>
        /// <param name="format">
        /// Configures whether the response object is JSON or MessagePack encoded.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<PendingTransactionsResponse> GetPendingTransactions(
            Optional<ulong> max = default,
        
            ResponseFormat format = default
        );

        /// <summary>
        /// Get the current supply reported by the ledger.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<SupplyResponse> GetSupply(
             
        );

        /// <summary>
        /// Get application information.
        /// </summary>
        /// <remarks>
        /// Given a application ID, it returns application information including creator, approval and clear programs, global and local schemas, and global state.
        /// </remarks>
        /// <param name="applicationId">
        /// An application identifier
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<ApplicationResponse> GetApplicationByID(
            ulong applicationId
        );

        /// <summary>
        /// Gets the node status after waiting for the given round.
        /// </summary>
        /// <remarks>
        /// Waits for a block to appear after round {round} and returns the node's status at the time.
        /// </remarks>
        /// <param name="round">
        /// The round to wait until returning status
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<NodeStatusResponse> WaitForBlock(
            ulong round
        );

        /// <summary>
        /// Gets the current node status.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<NodeStatusResponse> GetStatus(
             
        );

        /// <summary>
        /// Gets the current swagger spec.
        /// </summary>
        /// <remarks>
        /// Returns the entire swagger spec in json.
        /// </remarks>
        /// <param name="">
        /// Returns the entire swagger spec in json.
        /// </param>
        /// <returns>
        /// The current swagger spec
        /// </returns>
        AlgoApiRequest.Sent<AlgoApiObject> SwaggerJSON(
             
        );

        /// <summary>
        /// Get the block hash for the block on the given round.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="round">
        /// The round from which to fetch block hash information.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<BlockHashResponse> GetBlockHash(
            ulong round
        );

        /// <summary>
        /// Delete a given participation key by ID
        /// </summary>
        /// <remarks>
        /// Delete a given participation key by ID
        /// </remarks>
        /// <param name="participationId">
        /// 
        /// </param>
        AlgoApiRequest.Sent DeleteParticipationKeyByID(
            string participationId
        );

        /// <summary>
        /// Get participation key info given a participation ID
        /// </summary>
        /// <remarks>
        /// Given a participation ID, return information about that participation key
        /// </remarks>
        /// <param name="participationId">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<ParticipationKeyResponse> GetParticipationKeyByID(
            string participationId
        );

        /// <summary>
        /// Append state proof keys to a participation key
        /// </summary>
        /// <remarks>
        /// Given a participation ID, append state proof keys to a particular set of participation keys
        /// </remarks>
        /// <param name="keymap">
        /// The state proof keys to add to an existing participation ID
        /// </param>
        /// <param name="participationId">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<ParticipationKeyResponse> AppendKeys(
            byte[] keymap,
        
            string participationId
        );

        /// <summary>
        /// Return a list of participation keys
        /// </summary>
        /// <remarks>
        /// Return a list of participation keys
        /// </remarks>
        /// <param name="">
        /// Return a list of participation keys
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<ParticipationKeysResponse> GetParticipationKeys(
             
        );

        /// <summary>
        /// Add a participation key to the node
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="participationkey">
        /// The participation key to add to the node
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<PostParticipationResponse> AddParticipationKey(
            byte[] participationkey
        );

        /// <summary>
        /// Compile TEAL source code to binary, produce its hash
        /// </summary>
        /// <remarks>
        /// Given TEAL source code in plain text, return base64 encoded program bytes and base32 SHA512_256 hash of program bytes (Address style). This endpoint is only enabled when a node's configuration file sets EnableDeveloperAPI to true.
        /// </remarks>
        /// <param name="source">
        /// TEAL source code to be compiled
        /// </param>
        /// <param name="sourcemap">
        /// When set to `true`, returns the source map of the program as a JSON. Defaults to `false`.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<CompileResponse> TealCompile(
            byte[] source,
        
            Optional<bool> sourcemap = default
        );

        /// <summary>
        /// Get account information.
        /// </summary>
        /// <remarks>
        /// Given a specific account public key, this call returns the accounts status, balance and spendable amounts
        /// </remarks>
        /// <param name="address">
        /// An account public key
        /// </param>
        /// <param name="exclude">
        /// When set to `all` will exclude asset holdings, application local state, created asset parameters, any created application parameters. Defaults to `none`.
        /// </param>
        /// <param name="format">
        /// Configures whether the response object is JSON or MessagePack encoded.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<AccountResponse> AccountInformation(
            string address,
        
            ExcludeFields exclude = default,
        
            ResponseFormat format = default
        );

        /// <summary>
        /// Returns OK if healthy.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="">
        /// 
        /// </param>
        AlgoApiRequest.Sent HealthCheck(
             
        );

        /// <summary>
        /// Get a specific pending transaction.
        /// </summary>
        /// <remarks>
        /// Given a transaction ID of a recently submitted transaction, it returns information about it.  There are several cases when this might succeed:
        /// - transaction committed (committed round > 0)
        /// - transaction still in the pool (committed round = 0, pool error = "")
        /// - transaction removed from pool due to error (committed round = 0, pool error != "")
        /// Or the transaction may have happened sufficiently long ago that the node no longer remembers it, and this will return an error.
        /// </remarks>
        /// <param name="txid">
        /// A transaction ID
        /// </param>
        /// <param name="format">
        /// Configures whether the response object is JSON or MessagePack encoded.
        /// </param>
        /// <returns>
        /// Given a transaction ID of a recently submitted transaction, it returns information about it.  There are several cases when this might succeed:
        /// - transaction committed (committed round > 0)
        /// - transaction still in the pool (committed round = 0, pool error = "")
        /// - transaction removed from pool due to error (committed round = 0, pool error != "")
        /// 
        /// Or the transaction may have happened sufficiently long ago that the node no longer remembers it, and this will return an error.
        /// </returns>
        AlgoApiRequest.Sent<PendingTransactionResponse> PendingTransactionInformation(
            string txid,
        
            ResponseFormat format = default
        );

        /// <summary>
        /// Get account information about a given app.
        /// </summary>
        /// <remarks>
        /// Given a specific account public key and application ID, this call returns the account's application local state and global state (AppLocalState and AppParams, if either exists). Global state will only be returned if the provided address is the application's creator.
        /// </remarks>
        /// <param name="address">
        /// An account public key
        /// </param>
        /// <param name="applicationId">
        /// An application identifier
        /// </param>
        /// <param name="format">
        /// Configures whether the response object is JSON or MessagePack encoded.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<AccountApplicationResponse> AccountApplicationInformation(
            string address,
        
            ulong applicationId,
        
            ResponseFormat format = default
        );

        /// <summary>
        /// Gets the genesis information.
        /// </summary>
        /// <remarks>
        /// Returns the entire genesis file in json.
        /// </remarks>
        /// <param name="">
        /// Returns the entire genesis file in json.
        /// </param>
        /// <returns>
        /// The genesis file in json.
        /// </returns>
        AlgoApiRequest.Sent<AlgoApiObject> GetGenesis(
             
        );

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Retrieves the supported API versions, binary build versions, and genesis information.
        /// </remarks>
        /// <param name="">
        /// Retrieves the supported API versions, binary build versions, and genesis information.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<VersionsResponse> GetVersion(
             
        );

        /// <summary>
        /// Disassemble program bytes into the TEAL source code.
        /// </summary>
        /// <remarks>
        /// Given the program bytes, return the TEAL source code in plain text. This endpoint is only enabled when a node's configuration file sets EnableDeveloperAPI to true.
        /// </remarks>
        /// <param name="source">
        /// TEAL program binary to be disassembled
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<DisassembleResponse> TealDisassemble(
            byte[] source
        );

        /// <summary>
        /// Return metrics about algod functioning.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="">
        /// 
        /// </param>
        AlgoApiRequest.Sent Metrics(
             
        );

        /// <summary>
        /// Get account information about a given asset.
        /// </summary>
        /// <remarks>
        /// Given a specific account public key and asset ID, this call returns the account's asset holding and asset parameters (if either exist). Asset parameters will only be returned if the provided address is the asset's creator.
        /// </remarks>
        /// <param name="address">
        /// An account public key
        /// </param>
        /// <param name="assetId">
        /// An asset identifier
        /// </param>
        /// <param name="format">
        /// Configures whether the response object is JSON or MessagePack encoded.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        AlgoApiRequest.Sent<AccountAssetResponse> AccountAssetInformation(
            string address,
        
            ulong assetId,
        
            ResponseFormat format = default
        );

    }

    public partial struct AlgodClient
        : IAlgodClient
    {
        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionProofResponse> GetTransactionProof(
            ulong round,
        
            string txid,
        
            string hashtype = default,
        
            ResponseFormat format = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("hashtype", hashtype)
                .Add("format", format)
                ;
            var path = $"/v2/blocks/{round}/transactions/{txid}/proof{queryBuilder}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AlgoApiObject> ShutdownNode(
            Optional<ulong> timeout = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("timeout", timeout)
                ;
            var path = $"/v2/shutdown{queryBuilder}";
            return this
                .Post(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<CatchpointStartResponse> StartCatchup(
            string catchpoint
        )
        {
            var path = $"/v2/catchup/{catchpoint}";
            return this
                .Post(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<CatchpointAbortResponse> AbortCatchup(
            string catchpoint
        )
        {
            var path = $"/v2/catchup/{catchpoint}";
            return this
                .Delete(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<BlockResponse> GetBlock(
            ulong round,
        
            ResponseFormat format = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("format", format)
                ;
            var path = $"/v2/blocks/{round}{queryBuilder}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<TransactionParametersResponse> TransactionParams(
             
        )
        {
            var path = $"/v2/transactions/params";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<LightBlockHeaderProofResponse> GetLightBlockHeaderProof(
            ulong round
        )
        {
            var path = $"/v2/blocks/{round}/lightheader/proof";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<DryrunResponse> TealDryrun(
            DryrunRequest request = default
        )
        {
            var path = $"/v2/teal/dryrun";
            return this
                .Post(path)
                .SetMessagePackBody(request)
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<PendingTransactionsResponse> GetPendingTransactionsByAddress(
            string address,
        
            Optional<ulong> max = default,
        
            ResponseFormat format = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("max", max)
                .Add("format", format)
                ;
            var path = $"/v2/accounts/{address}/transactions/pending{queryBuilder}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<StateProofResponse> GetStateProof(
            ulong round
        )
        {
            var path = $"/v2/stateproofs/{round}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<PostTransactionsResponse> RawTransaction(
            byte[] rawtxn
        )
        {
            var path = $"/v2/transactions";
            return this
                .Post(path)
                .SetMessagePackBody(rawtxn)
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AssetResponse> GetAssetByID(
            ulong assetId
        )
        {
            var path = $"/v2/assets/{assetId}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<PendingTransactionsResponse> GetPendingTransactions(
            Optional<ulong> max = default,
        
            ResponseFormat format = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("max", max)
                .Add("format", format)
                ;
            var path = $"/v2/transactions/pending{queryBuilder}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<SupplyResponse> GetSupply(
             
        )
        {
            var path = $"/v2/ledger/supply";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ApplicationResponse> GetApplicationByID(
            ulong applicationId
        )
        {
            var path = $"/v2/applications/{applicationId}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<NodeStatusResponse> WaitForBlock(
            ulong round
        )
        {
            var path = $"/v2/status/wait-for-block-after/{round}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<NodeStatusResponse> GetStatus(
             
        )
        {
            var path = $"/v2/status";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AlgoApiObject> SwaggerJSON(
             
        )
        {
            var path = $"/swagger.json";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<BlockHashResponse> GetBlockHash(
            ulong round
        )
        {
            var path = $"/v2/blocks/{round}/hash";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent DeleteParticipationKeyByID(
            string participationId
        )
        {
            var path = $"/v2/participation/{participationId}";
            return this
                .Delete(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ParticipationKeyResponse> GetParticipationKeyByID(
            string participationId
        )
        {
            var path = $"/v2/participation/{participationId}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ParticipationKeyResponse> AppendKeys(
            byte[] keymap,
        
            string participationId
        )
        {
            var path = $"/v2/participation/{participationId}";
            return this
                .Post(path)
                .SetMessagePackBody(keymap)
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ParticipationKeysResponse> GetParticipationKeys(
             
        )
        {
            var path = $"/v2/participation";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<PostParticipationResponse> AddParticipationKey(
            byte[] participationkey
        )
        {
            var path = $"/v2/participation";
            return this
                .Post(path)
                .SetMessagePackBody(participationkey)
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<CompileResponse> TealCompile(
            byte[] source,
        
            Optional<bool> sourcemap = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("sourcemap", sourcemap)
                ;
            var path = $"/v2/teal/compile{queryBuilder}";
            return this
                .Post(path)
                .SetPlainTextBody(source)
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AccountResponse> AccountInformation(
            string address,
        
            ExcludeFields exclude = default,
        
            ResponseFormat format = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("exclude", exclude)
                .Add("format", format)
                ;
            var path = $"/v2/accounts/{address}{queryBuilder}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent HealthCheck(
             
        )
        {
            var path = $"/health";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<PendingTransactionResponse> PendingTransactionInformation(
            string txid,
        
            ResponseFormat format = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("format", format)
                ;
            var path = $"/v2/transactions/pending/{txid}{queryBuilder}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AccountApplicationResponse> AccountApplicationInformation(
            string address,
        
            ulong applicationId,
        
            ResponseFormat format = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("format", format)
                ;
            var path = $"/v2/accounts/{address}/applications/{applicationId}{queryBuilder}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AlgoApiObject> GetGenesis(
             
        )
        {
            var path = $"/genesis";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<VersionsResponse> GetVersion(
             
        )
        {
            var path = $"/versions";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<DisassembleResponse> TealDisassemble(
            byte[] source
        )
        {
            var path = $"/v2/teal/disassemble";
            return this
                .Post(path)
                .SetMessagePackBody(source)
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent Metrics(
             
        )
        {
            var path = $"/metrics";
            return this
                .Get(path)
                
                .Send()
                ;
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AccountAssetResponse> AccountAssetInformation(
            string address,
        
            ulong assetId,
        
            ResponseFormat format = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent)
                .Add("format", format)
                ;
            var path = $"/v2/accounts/{address}/assets/{assetId}{queryBuilder}";
            return this
                .Get(path)
                
                .Send()
                ;
        }

    }
}

using System;
using Algorand.Unity.Crypto;
using Algorand.Unity.Kmd;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// A client for accessing the kmd service
    /// </summary>
    /// <remarks>
    /// The kmd service is responsible for managing keys and wallets
    /// </remarks>
    [Serializable]
    public partial struct KmdClient : IKmdClient
    {
        [SerializeField] private string address;

        [SerializeField] private string token;

        [SerializeField] private Header[] headers;

        /// <summary>
        /// Create a new kmd client
        /// </summary>
        /// <param name="address">url of the service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="token">token used in authenticating to the service</param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key, my-api-key")</c></param>
        public KmdClient(string address, string token = null, params Header[] headers)
        {
            this.address = address.TrimEnd('/');
            this.token = token;
            this.headers = headers;
        }

        /// <summary>
        /// Create a new kmd client
        /// </summary>
        /// <param name="address">url of the service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key, my-api-key")</c></param>
        public KmdClient(string address, params Header[] headers) : this(address, null, headers)
        {
        }

        public string Address => address;

        public string Token => token;

        public string TokenHeader => "X-KMD-API-Token";

        public Header[] Headers => headers;

        /// <inheritdoc />
        public AlgoApiRequest.Sent<AlgoApiObject> GetSwaggerSpec()
        {
            return this
                .Get("/swagger.json")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<GenerateKeyResponse> GenerateKey(
            FixedString128Bytes walletHandleToken,
            Optional<bool> displayMnemonic = default
        )
        {
            var request = new GenerateKeyRequest { DisplayMnemonic = displayMnemonic, WalletHandleToken = walletHandleToken };
            return this
                .Post("/v1/key")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent DeleteKey(
            Address address,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        )
        {
            var request = new DeleteKeyRequest
            {
                Address = address,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return this
                .Delete("/v1/key")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ExportKeyResponse> ExportKey(
            Address address,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        )
        {
            var request = new ExportKeyRequest
            {
                Address = address,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return this
                .Post("/v1/key/export")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ImportKeyResponse> ImportKey(
            PrivateKey privateKey,
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ImportKeyRequest
            {
                PrivateKey = privateKey,
                WalletHandleToken = walletHandleToken
            };
            return this
                .Post("/v1/key/import")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ListKeysResponse> ListKeys(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ListKeysRequest { WalletHandleToken = walletHandleToken };
            return this
                .Post("/v1/key/list")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ExportMasterKeyResponse> ExportMasterKey(
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        )
        {
            var request = new ExportMasterKeyRequest
            {
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return this
                .Post("/v1/master-key/export")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent DeleteMultisig(
            Address address,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        )
        {
            var request = new DeleteMultisigRequest
            {
                Address = address,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return this
                .Delete("/v1/multisig")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ExportMultisigResponse> ExportMultisig(
            Address address,
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ExportMultisigRequest
            {
                Address = address,
                WalletHandleToken = walletHandleToken
            };
            return this
                .Post("/v1/multisig/export")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ImportMultisigResponse> ImportMultisig(
            Ed25519.PublicKey[] publicKeys,
            byte threshold,
            FixedString128Bytes walletHandleToken,
            byte version = 1
        )
        {
            var request = new ImportMultisigRequest
            {
                Version = version,
                PublicKeys = publicKeys,
                Threshold = threshold,
                WalletHandleToken = walletHandleToken
            };
            return this
                .Post("/v1/multisig/import")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ListMultisigResponse> ListMultisig(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ListMultisigRequest { WalletHandleToken = walletHandleToken };
            return this
                .Post("/v1/multisig/list")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<SignMultisigResponse> SignMultisig(
            MultisigSig msig,
            Ed25519.PublicKey publicKey,
            byte[] transactionData,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        )
        {
            var request = new SignMultisigRequest
            {
                Multisig = msig,
                PublicKey = publicKey,
                Transaction = transactionData,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return this
                .Post("/v1/multisig/sign")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<SignProgramMultisigResponse> SignProgramMultisig(
            Address msigAccount,
            byte[] programData,
            MultisigSig msig,
            Ed25519.PublicKey publicKey,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        )
        {
            var request = new SignProgramMultisigRequest
            {
                Address = msigAccount,
                Data = programData,
                Multisig = msig,
                PublicKey = publicKey,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return this
                .Post("/v1/multisig/signprogram")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<SignProgramResponse> SignProgram(
            Address account,
            byte[] programData,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        )
        {
            var request = new SignProgramRequest
            {
                Address = account,
                Data = programData,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return this
                .Post("/v1/program/sign")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<SignTransactionResponse> SignTransaction(
            Address account,
            byte[] transactionData,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        )
        {
            var request = new SignTransactionRequest
            {
                PublicKey = account,
                Transaction = transactionData,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return this
                .Post("/v1/transaction/sign")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<CreateWalletResponse> CreateWallet(
            PrivateKey masterDerivationKey,
            FixedString128Bytes walletDriverName,
            FixedString128Bytes walletName,
            FixedString128Bytes walletPassword
        )
        {
            var request = new CreateWalletRequest
            {
                MasterDerivationKey = masterDerivationKey,
                WalletDriverName = walletDriverName,
                WalletName = walletName,
                WalletPassword = walletPassword
            };
            return this
                .Post("/v1/wallet")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<WalletInfoResponse> WalletInfo(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new WalletInfoRequest { WalletHandleToken = walletHandleToken };
            return this
                .Post("/v1/wallet/info")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<InitWalletHandleTokenResponse> InitWalletHandleToken(
            FixedString128Bytes walletId,
            FixedString128Bytes walletPassword
        )
        {
            var request = new InitWalletHandleTokenRequest
            {
                WalletId = walletId,
                WalletPassword = walletPassword
            };
            return this
                .Post("/v1/wallet/init")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent ReleaseWalletHandleToken(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ReleaseWalletHandleTokenRequest { WalletHandleToken = walletHandleToken };
            return this
                .Post("/v1/wallet/release")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<RenameWalletResponse> RenameWallet(
            FixedString128Bytes walletId,
            FixedString128Bytes newName,
            FixedString128Bytes walletPassword
        )
        {
            var request = new RenameWalletRequest
            {
                WalletId = walletId,
                WalletName = newName,
                WalletPassword = walletPassword
            };
            return this
                .Post("/v1/wallet/rename")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<RenewWalletHandleTokenResponse> RenewWalletHandleToken(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new RenewWalletHandleTokenRequest { WalletHandleToken = walletHandleToken };
            return this
                .Post("/v1/wallet/renew")
                .SetJsonBody(request)
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<ListWalletsResponse> ListWallets()
        {
            return this
                .Get("/v1/wallets")
                .Send();
        }

        /// <inheritdoc />
        public AlgoApiRequest.Sent<VersionsResponse> Versions()
        {
            return this
                .Get("/versions")
                .Send();
        }
    }
}

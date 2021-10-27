using System;
using AlgoSdk.Crypto;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// A client for accessing the kmd service
    /// </summary>
    /// <remarks>
    /// The kmd service is responsible for managing keys and wallets
    /// </remarks>
    [Serializable]
    public struct KmdClient : IKmdClient
    {
        [SerializeField]
        string address;

        [SerializeField]
        string token;

        /// <summary>
        /// Create a new kmd client
        /// </summary>
        /// <param name="address">url of the service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="token">token used in authenticating to the service</param>
        public KmdClient(string address, string token)
        {
            this.address = address.TrimEnd('/');
            this.token = token;
        }

        public string Address => address;

        public string Token => token;

        public string TokenHeader => "X-KMD-API-Token";

        public async UniTask<AlgoApiResponse<AlgoApiObject>> GetSwaggerSpec()
        {
            return await this
                .Get("/swagger.json")
                .Send();
        }

        public async UniTask<AlgoApiResponse<GenerateKeyResponse>> GenerateKey(
            FixedString128Bytes walletHandleToken,
            Optional<bool> displayMnemonic = default
        )
        {
            var request = new GenerateKeyRequest { DisplayMnemonic = displayMnemonic, WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/key")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse> DeleteKey(
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
            return await this
                .Delete("/v1/key")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ExportKeyResponse>> ExportKey(
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
            return await this
                .Post("/v1/key/export")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ImportKeyResponse>> ImportKey(
            PrivateKey privateKey,
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ImportKeyRequest
            {
                PrivateKey = privateKey,
                WalletHandleToken = walletHandleToken
            };
            return await this
                .Post("/v1/key/import")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ListKeysResponse>> ListKeys(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ListKeysRequest { WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/key/list")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ExportMasterKeyResponse>> ExportMasterKey(
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        )
        {
            var request = new ExportMasterKeyRequest
            {
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return await this
                .Post("/v1/master-key/export")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse> DeleteMultisig(
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
            return await this
                .Delete("/v1/multisig")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ExportMultisigResponse>> ExportMultisig(
            Address address,
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ExportMultisigRequest
            {
                Address = address,
                WalletHandleToken = walletHandleToken
            };
            return await this
                .Post("/v1/multisig/export")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ImportMultisigResponse>> ImportMultisig(
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
            return await this
                .Post("/v1/multisig/import")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ListMultisigResponse>> ListMultisig(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ListMultisigRequest { WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/multisig/list")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<SignMultisigResponse>> SignMultisig(
            Multisig msig,
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
            return await this
                .Post("/v1/multisig/sign")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<SignProgramMultisigResponse>> SignProgramMultisig(
            Address msigAccount,
            byte[] programData,
            Multisig msig,
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
            return await this
                .Post("/v1/multisig/signprogram")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<SignProgramResponse>> SignProgram(
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
            return await this
                .Post("/v1/program/sign")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<SignTransactionResponse>> SignTransaction(
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
            return await this
                .Post("/v1/transaction/sign")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<CreateWalletResponse>> CreateWallet(
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
            return await this
                .Post("/v1/wallet")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<WalletInfoResponse>> WalletInfo(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new WalletInfoRequest { WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/wallet/info")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<InitWalletHandleTokenResponse>> InitWalletHandleToken(
            FixedString128Bytes walletId,
            FixedString128Bytes walletPassword
        )
        {
            var request = new InitWalletHandleTokenRequest
            {
                WalletId = walletId,
                WalletPassword = walletPassword
            };
            return await this
                .Post("/v1/wallet/init")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse> ReleaseWalletHandleToken(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new ReleaseWalletHandleTokenRequest { WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/wallet/release")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<RenameWalletResponse>> RenameWallet(
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
            return await this
                .Post("/v1/wallet/rename")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<RenewWalletHandleTokenResponse>> RenewWalletHandleToken(
            FixedString128Bytes walletHandleToken
        )
        {
            var request = new RenewWalletHandleTokenRequest { WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/wallet/renew")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ListWalletsResponse>> ListWallets()
        {
            return await this
                .Get("/v1/wallets")
                .Send();
        }

        public async UniTask<AlgoApiResponse<VersionsResponse>> Versions()
        {
            return await this
                .Get("/versions")
                .Send();
        }
    }
}

using AlgoSdk.Crypto;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public struct KmdClient : IKmdClient
    {
        readonly string address;

        readonly string token;

        public KmdClient(string address, string token)
        {
            this.address = address.TrimEnd('/');
            this.token = token;
        }

        public string Address => address;

        public string Token => token;

        public string TokenHeader => "X-KMD-API-Token";

        public async UniTask<AlgoApiResponse> GetSwaggerSpec()
        {
            return await this
                .Get("/swagger.json")
                .Send();
        }

        public async UniTask<AlgoApiResponse<GenerateKeyResponse>> GenerateKey(
            Optional<bool> displayMnemonic = default,
            FixedString128Bytes walletHandleToken = default
        )
        {
            var request = new GenerateKeyRequest { DisplayMnemonic = displayMnemonic, WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/key")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse> DeleteKey(
            Address address = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
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
            Address address = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
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
            PrivateKey privateKey = default,
            FixedString128Bytes walletHandleToken = default
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
            FixedString128Bytes walletHandleToken = default
        )
        {
            var request = new ListKeysRequest { WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/key/list")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ExportMasterKeyResponse>> ExportMasterKey(
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
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

        public async UniTask<AlgoApiResponse> DeleteMultiSig(
            Address address = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        )
        {
            var request = new DeleteMultiSigRequest
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

        public async UniTask<AlgoApiResponse<ExportMultiSigResponse>> ExportMultiSig(
            Address address = default,
            FixedString128Bytes walletHandleToken = default
        )
        {
            var request = new ExportMultiSigRequest
            {
                Address = address,
                WalletHandleToken = walletHandleToken
            };
            return await this
                .Post("/v1/multisig/export")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<ImportMultiSigResponse>> ImportMultiSig(
            Optional<byte> version = default,
            Ed25519.PublicKey[] publicKeys = default,
            Optional<byte> threshold = default,
            FixedString128Bytes walletHandleToken = default
        )
        {
            var request = new ImportMultiSigRequest
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

        public async UniTask<AlgoApiResponse<ListMultiSigResponse>> ListMultiSig(
            FixedString128Bytes walletHandleToken = default
        )
        {
            var request = new ListMultiSigRequest { WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/multisig/list")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<SignMultiSigResponse>> SignMultiSig(
            MultiSig multiSig = default,
            Ed25519.PublicKey publicKey = default,
            Address signer = default,
            byte[] transaction = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        )
        {
            var request = new SignMultiSigRequest
            {
                MultiSig = multiSig,
                PublicKey = publicKey,
                Signer = signer,
                Transaction = transaction,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return await this
                .Post("/v1/multisig/sign")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<SignProgramMultiSigResponse>> SignProgramMultiSig(
            Address address = default,
            byte[] data = default,
            MultiSig multiSig = default,
            Ed25519.PublicKey publicKey = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        )
        {
            var request = new SignProgramMultiSigRequest
            {
                Address = address,
                Data = data,
                MultiSig = multiSig,
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
            Address address = default,
            byte[] data = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        )
        {
            var request = new SignProgramRequest
            {
                Address = address,
                Data = data,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return await this
                .Post("/v1/program/sign")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<SignTransactionResponse>> SignTransaction(
            Ed25519.PublicKey publicKey = default,
            byte[] transaction = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        )
        {
            var request = new SignTransactionRequest
            {
                PublicKey = publicKey,
                Transaction = transaction,
                WalletHandleToken = walletHandleToken,
                WalletPassword = walletPassword
            };
            return await this
                .Post("/v1/transaction/sign")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<CreateWalletResponse>> CreateWallet(
            PrivateKey masterDerivationKey = default,
            FixedString128Bytes walletDriverName = default,
            FixedString128Bytes walletName = default,
            FixedString128Bytes walletPassword = default
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
            FixedString128Bytes walletHandleToken = default
        )
        {
            var request = new WalletInfoRequest { WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/wallet/info")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<InitWalletHandleTokenResponse>> InitWalletHandleToken(
            FixedString128Bytes walletId = default,
            FixedString128Bytes walletPassword = default
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
            FixedString128Bytes walletHandleToken = default
        )
        {
            var request = new ReleaseWalletHandleTokenRequest { WalletHandleToken = walletHandleToken };
            return await this
                .Post("/v1/wallet/release")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<RenameWalletResponse>> RenameWallet(
            FixedString128Bytes walletId = default,
            FixedString128Bytes walletName = default,
            FixedString128Bytes walletPassword = default
        )
        {
            var request = new RenameWalletRequest
            {
                WalletId = walletId,
                WalletName = walletName,
                WalletPassword = walletPassword
            };
            return await this
                .Post("/v1/wallet/rename")
                .SetJsonBody(request)
                .Send();
        }

        public async UniTask<AlgoApiResponse<RenewWalletHandleTokenResponse>> RenewWalletHandleToken(
            FixedString128Bytes walletHandleToken = default
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

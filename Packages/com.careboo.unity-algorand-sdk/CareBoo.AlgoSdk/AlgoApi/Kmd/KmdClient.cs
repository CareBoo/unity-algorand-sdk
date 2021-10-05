using Cysharp.Threading.Tasks;

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

        public async UniTask<AlgoApiResponse> GetSwaggerSpec()
        {
            return await this.GetAsync("/swagger.json");
        }

        public async UniTask<AlgoApiResponse<GenerateKeyResponse>> GenerateKey(GenerateKeyRequest request)
        {
            return await this.PostAsync("/v1/key", request);
        }

        public async UniTask<AlgoApiResponse<DeleteKeyResponse>> DeleteKey(DeleteKeyRequest request)
        {
            return await this.DeleteAsync("/v1/key", request);
        }

        public async UniTask<AlgoApiResponse<ExportKeyResponse>> ExportKey(ExportKeyRequest request)
        {
            return await this.PostAsync("/v1/key/export", request);
        }

        public async UniTask<AlgoApiResponse<ImportKeyResponse>> ImportKey(ImportKeyRequest request)
        {

            return await this.PostAsync("/v1/key/import", request);
        }

        public async UniTask<AlgoApiResponse<ListKeysResponse>> ListKeys(ListKeysRequest request)
        {
            return await this.PostAsync("/v1/key/list", request);
        }

        public async UniTask<AlgoApiResponse<ExportMasterKeyResponse>> ExportMasterKey(ExportMasterKeyRequest request)
        {
            return await this.PostAsync("/v1/master-key/export", request);
        }

        public async UniTask<AlgoApiResponse<DeleteMultiSigResponse>> DeleteMultiSig(DeleteMultiSigRequest request)
        {
            return await this.DeleteAsync("/v1/multisig", request);
        }

        public async UniTask<AlgoApiResponse<ExportMultiSigResponse>> ExportMultiSig(ExportMultiSigRequest request)
        {
            return await this.PostAsync("/v1/multisig/export", request);
        }

        public async UniTask<AlgoApiResponse<ImportMultiSigResponse>> ImportMultiSig(ImportMultiSigRequest request)
        {
            return await this.PostAsync("/v1/multisig/import", request);
        }

        public async UniTask<AlgoApiResponse<ListMultiSigResponse>> ListMultiSig(ListMultiSigRequest request)
        {
            return await this.PostAsync("/v1/multisig/list", request);
        }

        public async UniTask<AlgoApiResponse<SignMultiSigResponse>> SignMultiSig(SignMultiSigRequest request)
        {
            return await this.PostAsync("/v1/multisig/sign", request);
        }

        public async UniTask<AlgoApiResponse<SignProgramMultiSigResponse>> SignProgramMultiSig(SignProgramMultiSigRequest request)
        {
            return await this.PostAsync("/v1/multisig/signprogram", request);
        }

        public async UniTask<AlgoApiResponse<SignProgramResponse>> SignProgram(SignProgramRequest request)
        {
            return await this.PostAsync("/v1/program/sign", request);
        }

        public async UniTask<AlgoApiResponse<SignTransactionResponse>> SignTransaction(SignTransactionRequest request)
        {
            return await this.PostAsync("/v1/transaction/sign", request);
        }

        public async UniTask<AlgoApiResponse<CreateWalletResponse>> CreateWallet(CreateWalletRequest request)
        {
            return await this.PostAsync("/v1/wallet", request);
        }

        public async UniTask<AlgoApiResponse<WalletInfoResponse>> WalletInfo(WalletInfoRequest request)
        {
            return await this.PostAsync("/v1/wallet/info", request);
        }

        public async UniTask<AlgoApiResponse<InitWalletHandleTokenResponse>> InitWalletHandleToken(InitWalletHandleTokenRequest request)
        {
            return await this.PostAsync("/v1/wallet/init", request);
        }

        public async UniTask<AlgoApiResponse<ReleaseWalletHandleTokenResponse>> ReleaseWalletHandleToken(ReleaseWalletHandleTokenRequest request)
        {
            return await this.PostAsync("/v1/wallet/release", request);
        }

        public async UniTask<AlgoApiResponse<RenameWalletResponse>> RenameWallet(RenameWalletRequest request)
        {
            return await this.PostAsync("/v1/wallet/rename", request);
        }

        public async UniTask<AlgoApiResponse<RenewWalletHandleTokenResponse>> RenewWalletHandleToken(RenewWalletHandleTokenRequest request)
        {
            return await this.PostAsync("/v1/wallet/renew", request);
        }

        public async UniTask<AlgoApiResponse<ListWalletsResponse>> ListWallets()
        {
            return await this.GetAsync("/v1/wallets");
        }

        public async UniTask<AlgoApiResponse<VersionsResponse>> Versions()
        {
            return await this.GetAsync("/v1/versions");
        }
    }
}

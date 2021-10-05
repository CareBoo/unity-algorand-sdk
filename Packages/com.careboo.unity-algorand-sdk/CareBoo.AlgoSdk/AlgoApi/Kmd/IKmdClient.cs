using Cysharp.Threading.Tasks;

namespace AlgoSdk
{
    public interface IKmdClient : IAlgoApiClient
    {
        UniTask<AlgoApiResponse> GetSwaggerSpec();
        UniTask<AlgoApiResponse<GenerateKeyResponse>> GenerateKey(GenerateKeyRequest request);
        UniTask<AlgoApiResponse<DeleteKeyResponse>> DeleteKey(DeleteKeyRequest request);
        UniTask<AlgoApiResponse<ExportKeyResponse>> ExportKey(ExportKeyRequest request);
        UniTask<AlgoApiResponse<ImportKeyResponse>> ImportKey(ImportKeyRequest request);
        UniTask<AlgoApiResponse<ListKeysResponse>> ListKeys(ListKeysRequest request);
        UniTask<AlgoApiResponse<ExportMasterKeyResponse>> ExportMasterKey(ExportMasterKeyRequest request);
        UniTask<AlgoApiResponse<DeleteMultiSigResponse>> DeleteMultiSig(DeleteMultiSigRequest request);
        UniTask<AlgoApiResponse<ExportMultiSigResponse>> ExportMultiSig(ExportMultiSigRequest request);
        UniTask<AlgoApiResponse<ImportMultiSigResponse>> ImportMultiSig(ImportMultiSigRequest request);
        UniTask<AlgoApiResponse<ListMultiSigResponse>> ListMultiSig(ListMultiSigRequest request);
        UniTask<AlgoApiResponse<SignMultiSigResponse>> SignMultiSig(SignMultiSigRequest request);
        UniTask<AlgoApiResponse<SignProgramMultiSigResponse>> SignProgramMultiSig(SignProgramMultiSigRequest request);
        UniTask<AlgoApiResponse<SignProgramResponse>> SignProgram(SignProgramRequest request);
        UniTask<AlgoApiResponse<SignTransactionResponse>> SignTransaction(SignTransactionRequest request);
        UniTask<AlgoApiResponse<CreateWalletResponse>> CreateWallet(CreateWalletRequest request);
        UniTask<AlgoApiResponse<WalletInfoResponse>> WalletInfo(WalletInfoRequest request);
        UniTask<AlgoApiResponse<InitWalletHandleTokenResponse>> InitWalletHandleToken(InitWalletHandleTokenRequest request);
        UniTask<AlgoApiResponse<ReleaseWalletHandleTokenResponse>> ReleaseWalletHandleToken(ReleaseWalletHandleTokenRequest request);
        UniTask<AlgoApiResponse<RenameWalletResponse>> RenameWallet(RenameWalletRequest request);
        UniTask<AlgoApiResponse<RenewWalletHandleTokenResponse>> RenewWalletHandleToken(RenewWalletHandleTokenRequest request);
        UniTask<AlgoApiResponse<ListWalletsResponse>> ListWallets();
        UniTask<AlgoApiResponse<VersionsResponse>> Versions();
    }
}

using AlgoSdk.Crypto;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IKmdClient : IAlgoApiClient
    {
        UniTask<AlgoApiResponse> GetSwaggerSpec();
        UniTask<AlgoApiResponse<GenerateKeyResponse>> GenerateKey(
            Optional<bool> displayMnemonic = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<DeleteKeyResponse>> DeleteKey(
            Address address = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<ExportKeyResponse>> ExportKey(
            Address address = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<ImportKeyResponse>> ImportKey(
            PrivateKey privateKey = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<ListKeysResponse>> ListKeys(
            FixedString128Bytes walletHandleToken = default
        );
        UniTask<AlgoApiResponse<ExportMasterKeyResponse>> ExportMasterKey(
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<DeleteMultiSigResponse>> DeleteMultiSig(
            Address address = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<ExportMultiSigResponse>> ExportMultiSig(
            Address address = default,
            FixedString128Bytes walletHandleToken = default
        );
        UniTask<AlgoApiResponse<ImportMultiSigResponse>> ImportMultiSig(
            Optional<byte> version = default,
            Ed25519.PublicKey[] publicKeys = default,
            Optional<byte> threshold = default,
            FixedString128Bytes walletHandleToken = default
        );
        UniTask<AlgoApiResponse<ListMultiSigResponse>> ListMultiSig(
            FixedString128Bytes walletHandleToken = default
        );
        UniTask<AlgoApiResponse<SignMultiSigResponse>> SignMultiSig(
            MultiSig multiSig = default,
            Ed25519.PublicKey publicKey = default,
            Address signer = default,
            byte[] transaction = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<SignProgramMultiSigResponse>> SignProgramMultiSig(
            Address address = default,
            byte[] data = default,
            MultiSig multiSig = default,
            Ed25519.PublicKey publicKey = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<SignProgramResponse>> SignProgram(
            Address address = default,
            byte[] data = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<SignTransactionResponse>> SignTransaction(
            Ed25519.PublicKey publicKey = default,
            byte[] transaction = default,
            FixedString128Bytes walletHandleToken = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<CreateWalletResponse>> CreateWallet(
            PrivateKey masterDerivationKey = default,
            FixedString128Bytes walletDriverName = default,
            FixedString128Bytes walletName = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<WalletInfoResponse>> WalletInfo(
            FixedString128Bytes walletHandleToken = default
        );
        UniTask<AlgoApiResponse<InitWalletHandleTokenResponse>> InitWalletHandleToken(
            FixedString128Bytes walletId = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<ReleaseWalletHandleTokenResponse>> ReleaseWalletHandleToken(
            FixedString128Bytes walletHandleToken = default
        );
        UniTask<AlgoApiResponse<RenameWalletResponse>> RenameWallet(
            FixedString128Bytes walletId = default,
            FixedString128Bytes walletName = default,
            FixedString128Bytes walletPassword = default
        );
        UniTask<AlgoApiResponse<RenewWalletHandleTokenResponse>> RenewWalletHandleToken(
            FixedString128Bytes walletHandleToken = default
        );
        UniTask<AlgoApiResponse<ListWalletsResponse>> ListWallets();
        UniTask<AlgoApiResponse<VersionsResponse>> Versions();
    }
}

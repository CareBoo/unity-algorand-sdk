using AlgoSdk.Crypto;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IKmdClient : IAlgoApiClient
    {
        UniTask<AlgoApiResponse> GetSwaggerSpec();
        UniTask<AlgoApiResponse<GenerateKeyResponse>> GenerateKey(
            FixedString128Bytes walletPassword,
            Optional<bool> displayMnemonic = default
        );
        UniTask<AlgoApiResponse> DeleteKey(
            Address address,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<ExportKeyResponse>> ExportKey(
            Address address,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<ImportKeyResponse>> ImportKey(
            PrivateKey privateKey,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<ListKeysResponse>> ListKeys(
            FixedString128Bytes walletHandleToken
        );
        UniTask<AlgoApiResponse<ExportMasterKeyResponse>> ExportMasterKey(
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse> DeleteMultisig(
            Address address,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<ExportMultisigResponse>> ExportMultisig(
            Address address,
            FixedString128Bytes walletHandleToken
        );
        UniTask<AlgoApiResponse<ImportMultisigResponse>> ImportMultisig(
            byte version,
            Ed25519.PublicKey[] publicKeys,
            byte threshold,
            FixedString128Bytes walletHandleToken
        );
        UniTask<AlgoApiResponse<ListMultisigResponse>> ListMultisig(
            FixedString128Bytes walletHandleToken
        );
        UniTask<AlgoApiResponse<SignMultisigResponse>> SignMultisig(
            Multisig msig,
            Ed25519.PublicKey subAccount,
            byte[] transactionData,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<SignProgramMultisigResponse>> SignProgramMultisig(
            Address msigAccount,
            byte[] programData,
            Multisig multisig,
            Ed25519.PublicKey publicKey,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<SignProgramResponse>> SignProgram(
            Address account,
            byte[] programData,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<SignTransactionResponse>> SignTransaction(
            Address account,
            byte[] transactionData,
            FixedString128Bytes walletHandleToken,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<CreateWalletResponse>> CreateWallet(
            PrivateKey masterDerivationKey,
            FixedString128Bytes walletDriverName,
            FixedString128Bytes walletName,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<WalletInfoResponse>> WalletInfo(
            FixedString128Bytes walletHandleToken
        );
        UniTask<AlgoApiResponse<InitWalletHandleTokenResponse>> InitWalletHandleToken(
            FixedString128Bytes walletId,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse> ReleaseWalletHandleToken(
            FixedString128Bytes walletHandleToken
        );
        UniTask<AlgoApiResponse<RenameWalletResponse>> RenameWallet(
            FixedString128Bytes walletId,
            FixedString128Bytes walletName,
            FixedString128Bytes walletPassword
        );
        UniTask<AlgoApiResponse<RenewWalletHandleTokenResponse>> RenewWalletHandleToken(
            FixedString128Bytes walletHandleToken
        );
        UniTask<AlgoApiResponse<ListWalletsResponse>> ListWallets();
        UniTask<AlgoApiResponse<VersionsResponse>> Versions();
    }
}

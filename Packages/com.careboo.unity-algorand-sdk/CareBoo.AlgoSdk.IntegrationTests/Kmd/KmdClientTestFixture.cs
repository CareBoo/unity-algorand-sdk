using System.Linq;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine.Networking;

[TestFixture]
public abstract class KmdClientTestFixture : AlgoApiClientTestFixture
{
    public const string WalletName = "AlgoSdkTestWallet";
    public const string WalletPassword = "helloworld123";
    public const string WalletDriverName = "sqlite";
    public FixedString128Bytes walletHandleToken = default;
    public Wallet wallet;

    protected override AlgoServices RequiresServices => AlgoServices.Kmd;

    protected override async UniTask SetUpAsync()
    {
        await InitWalletHandleToken();
    }

    protected override async UniTask TearDownAsync()
    {
        await ReleaseWalletHandleToken();
    }

    protected async UniTask InitWalletHandleToken()
    {
        if (walletHandleToken.Length > 0)
            return;
        var wallets = await ListWallets();
        wallet = wallets?.FirstOrDefault(w => w.Name.Equals(WalletName)) ?? default;
        if (wallet.Equals(default))
        {
            wallet = await CreateWallet();
        }
        var initWalletHandleResponse = await AlgoApiClientSettings.Kmd.InitWalletHandleToken(wallet.Id, WalletPassword);
        walletHandleToken = initWalletHandleResponse.Payload.WalletHandleToken;
    }

    protected async UniTask ReleaseWalletHandleToken()
    {
        if (walletHandleToken.Length > 0)
            await AlgoApiClientSettings.Kmd.ReleaseWalletHandleToken(walletHandleToken);
        walletHandleToken.Clear();
        wallet = default;
    }

    static async UniTask<Wallet[]> ListWallets()
    {
        var response = await AlgoApiClientSettings.Kmd.ListWallets();
        if (response.Status != UnityWebRequest.Result.Success)
        {
            Assert.Ignore(
                $"Ignoring test because {nameof(ListWallets)} response was {response.ResponseCode}: {response.Status}. Message:\n\"{response.Error.Message}\""
            );
        }
        return response.Payload.Wallets;
    }

    static async UniTask<Wallet> CreateWallet()
    {
        var response = await AlgoApiClientSettings.Kmd.CreateWallet(
            masterDerivationKey: AlgoSdk.Crypto.Random.Bytes<PrivateKey>(),
            walletDriverName: WalletDriverName,
            walletName: WalletName,
            walletPassword: WalletPassword
        );
        if (response.Status != UnityWebRequest.Result.Success)
        {
            Assert.Ignore(
                $"Ignoring test because {nameof(CreateWallet)} response was {response.ResponseCode}: {response.Status}. Message:\n\"{response.Error.Message}\""
            );
        }
        return response.Payload.Wallet;
    }
}

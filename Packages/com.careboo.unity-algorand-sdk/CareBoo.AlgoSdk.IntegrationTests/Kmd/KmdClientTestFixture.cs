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
    public FixedString128Bytes walletHandle = default;

    protected override AlgoServices RequiresServices => AlgoServices.Kmd;

    protected override async UniTask SetUpAsync()
    {
        await CheckServices();
        await InitWalletHandleToken();
    }

    protected override async UniTask TearDownAsync()
    {
        await ReleaseWalletHandleToken();
    }

    protected async UniTask InitWalletHandleToken()
    {
        if (walletHandle.Length > 0)
            return;
        var wallets = await ListWallets();
        Wallet wallet = wallets.FirstOrDefault(w => w.Name.Equals(WalletName));
        if (wallet.Equals(default))
        {
            wallet = await CreateWallet();
        }
        var initWalletHandleResponse = await kmd.InitWalletHandleToken(wallet.Id, WalletPassword);
        walletHandle = initWalletHandleResponse.Payload.WalletHandleToken;
    }

    protected async UniTask ReleaseWalletHandleToken()
    {
        if (walletHandle.Length > 0)
            await kmd.ReleaseWalletHandleToken(walletHandle);
        walletHandle.Clear();
    }

    static async UniTask<Wallet[]> ListWallets()
    {
        var response = await kmd.ListWallets();
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
        var response = await kmd.CreateWallet(
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

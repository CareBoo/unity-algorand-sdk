using System.Collections;
using System.Linq;
using AlgoSdk;
using AlgoSdk.Crypto;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

[TestFixture]
public class KmdClientTest : AlgoApiClientTest
{
    const string WalletName = "AlgoSdkTestWallet";
    const string WalletPassword = "helloworld123";
    const string WalletDriverName = "sqlite";
    FixedString128Bytes walletHandle = default;

    protected override AlgoServices RequiresServices => AlgoServices.Kmd;

    static async UniTask<(bool, Wallet[])> TryListWallets()
    {
        var response = await kmd.ListWallets();
        if (response.Status != UnityWebRequest.Result.Success)
        {
            Assert.Ignore(
                $"Ignoring test because {nameof(TryListWallets)} response was {response.ResponseCode}: {response.Status}. Message:\n\"{response.Error.Message}\""
            );
            return (false, default);
        }
        if (response.Payload.Error)
        {
            Assert.Ignore(
                $"Ignoring test because of {nameof(TryListWallets)} API err:\n\"{response.Payload.Message}\""
            );
            return (false, default);
        }
        return (true, response.Payload.Wallets);
    }

    static async UniTask<(bool, Wallet)> TryCreateWallet()
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
                $"Ignoring test because {nameof(TryCreateWallet)} response was {response.ResponseCode}: {response.Status}. Message:\n\"{response.Error.Message}\""
            );
            return (false, default);
        }
        if (response.Payload.Error)
        {
            Assert.Ignore(
                $"Ignoring test because of {nameof(TryCreateWallet)} API err:\n\"{response.Payload.Message}\""
            );
            return (false, default);
        }
        return (true, response.Payload.Wallet);
    }

    protected override async UniTask SetUpAsync()
    {
        await base.SetUpAsync();
        if (walletHandle.Length > 0)
            return;
        var (ok, wallets) = await TryListWallets();
        if (!ok)
            return;
        Wallet wallet = wallets.FirstOrDefault(w => w.Name.Equals(WalletName));
        if (wallet.Equals(default))
        {
            (ok, wallet) = await TryCreateWallet();
            if (!ok)
                return;
        }
        var initWalletHandleResponse = await kmd.InitWalletHandleToken(wallet.Id, WalletPassword);
        walletHandle = initWalletHandleResponse.Payload.WalletHandleToken;
        return;
    }

    [UnityTearDown]
    public IEnumerator TearDownTest() => UniTask.ToCoroutine(async () =>
    {
        if (walletHandle.Length > 0)
            await kmd.ReleaseWalletHandleToken(walletHandle);
        walletHandle.Clear();
    });

    [UnityTest]
    public IEnumerator GetSwaggerSpecShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.GetSwaggerSpec();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GenerateKeyThenDeleteKeyShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
    });
}

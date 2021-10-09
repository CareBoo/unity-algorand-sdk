using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

[TestFixture]
[ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires kmd service to be running.")]
public class KmdClientTest : AlgoApiClientTest
{
    const string WalletPassword = "helloworld123";
    FixedString128Bytes walletHandle;

    static async UniTask<(bool, Wallet[])> TryListWallets()
    {
        var response = await kmd.ListWallets();
        if (response.Status != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning(
                $"Ignoring test because {nameof(TryListWallets)} response was {response.ResponseCode}: {response.Status}. Message:\n\"{response.Error.Message}\""
            );
            return (false, default);
        }
        if (response.Payload.Error)
        {
            Debug.LogWarning(
                $"Ignoring test because of {nameof(TryListWallets)} API err:\n\"{response.Payload.Message}\""
            );
            return (false, default);
        }
        return (true, response.Payload.Wallets);
    }

    static async UniTask<(bool, Wallet)> TryCreateWallet()
    {
        var response = await kmd.CreateWallet();
        if (response.Status != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning(
                $"Ignoring test because {nameof(TryCreateWallet)} response was {response.ResponseCode}: {response.Status}. Message:\n\"{response.Error.Message}\""
            );
            return (false, default);
        }
        if (response.Payload.Error)
        {
            Debug.LogWarning(
                $"Ignoring test because of {nameof(TryCreateWallet)} API err:\n\"{response.Payload.Message}\""
            );
            return (false, default);
        }
        return (true, response.Payload.Wallet);
    }

    async UniTask<bool> TrySetUp()
    {
        if (walletHandle.Length > 0)
            return true;
        var (ok, wallets) = await TryListWallets();
        if (!ok)
            return false;
        Wallet wallet;
        if (wallets.Length == 0)
        {
            (ok, wallet) = await TryCreateWallet();
            if (!ok)
                return false;
        }
        else
        {
            wallet = wallets[0];
        }
        var initWalletHandleResponse = await kmd.InitWalletHandleToken(wallet.Id, WalletPassword);
        walletHandle = initWalletHandleResponse.Payload.WalletHandleToken;
        return true;
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
        if (!await TrySetUp())
            Assert.Ignore();
    });
}

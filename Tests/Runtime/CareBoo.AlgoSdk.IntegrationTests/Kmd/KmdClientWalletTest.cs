using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

[TestFixture]
public class KmdClientWalletTest : KmdClientTestFixture
{
    [UnityTest]
    public IEnumerator WalletInfoShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Kmd.WalletInfo(walletHandleToken);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator RenameWalletShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        const string newName = "new wallet name";
        var response = await AlgoApiClientSettings.Kmd.RenameWallet(wallet.Id, newName, WalletPassword);
        AssertOkay(response.Error);
        await AlgoApiClientSettings.Kmd.RenameWallet(wallet.Id, WalletName, WalletPassword);
    });

    [UnityTest]
    public IEnumerator RenewWalletHandleTokenShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Kmd.RenewWalletHandleToken(walletHandleToken);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator ListWalletsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Kmd.ListWallets();
        AssertOkay(response.Error);
    });
}

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
        var response = await kmd.WalletInfo(walletHandleToken);
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator RenameWalletShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        const string newName = "new wallet name";
        var response = await kmd.RenameWallet(wallet.Id, newName, WalletPassword);
        AssertResponseSuccess(response);
        response = await kmd.RenameWallet(wallet.Id, WalletName, WalletPassword);
    });

    [UnityTest]
    public IEnumerator RenewWalletHandleTokenShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.RenewWalletHandleToken(walletHandleToken);
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator ListWalletsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.ListWallets();
        AssertResponseSuccess(response);
    });
}

using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
[ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires kmd service to be running.")]
public class KmdClientTest : AlgoApiClientTest
{
    const string WalletPassword = "helloworld123";
    string walletHandle;

    [UnitySetUp]
    public IEnumerator SetUpTest() => UniTask.ToCoroutine(async () =>
    {
        var createWalletResponse = await kmd.CreateWallet(walletPassword: WalletPassword);
        var wallet = createWalletResponse.Payload.Wallet;
        var initWalletHandleResponse = await kmd.InitWalletHandleToken(wallet.Id, WalletPassword);
        walletHandle = initWalletHandleResponse.Payload.WalletHandleToken;
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

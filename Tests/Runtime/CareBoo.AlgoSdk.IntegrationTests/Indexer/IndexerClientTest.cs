using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

[TestFixture]
public class IndexerClientTest : IndexerClientTestFixture
{
    [UnityTest]
    public IEnumerator GetAccountShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        Address accountAddress = AlgoApiClientSettings.AccountMnemonic.ToPrivateKey().ToPublicKey();
        var response = await AlgoApiClientSettings.Indexer.GetAccount(accountAddress);
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetAccountsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.GetAccounts();
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetAccountsGreaterThan1000AlgoShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.GetAccounts(currencyGreaterThan: 1000);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetAccountsPaginatedShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        ulong limit = 3;
        var firstPageResponse = await AlgoApiClientSettings.Indexer.GetAccounts(limit: limit);
        AssertOkay(firstPageResponse.Error);
        var secondPageResponse = await AlgoApiClientSettings.Indexer.GetAccounts(
            limit: limit,
            next: firstPageResponse.Payload.NextToken);
        AssertOkay(secondPageResponse.Error);
    });


    [UnityTest]
    public IEnumerator GetAccountTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        await MakePaymentTransaction(10_000);
        Address accountAddress = AlgoApiClientSettings.AccountMnemonic.ToPrivateKey().ToPublicKey();
        var response = await AlgoApiClientSettings.Indexer.GetAccountTransactions(accountAddress);
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetApplicationsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.GetApplications();
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetAssetsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.GetAssets();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.GetTransactions();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetLogicSigTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.GetTransactions(sigType: SignatureType.LogicSig);
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetHealthShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.GetHealth();
        AssertOkay(response.Error);
        Assert.IsTrue(response.Payload.DatabaseAvailable);
    });
}

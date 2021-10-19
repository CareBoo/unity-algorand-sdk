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
        Address accountAddress = AccountMnemonic.ToPrivateKey().ToPublicKey();
        var response = await indexer.GetAccount(accountAddress);
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetAccountsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetAccounts();
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetAccountsGreaterThan1000AlgoShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetAccounts(currencyGreaterThan: 1000);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetAccountsPaginatedShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        ulong limit = 3;
        var firstPageResponse = await indexer.GetAccounts(limit: limit);
        AssertOkay(firstPageResponse.Error);
        var secondPageResponse = await indexer.GetAccounts(
            limit: limit,
            next: firstPageResponse.Payload.NextToken);
        AssertOkay(secondPageResponse.Error);
    });


    [UnityTest]
    public IEnumerator GetAccountTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        await MakePaymentTransaction(10_000);
        Address accountAddress = AccountMnemonic.ToPrivateKey().ToPublicKey();
        var response = await indexer.GetAccountTransactions(accountAddress);
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetApplicationsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetApplications();
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetAssetsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetAssets();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetTransactions();
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetHealthShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetHealth();
        AssertOkay(response.Error);
        Assert.IsTrue(response.Payload.DatabaseAvailable);
    });
}

using System.Collections;
using Algorand.Unity;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

[TestFixture]
public class IndexerClientTest : IndexerClientTestFixture
{
    [UnityTest]
    public IEnumerator GetAccountShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        Address accountAddress = PublicKey;
        var response = await AlgoApiClientSettings.Indexer.LookupAccountByID(accountAddress);
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetAccountsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.SearchForAccounts();
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetAccountsGreaterThan1000AlgoShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.SearchForAccounts(currencyGreaterThan: 1000);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetAccountsPaginatedShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        ulong limit = 3;
        var firstPageResponse = await AlgoApiClientSettings.Indexer.SearchForAccounts(limit: limit);
        AssertOkay(firstPageResponse.Error);
        var secondPageResponse = await AlgoApiClientSettings.Indexer.SearchForAccounts(
            limit: limit,
            next: firstPageResponse.Payload.NextToken);
        AssertOkay(secondPageResponse.Error);
    });


    [UnityTest]
    public IEnumerator GetAccountTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        await MakePaymentTransaction(100_000);
        Address accountAddress = PublicKey;
        var response = await AlgoApiClientSettings.Indexer.LookupAccountTransactions(accountAddress);
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetApplicationsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.SearchForApplications();
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetAssetsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.SearchForAssets();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.SearchForTransactions();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetLogicSigTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.SearchForTransactions(sigType: SignatureType.LogicSig);
        AssertOkay(response.Error);
    });


    [UnityTest]
    public IEnumerator GetHealthShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Indexer.MakeHealthCheck();
        AssertOkay(response.Error);
        Assert.IsTrue(response.Payload.WrappedValue.DbAvailable);
    });
}

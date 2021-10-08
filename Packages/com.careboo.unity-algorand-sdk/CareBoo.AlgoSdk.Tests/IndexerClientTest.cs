using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires indexer service to be running.")]
public class IndexerClientTest : AlgoApiClientTest
{
    [UnityTest]
    public IEnumerator GetAccountShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        Address accountAddress = AccountMnemonic.ToPrivateKey().ToPublicKey();
        var response = await indexer.GetAccount(accountAddress);
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator GetAccountsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetAccounts();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator GetAccountsGreaterThan1000AlgoShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetAccounts(new AccountsQuery
        {
            CurrencyGreaterThan = 1000
        });
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    [Ignore("limit isn't working at the moment...")]
    public IEnumerator GetAccountsPaginatedShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        ulong limit = 1;
        var firstPageResponse = await indexer.GetAccounts(new AccountsQuery
        {
            Limit = limit,
            IncludeAll = true
        });
        Debug.Log($"first page:\n{firstPageResponse.GetText()}");
        AssertResponseSuccess(firstPageResponse);
        var secondPageResponse = await indexer.GetAccounts(new AccountsQuery
        {
            Next = firstPageResponse.Payload.NextToken,
            Limit = limit
        });
        Debug.Log($"second page:\n{secondPageResponse.GetText()}");
        AssertResponseSuccess(secondPageResponse);
    });


    [UnityTest]
    public IEnumerator GetAccountTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        await MakePaymentTransaction(10_000);
        Address accountAddress = AccountMnemonic.ToPrivateKey().ToPublicKey();
        var response = await indexer.GetAccountTransactions(accountAddress);
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator GetApplicationsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetApplications();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator GetAssetsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetAssets();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator GetHealthShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await indexer.GetHealth();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
        Assert.IsTrue(response.Payload.DatabaseAvailable);
    });
}

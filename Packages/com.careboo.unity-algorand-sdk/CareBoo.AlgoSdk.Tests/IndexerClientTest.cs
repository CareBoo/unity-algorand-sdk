using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

[ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires indexer service to be running.")]
public class IndexerClientTest : AlgoApiClientTest
{
    const string SandboxToken = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    const string SandBoxAddress = "http://localhost:8980";
    static readonly Mnemonic AccountMnemonic = "earth burst hero frown popular genius occur interest hobby push throw canoe orchard dish shed poem child frequent shop lecture female define state abstract tree";

    static readonly IndexerClient client = new IndexerClient(SandBoxAddress, SandboxToken);

    static async UniTask<bool> SandboxIsHealthy()
    {
        var response = await client.GetHealth();
        return response.Payload.DatabaseAvailable;
    }


    [UnityTest]
    public IEnumerator GetAccountShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        Address accountAddress = AccountMnemonic.ToPrivateKey().ToPublicKey();
        var response = await client.GetAccount(accountAddress);
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator GetAccountsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetAccounts();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });


    [UnityTest]
    public IEnumerator GetAccountsGreaterThan1000AlgoShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetAccounts(new AccountsQuery
        {
            CurrencyGreaterThan = 1000
        });
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetAccountsPaginatedShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var firstPageResponse = await client.GetAccounts(new AccountsQuery
        {
            Limit = 1
        });
        Debug.Log(firstPageResponse.Raw.GetText());
        AssertResponseSuccess(firstPageResponse);
        Assert.AreEqual(firstPageResponse.Payload.Accounts.Length, 1);
        var secondPageResponse = await client.GetAccounts(new AccountsQuery
        {
            Next = firstPageResponse.Payload.NextToken,
            Limit = 1
        });
        Debug.Log(secondPageResponse.Raw.GetText());
        AssertResponseSuccess(secondPageResponse);
        Assert.AreEqual(secondPageResponse.Payload.Accounts.Length, 1);
    });
}

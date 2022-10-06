using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class DotnetAlgodClientTest : AlgodClientTest
{
    [Test]
    public void InstantiatingClientShouldThrowNoErrors()
    {
        var defaultAlgod = AlgoApiClientSettings.DefaultAlgod;
        using var client = new AlgodApiClient(
            defaultAlgod.Address + '/',
            defaultAlgod.Token,
            defaultAlgod.Headers
            );
    }

    [UnityTest]
    public IEnumerator SendSimpleRequestShouldThrowNoErrors() => UniTask.ToCoroutine(async () =>
    {
        var defaultAlgod = AlgoApiClientSettings.DefaultAlgod;
        using var client = new AlgodApiClient(
            defaultAlgod.Address + '/',
            defaultAlgod.Token,
            defaultAlgod.Headers
            );
        var response = await client.Api.GetStatusAsync();
        Debug.Log($"Last Round: {response.LastRound}");
    });

    protected override async UniTask SetUpAsync()
    {
        await base.SetUpAsync();
    }

    protected override async UniTask TearDownAsync()
    {
        await base.TearDownAsync();
    }
}

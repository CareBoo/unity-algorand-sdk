using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class DotnetAlgodClientTest : AlgodClientTestFixture
{
    [Test]
    public void InstantiatingClientShouldThrowNoErrors()
    {
        var defaultAlgod = AlgoApiClientSettings.DefaultAlgod;
        var api = (Algorand.Algod.DefaultApi)defaultAlgod;
    }

    [UnityTest]
    public IEnumerator SendSimpleRequestShouldThrowNoErrors() => UniTask.ToCoroutine(async () =>
    {
        var defaultAlgod = AlgoApiClientSettings.DefaultAlgod;
        var api = (Algorand.Algod.DefaultApi)defaultAlgod;
        var response = await api.GetStatusAsync();
        Debug.Log($"Last Round: {response.LastRound}");
    });
}

using System;
using System.Collections;
using System.Linq;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

public class AlgodClientTest
{
    const string SandboxToken = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    const string SandBoxAddress = "http://localhost:4001";

    static readonly AlgodClient client = new AlgodClient(SandBoxAddress, SandboxToken);

    private static async UniTask<Address[]> GetAddresses()
    {
        var genesisResponse = await client.GetGenesisInformation();
        var genesisJson = genesisResponse.GetText();
        var genesisInfo = JsonUtility.FromJson<GenesisInformation>(genesisJson);
        return genesisInfo.alloc
            .Where(a => a.comment.Contains("Wallet"))
            .Select(a => (Address)a.addr)
            .ToArray();
    }

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator SandboxShouldBeHealthy() => UniTask.ToCoroutine(async () =>
    {
        var expected = "null\n";
        var response = await client.GetHealth();
        Assert.AreEqual(expected, response.GetText());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator PlayException() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetAsync("/does_not_exist");
        Assert.AreEqual(UnityWebRequest.Result.ProtocolError, response.Status);
        Debug.Log(response.GetText());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetGenesisInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetGenesisInformation();
        Debug.Log(response.GetText());
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status);
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetMetricsShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetMetrics();
        Debug.Log(response.GetText());
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status);
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetSwaggerSpecShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetSwaggerSpec();
        Debug.Log(response.GetText());
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status);
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetAccountInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var addresses = await GetAddresses();
        foreach (var expected in addresses)
        {
            var response = await client.GetAccountInformation(expected);
            var account = response.Payload;
            var actual = account.Address;
            Debug.Log(response.Raw.GetText());
            Assert.AreEqual(expected, actual);
        }
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetPendingTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetPendingTransactions();
        Debug.Log(response.Raw.GetText());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetBlockShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetBlock(1);
        Debug.Log(response.Raw.GetText());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetResultsFromAllEndpoints() => UniTask.ToCoroutine(async () =>
    {
        var endpoints = new[]
        {
            "/v2/accounts",
            "/v2/applications",
            "/v2/blocks",
            "/v2/transactions",
            "/v2/status",
            "/v2/versions"
        };

        foreach (var endpoint in endpoints)
        {
            var response = await client.GetAsync(endpoint);
            Debug.Log(response.GetText());
        }
    });

    [Serializable]
    public struct Data
    {
        public int camelcase_value;
    }

    [Test]
    public void CheckUnityJsonFormat()
    {
        Debug.Log(JsonUtility.ToJson(new Data { camelcase_value = 3 }));
    }
}

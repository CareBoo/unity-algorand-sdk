using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.Networking;
using UnityEngine.TestTools;

public class AlgodClientTest
{
    const string SandboxToken = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    const string SandBoxAddress = "http://localhost:4001";

    static readonly AlgodClient client = new AlgodClient(SandBoxAddress, SandboxToken);

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
        UnityEngine.Debug.Log(response.GetText());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetGenesisInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetGenesisInformation();
        UnityEngine.Debug.Log(response.GetText());
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status);
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetMetricsShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetMetrics();
        UnityEngine.Debug.Log(response.GetText());
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status);
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetSwaggerSpecShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetSwaggerSpec();
        UnityEngine.Debug.Log(response.GetText());
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status);
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetAccountInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var addresses = new Address[] {
            "HF4JGMDSCSXWVVMGQXO7FKGQ2PEBDTG75L6J5Q7WCKEOP2G46U3LVKX66A",
            "SPR2DKU76E2C6JA3RTNUAZJQD7NLR2NMBIJFTNEQCIS5K6ETVOPUCW5UZI",
            "7OLOYTOSXAF5M5X25DLZ56MC3FNFLIBKJOLWTDM6UGRTOYHONBY376FSMI",
            "C6S46WCW4T5DOHKTEQBA52YPU6ZNBYU3T476SIIW2YMB2MHLZNPINNNU7U",
        };
        foreach (var expected in addresses)
        {
            var response = await client.GetAccountInformation(expected);
            var account = response.Payload;
            var actual = account.Address;
            Assert.AreEqual(expected, actual);
            UnityEngine.Debug.Log(response.Raw.GetText());
        }
    });
}

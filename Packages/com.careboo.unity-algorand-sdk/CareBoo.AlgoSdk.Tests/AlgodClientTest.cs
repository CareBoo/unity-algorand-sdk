using System.Collections;
using AlgoSdk;
using AlgoSdk.LowLevel;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine.Networking;
using UnityEngine.TestTools;

public class AlgodClientTest
{
    const string SandboxToken = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    const string SandBoxAddress = "http://localhost:4001";

    static readonly AlgodClient client = new AlgodClient(SandBoxAddress, SandboxToken);

    [UnityTest]
    // [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator SandboxShouldBeHealthy() => UniTask.ToCoroutine(async () =>
    {
        var expected = "null\n";
        using var response = await client.GetHealth();
        using var text = response.Data.AsUtf8Text(Allocator.Temp);
        Assert.AreEqual(expected, text.ToString());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator PlayException() => UniTask.ToCoroutine(async () =>
    {
        using var response = await client.GetAsync("/does_not_exist");
        Assert.IsTrue(response.Status == UnityWebRequest.Result.ProtocolError);
        using var text = response.Data.AsUtf8Text(Allocator.Temp);
        UnityEngine.Debug.Log(text.ToString());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetGenesisInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        using var response = await client.GetGenesisInformation();
        using var text = response.Data.AsUtf8Text(Allocator.Temp);
        UnityEngine.Debug.Log(text.ToString());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetMetricsShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        using var response = await client.GetMetrics();
        using var text = response.Data.AsUtf8Text(Allocator.Temp);
        UnityEngine.Debug.Log(text.ToString());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator GetSwaggerSpecShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        using var stream = new NativeStream(1, Allocator.Temp);
        using var response = await client.GetSwaggerSpec();
        using var text = response.Data.AsUtf8Text(Allocator.Temp);
        UnityEngine.Debug.Log(text.ToString());
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
            using var response = await client.GetAccountInformation(expected);
            using var account = response.Payload;
            var actual = account.Address;
            Assert.AreEqual(expected, actual);
            using var text = response.Raw.Data.AsUtf8Text(Allocator.Temp);
            UnityEngine.Debug.Log(text.ToString());
        }
    });
}

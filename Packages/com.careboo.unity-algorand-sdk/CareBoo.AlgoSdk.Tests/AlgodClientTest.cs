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
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator SandboxShouldBeHealthy() => UniTask.ToCoroutine(async () =>
    {
        var expected = "null\n";
        using var response = await client.GetAsync("health");
        using var text = response.Data.AsUtf8Text(Allocator.Temp);
        Assert.AreEqual(expected, text.ToString());
    });

    [UnityTest]
    [ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires certain dependencies to be set up and running.")]
    public IEnumerator PlayException() => UniTask.ToCoroutine(async () =>
    {
        using var response = await client.GetAsync("dne");
        Assert.IsTrue(response.Status == UnityWebRequest.Result.ProtocolError);
    });
}

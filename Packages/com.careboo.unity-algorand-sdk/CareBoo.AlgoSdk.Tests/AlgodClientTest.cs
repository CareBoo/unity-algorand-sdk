using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

public class AlgodClientTest
{
    const string SandboxToken = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    const string SandBoxAddress = "http://localhost:4001";

    static readonly AlgodClient client = new AlgodClient(SandBoxAddress, SandboxToken);

    [UnityTest]
    public IEnumerator SandboxShouldBeHealthy() => UniTask.ToCoroutine(async () =>
    {
        var expected = "null\n";
        using var response = await client.GetAsync("health");
        Assert.AreEqual(expected, response.Message.ToString());
    });

    [UnityTest]
    public IEnumerator PlayException() => UniTask.ToCoroutine(async () =>
    {
        using var response = await client.GetAsync("dne");
        Assert.IsTrue(response.IsError);
        UnityEngine.Debug.Log(response.Error.Message);
    });
}

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
    [Ignore("This is just for playing around...")]
    public IEnumerator SandboxShouldBeHealthy() => UniTask.ToCoroutine(async () =>
    {
        var expected = "null\n";
        var actual = await client.GetTextAsync("health");
        Assert.AreEqual(expected, actual);
    });

    [UnityTest]
    [Ignore("This is just for playing around...")]
    public IEnumerator PlayException() => UniTask.ToCoroutine(async () =>
    {
        var expected = "null\n";
        var actual = await client.GetTextAsync("dne");
        Assert.AreEqual(expected, actual);
    });
}

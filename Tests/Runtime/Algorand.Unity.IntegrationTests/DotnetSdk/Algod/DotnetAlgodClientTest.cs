using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

[TestFixture]
public class DotnetAlgodClientTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator SendSimpleRequestShouldThrowNoErrors() => UniTask.ToCoroutine(async () =>
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Assert.Ignore("HttpClient is not supported in WebGL");
#else
        SynchronizationContext.SetSynchronizationContext(new UniTaskSynchronizationContext());
        var api = AlgoApiClientSettings.DefaultAlgod.ToDefaultApi();
        var response = await api.GetStatusAsync();
        Assert.Greater(response.LastRound, 0);
#endif
    });
}
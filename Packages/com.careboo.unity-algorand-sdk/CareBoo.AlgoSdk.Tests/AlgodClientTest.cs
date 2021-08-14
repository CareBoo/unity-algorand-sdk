using System.Collections;
using System.Collections.Generic;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using UnityEngine.TestTools;

public class AlgodClientTest
{
    [UnityTest]
    public IEnumerator SendRequestToGoogle() => UniTask.ToCoroutine(async () =>
    {
        var client = new AlgodClient();
        var response = await client.GetAsync("https://www.google.com");
        UnityEngine.Debug.Log(response);
    });
}

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
        await UniTask.Delay(10);
    });
}

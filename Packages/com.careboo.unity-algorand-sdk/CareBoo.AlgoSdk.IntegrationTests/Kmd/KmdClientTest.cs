using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

public class KmdClientTest : KmdClientTestFixture
{
    [UnityTest]
    public IEnumerator GetSwaggerSpecShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await kmd.GetSwaggerSpec();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });
}

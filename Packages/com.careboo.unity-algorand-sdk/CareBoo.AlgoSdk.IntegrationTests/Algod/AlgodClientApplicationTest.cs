using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine.TestTools;

public class AlgodClientApplicationTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator CreatingConfiguringThenDeletingAppShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        using var keyPair = AccountPrivateKey.ToKeyPair();
        var (_, txnParams) = await algod.GetSuggestedParams();
    });
}

using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using UnityEngine.TestTools;

public class AlgodClientApplicationTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator CreatingThenConfiguringAppShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        using var keyPair = AccountPrivateKey.ToKeyPair();
        var (_, compileResult) = await algod.TealCompile(TealCodeCases.SmartContract.ApprovalSrc);
        var (_, txnParams) = await algod.GetSuggestedParams();
        var createTxn = Transaction.AppCreate(
            keyPair.PublicKey,
            txnParams,
            OnCompletion.NoOp
        );
        var progBytes = System.Convert.FromBase64String(compileResult.CompiledBytesBase64);
        createTxn.ApprovalProgram = progBytes;
        createTxn.ClearStateProgram = progBytes;
        var (createError, txid) = await algod.SendTransaction(createTxn.Sign(keyPair.SecretKey));
        AssertOkay(createError);
        await WaitForTransaction(txid);
    });
}

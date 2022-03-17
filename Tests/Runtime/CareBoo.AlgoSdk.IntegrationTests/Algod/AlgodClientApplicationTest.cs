using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

public class AlgodClientApplicationTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator CreatingThenDeletingAppShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var (_, compileResult) = await AlgoApiClientSettings.Algod.TealCompile(TealCodeCases.SmartContract.ApprovalSrc);
        var approval = System.Convert.FromBase64String(compileResult.CompiledBytesBase64);
        (_, compileResult) = await AlgoApiClientSettings.Algod.TealCompile(TealCodeCases.SmartContract.ClearStateSrc);
        var clearstate = System.Convert.FromBase64String(compileResult.CompiledBytesBase64);
        var (_, txnParams) = await AlgoApiClientSettings.Algod.GetSuggestedParams();
        var createTxn = Transaction.AppCreate(
            PublicKey,
            txnParams,
            approvalProgram: approval,
            clearStateProgram: clearstate
        );
        var signedCreateTxn = await Sign(createTxn);
        var (createError, txid) = await AlgoApiClientSettings.Algod.SendTransaction(signedCreateTxn);
        AssertOkay(createError);
        var created = await WaitForTransaction(txid);
        var deleteTxn = Transaction.AppDelete(
            PublicKey,
            txnParams,
            created.ApplicationIndex
        );
        var signedDeleteTxn = await Sign(deleteTxn);
        var (deleteError, deleteId) = await AlgoApiClientSettings.Algod.SendTransaction(signedDeleteTxn);
        AssertOkay(deleteError);
        var deleted = await WaitForTransaction(deleteId);
    });

    [UnityTest]
    public IEnumerator DeletingAllAppsForAccountShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var (error, accountInfo) = await AlgoApiClientSettings.Algod.GetAccountInformation(PublicKey);
        AssertOkay(error);
        Assume.That(accountInfo.CreatedApplications != null, "account has no transactions to delete");
        var (_, txnParams) = await AlgoApiClientSettings.Algod.GetSuggestedParams();
        foreach (var app in accountInfo.CreatedApplications)
        {
            var deleteTxn = Transaction.AppDelete(
                PublicKey,
                txnParams,
                app.Id
            );
            var signedDeleteTxn = await Sign(deleteTxn);
            var (deleteError, deleteId) = await AlgoApiClientSettings.Algod.SendTransaction(signedDeleteTxn);
            AssertOkay(deleteError);
        }
    });
}

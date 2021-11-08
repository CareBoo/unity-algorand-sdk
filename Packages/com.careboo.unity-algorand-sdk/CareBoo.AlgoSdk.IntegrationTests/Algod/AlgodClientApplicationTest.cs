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
        using var keyPair = AccountPrivateKey.ToKeyPair();
        var (_, compileResult) = await AlgoApiClientSettings.Algod.TealCompile(TealCodeCases.SmartContract.ApprovalSrc);
        var approval = System.Convert.FromBase64String(compileResult.CompiledBytesBase64);
        (_, compileResult) = await AlgoApiClientSettings.Algod.TealCompile(TealCodeCases.SmartContract.ClearStateSrc);
        var clearstate = System.Convert.FromBase64String(compileResult.CompiledBytesBase64);
        var (_, txnParams) = await AlgoApiClientSettings.Algod.GetSuggestedParams();
        var createTxn = Transaction.AppCreate(
            keyPair.PublicKey,
            txnParams,
            approvalProgram: approval,
            clearStateProgram: clearstate
        ).Sign(keyPair.SecretKey);
        var (createError, txid) = await AlgoApiClientSettings.Algod.SendTransaction(createTxn);
        AssertOkay(createError);
        var created = await WaitForTransaction(txid);
        var deleteTxn = Transaction.AppDelete(
            keyPair.PublicKey,
            txnParams,
            created.ApplicationIndex
        ).Sign(keyPair.SecretKey);
        var (deleteError, deleteId) = await AlgoApiClientSettings.Algod.SendTransaction(deleteTxn);
        AssertOkay(deleteError);
        var deleted = await WaitForTransaction(deleteId);
    });

    [UnityTest]
    public IEnumerator DeletingAllAppsForAccountShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        using var keyPair = AccountPrivateKey.ToKeyPair();
        var (error, accountInfo) = await AlgoApiClientSettings.Algod.GetAccountInformation(keyPair.PublicKey);
        AssertOkay(error);
        Assume.That(accountInfo.CreatedApplications != null, "account has no transactions to delete");
        var (_, txnParams) = await AlgoApiClientSettings.Algod.GetSuggestedParams();
        foreach (var app in accountInfo.CreatedApplications)
        {
            var deleteTxn = Transaction.AppDelete(
                keyPair.PublicKey,
                txnParams,
                app.Id
            ).Sign(keyPair.SecretKey);
            var (deleteError, deleteId) = await AlgoApiClientSettings.Algod.SendTransaction(deleteTxn);
            AssertOkay(deleteError);
        }
    });
}

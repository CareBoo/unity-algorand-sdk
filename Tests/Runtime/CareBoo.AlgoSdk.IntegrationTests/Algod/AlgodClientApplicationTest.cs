using System.Collections;
using System.Collections.Generic;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine.TestTools;

public class AlgodClientApplicationTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator CreatingThenDeletingAppShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var appId = await CreateSmartContractApp();
        await DeleteApp(appId);
    });

    [UnityTest]
    public IEnumerator DeletingAllAppsForAccountShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var (error, accountInfoResponse) = await AlgoApiClientSettings.Algod.AccountInformation(PublicKey);
        AssertOkay(error);
        var accountInfo = accountInfoResponse.WrappedValue;
        Assume.That(accountInfo.CreatedApps != null, "account has no transactions to delete");
        var (_, txnParams) = await AlgoApiClientSettings.Algod.TransactionParams();
        foreach (var app in accountInfo.CreatedApps)
        {
            var deleteTxn = Transaction.AppDelete(
                PublicKey,
                txnParams,
                app.Id
            );
            var signedDeleteTxn = await Sign(deleteTxn);
            var (deleteError, deleteId) = await AlgoApiClientSettings.Algod.RawTransaction(signedDeleteTxn);
            AssertOkay(deleteError);
        }
    });

    [UnityTest]
    public IEnumerator CallingAppWithAppArgumenmtsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var appId = await CreateSmartContractApp();
        var (_, txnParams) = await AlgoApiClientSettings.Algod.TransactionParams();
        var appArguments = new List<CompiledTeal>();
        ulong someArg = 27L;
        using var someArgBytes = someArg.ToBytesBigEndian(Allocator.Persistent);
        appArguments.Add(someArgBytes.ToArray());
        var txn = Transaction.AppCall(PublicKey, txnParams, appId, appArguments: appArguments.ToArray());
        var signedTxn = await Sign(txn);
        var (callErr, callId) = await AlgoApiClientSettings.Algod.RawTransaction(signedTxn);
        AssertOkay(callErr);
        await WaitForTransaction(callId.TxId);
        await DeleteApp(appId);
    });

    protected async UniTask<ulong> CreateSmartContractApp()
    {
        var (_, compileResult) = await AlgoApiClientSettings.Algod.TealCompile(TealCodeCases.SmartContract.ApprovalBytes);
        var approval = System.Convert.FromBase64String(compileResult.Result);
        (_, compileResult) = await AlgoApiClientSettings.Algod.TealCompile(TealCodeCases.SmartContract.ClearStateBytes);
        var clearstate = System.Convert.FromBase64String(compileResult.Result);
        var (_, txnParams) = await AlgoApiClientSettings.Algod.TransactionParams();
        var createTxn = Transaction.AppCreate(
            PublicKey,
            txnParams,
            approvalProgram: approval,
            clearStateProgram: clearstate,
            globalStateSchema: new StateSchema { NumUints = 4, NumByteSlices = 3 },
            localStateSchema: new StateSchema { NumUints = 2, NumByteSlices = 7 },
            extraProgramPages: 3
        );
        var signedCreateTxn = await Sign(createTxn);
        var (createError, txid) = await AlgoApiClientSettings.Algod.RawTransaction(signedCreateTxn);
        AssertOkay(createError);
        var created = await WaitForTransaction(txid.TxId);
        return created.ApplicationIndex;
    }

    protected async UniTask DeleteApp(ulong appId)
    {
        var (_, txnParams) = await AlgoApiClientSettings.Algod.TransactionParams();
        var deleteTxn = Transaction.AppDelete(
            PublicKey,
            txnParams,
            appId
        );
        var signedDeleteTxn = await Sign(deleteTxn);
        var (deleteError, deleteId) = await AlgoApiClientSettings.Algod.RawTransaction(signedDeleteTxn);
        AssertOkay(deleteError);
        await WaitForTransaction(deleteId.TxId);
    }
}

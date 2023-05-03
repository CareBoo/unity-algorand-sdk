using System;
using System.Collections;
using System.Collections.Generic;
using Algorand.Unity;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class AlgodClientApplicationTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator CreatingThenDeletingAppShouldReturnOkay()
    {
        return UniTask.ToCoroutine(async () =>
        {
            var appId = await CreateSmartContractApp();
            await DeleteApp(appId);
        });
    }

    [UnityTest]
    public IEnumerator CallingAppWithAppArgumentsShouldReturnOkay()
    {
        return UniTask.ToCoroutine(async () =>
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
    }

    [UnityTest]
    public IEnumerator CallingAppWithBoxAndBoxRefShouldReturnOkay()
    {
        return UniTask.ToCoroutine(async () =>
        {
            AppIndex appId = await CreateSmartContractApp(
                TealCodeCases.BoxApp.ApprovalBytes,
                TealCodeCases.BoxApp.ClearBytes);
            var appAddress = appId.GetAppAddress();
            var (_, txnParams) = await AlgoApiClientSettings.Algod.TransactionParams();
            var fundTxn = Transaction.Payment(PublicKey, txnParams, appAddress, 50000000);
            var signedFund = await Sign(fundTxn);
            var (fundErr, fundResponse) = await AlgoApiClientSettings.Algod.RawTransaction(signedFund);
            AssertOkay(fundErr);

            await AlgoApiClientSettings.Algod.WaitForConfirmation(fundResponse.TxId);

            var boxRef = new BoxRef { Index = 0, Name = "str:name" };
            var boxRefs = new[] { boxRef };
            var appArguments = new CompiledTeal[] { "create", "str:name" };
            var txn = Transaction.AppCall(
                PublicKey,
                txnParams,
                appId,
                appArguments: appArguments,
                boxRefs: boxRefs);
            var signedTxnBytes = await Sign(txn);
            var (callErr, callId) = await AlgoApiClientSettings.Algod.RawTransaction(signedTxnBytes);
            AssertOkay(callErr);
            await AlgoApiClientSettings.Algod.WaitForConfirmation(callId.TxId);
            var nameBytes = boxRef.NameBytes;
            var nameB64 = Convert.ToBase64String(nameBytes);
            var sentRequest = AlgoApiClientSettings.Algod.GetApplicationBoxByName(appId, $"b64:{nameB64}");
            var (boxesResponseErr, boxResponse) = await sentRequest;
            AssertOkay(boxesResponseErr);
            var expectedBoxContents = Convert.FromBase64String("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Assert.AreEqual(expectedBoxContents, boxResponse.WrappedValue.Value);

            appArguments = new CompiledTeal[] { "set", "str:name", "value" };
            txn = Transaction.AppCall(PublicKey, txnParams, appId, appArguments: appArguments, boxRefs: boxRefs);
            signedTxnBytes = await Sign(txn);
            (callErr, callId) = await AlgoApiClientSettings.Algod.RawTransaction(signedTxnBytes);
            AssertOkay(callErr);
            await AlgoApiClientSettings.Algod.WaitForConfirmation(callId.TxId);
            sentRequest = AlgoApiClientSettings.Algod.GetApplicationBoxByName(appId, $"b64:{nameB64}");
            (boxesResponseErr, boxResponse) = await sentRequest;
            AssertOkay(boxesResponseErr);
            expectedBoxContents = Convert.FromBase64String("dmFsdWUAAAAAAAAAAAAAAAAAAAAAAAAA");
            Debug.Log(Convert.ToBase64String(boxResponse.WrappedValue.Value));
            Assert.AreEqual(expectedBoxContents, boxResponse.WrappedValue.Value);
        });
    }

    protected async UniTask<ulong> CreateSmartContractApp()
    {
        return await CreateSmartContractApp(
            TealCodeCases.SmartContract.ApprovalBytes,
            TealCodeCases.SmartContract.ClearStateBytes,
            new StateSchema { NumUints = 4, NumByteSlices = 3 },
            new StateSchema { NumUints = 2, NumByteSlices = 7 },
            3);
    }

    protected async UniTask<ulong> CreateSmartContractApp(
        byte[] approvalBytes,
        byte[] clearBytes,
        StateSchema globalStateSchema = default,
        StateSchema localStateSchema = default,
        ulong extraProgramPages = default)
    {
        var (_, compileResult) = await AlgoApiClientSettings.Algod.TealCompile(approvalBytes);
        var approval = Convert.FromBase64String(compileResult.Result);
        var clearState = Array.Empty<byte>();
        if (clearBytes != null)
        {
            (_, compileResult) = await AlgoApiClientSettings.Algod.TealCompile(clearBytes);
            clearState = Convert.FromBase64String(compileResult.Result);
        }

        var (_, txnParams) = await AlgoApiClientSettings.Algod.TransactionParams();
        var createTxn = Transaction.AppCreate(
            PublicKey,
            txnParams,
            approval,
            clearState,
            globalStateSchema,
            localStateSchema,
            extraProgramPages
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
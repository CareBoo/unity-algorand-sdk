using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class AlgodClientTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator GetGenesisInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetGenesisInformation();
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetMetricsShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetMetrics();
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetSwaggerSpecShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetSwaggerSpec();
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetAccountInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var addresses = await GetAddresses();
        foreach (var expected in addresses)
        {
            var response = await algod.GetAccountInformation(expected);
            var account = response.Payload;
            var actual = account.Address;
            Assert.AreEqual(expected, actual);
        }
    });

    [UnityTest]
    public IEnumerator GetPendingTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txId = await MakePaymentTransaction(100_000);
        var response = await algod.GetPendingTransactions();
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetBlockShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        TransactionIdResponse txId = await MakePaymentTransaction(100000);
        var pendingTxn = new PendingTransaction();
        while (pendingTxn.ConfirmedRound <= 0)
        {
            await UniTask.Delay(100);
            var pendingResponse = await algod.GetPendingTransaction(txId);
            pendingTxn = pendingResponse.Payload;
        }
        var round = pendingTxn.ConfirmedRound;
        var blockResponse = await algod.GetBlock(round);
        Debug.Log(blockResponse.Raw.GetText());
        AssertResponseSuccess(blockResponse);
    });

    [UnityTest]
    [Ignore("Register Participation keys isn't supported yet in the algod sandbox... :(")]
    public IEnumerator RegisterParticipationKeysShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var addresses = await GetAddresses();
        var response = await algod.RegisterParticipationKeys(addresses[0]);
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetCurrentStatusShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetCurrentStatus();
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetStatusAfterWaitingForRoundShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetStatusAfterWaitingForRound(0);
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetVersionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetVersions();
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetTransactionParamsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetSuggestedParams();
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetMerkleProofShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        TransactionIdResponse txId = await MakePaymentTransaction(100000);
        var pendingTxn = new PendingTransaction();
        while (pendingTxn.ConfirmedRound <= 0)
        {
            await UniTask.Delay(500);
            var pendingResponse = await algod.GetPendingTransaction(txId);
            pendingTxn = pendingResponse.Payload;
        }
        var round = pendingTxn.ConfirmedRound;
        var response = await algod.GetMerkleProof(round, txId);
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator TransferFundsShouldReturnTransactionId() => UniTask.ToCoroutine(async () =>
    {
        var txId = await MakePaymentTransaction(100000);
        var pendingResponse = await algod.GetPendingTransaction(txId);
        AssertResponseSuccess(pendingResponse);
        Debug.Log(pendingResponse.Raw.GetText());
    });

    [UnityTest]
    public IEnumerator TealCompileShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.TealCompile(TealCodeCases.AtomicSwap.Src);
        AssertResponseSuccess(response);
        Assert.AreEqual(TealCodeCases.AtomicSwap.CompiledResult, response.Payload.CompiledBytesBase64);
        Assert.AreEqual(TealCodeCases.AtomicSwap.CompiledHash, response.Payload.Hash.ToString());
    });
}

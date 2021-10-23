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
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetMetricsShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetMetrics();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetSwaggerSpecShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetSwaggerSpec();
        AssertOkay(response.Error);
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
        AssertOkay(response.Error);
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
        AssertOkay(blockResponse.Error);
    });

    [UnityTest]
    [Ignore("Register Participation keys isn't supported yet in the algod sandbox... :(")]
    public IEnumerator RegisterParticipationKeysShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var addresses = await GetAddresses();
        var response = await algod.RegisterParticipationKeys(addresses[0].ToString());
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetCurrentStatusShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetCurrentStatus();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetStatusAfterWaitingForRoundShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetStatusAfterWaitingForRound(0);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetVersionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetVersions();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetTransactionParamsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetSuggestedParams();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetMerkleProofShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        TransactionIdResponse txid = await MakePaymentTransaction(100000);
        var pendingTxn = await WaitForTransaction(txid);
        var response = await algod.GetMerkleProof(pendingTxn.ConfirmedRound, txid);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator TransferFundsShouldReturnTransactionId() => UniTask.ToCoroutine(async () =>
    {
        var txId = await MakePaymentTransaction(100000);
        var pendingResponse = await algod.GetPendingTransaction(txId);
        AssertOkay(pendingResponse.Error);
    });

    [UnityTest]
    public IEnumerator TealCompileShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.TealCompile(TealCodeCases.AtomicSwap.Src);
        AssertOkay(response.Error);
        Assert.AreEqual(TealCodeCases.AtomicSwap.CompiledResult, response.Payload.CompiledBytesBase64);
        Assert.AreEqual(TealCodeCases.AtomicSwap.CompiledHash, response.Payload.Hash.ToString());
    });
}

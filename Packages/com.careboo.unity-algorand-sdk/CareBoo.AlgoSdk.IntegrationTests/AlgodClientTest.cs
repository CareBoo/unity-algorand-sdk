using System.Collections;
using System.Linq;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;


[ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires algod service to be running.")]
public class AlgodClientTest : AlgoApiClientTest
{
    static async UniTask<bool> AlgodIsHealthy()
    {
        var expected = "null\n";
        var response = await algod.GetHealth();
        return expected == response.GetText();
    }

    static async UniTask<Address[]> GetAddresses()
    {
        var genesisResponse = await algod.GetGenesisInformation();
        var genesisJson = genesisResponse.GetText();
        var genesisInfo = JsonUtility.FromJson<GenesisInformation>(genesisJson);
        return genesisInfo.alloc
            .Where(a => a.comment.Contains("Wallet"))
            .Select(a => (Address)a.addr)
            .ToArray();
    }

    [UnityTest]
    public IEnumerator PlayException() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var response = await algod.GetAsync("/does_not_exist");
        Assert.AreEqual(UnityWebRequest.Result.ProtocolError, response.Status);
        Debug.Log(response.GetText());
    });

    [UnityTest]
    public IEnumerator GetGenesisInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var response = await algod.GetGenesisInformation();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetMetricsShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var response = await algod.GetMetrics();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetSwaggerSpecShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var response = await algod.GetSwaggerSpec();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetAccountInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var addresses = await GetAddresses();
        foreach (var expected in addresses)
        {
            var response = await algod.GetAccountInformation(expected);
            var account = response.Payload;
            var actual = account.Address;
            Debug.Log(response.Raw.GetText());
            Assert.AreEqual(expected, actual);
        }
    });

    [UnityTest]
    public IEnumerator GetPendingTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        await MakePaymentTransaction(100_000);
        var response = await algod.GetPendingTransactions();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetBlockShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        TransactionIdResponse txId = await MakePaymentTransaction(100000);
        var pendingTxn = new PendingTransaction();
        while (pendingTxn.ConfirmedRound <= 0)
        {
            await UniTask.Delay(500);
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
        if (!await AlgodIsHealthy())
            return;
        var addresses = await GetAddresses();
        var response = await algod.RegisterParticipationKeys(addresses[0]);
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetCurrentStatusShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var response = await algod.GetCurrentStatus();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetStatusAfterWaitingForRoundShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var response = await algod.GetStatusAfterWaitingForRound(0);
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetVersionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var response = await algod.GetVersions();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetTransactionParamsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var response = await algod.GetTransactionParams();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetMerkleProofShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
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
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator TransferFundsShouldReturnTransactionId() => UniTask.ToCoroutine(async () =>
    {
        if (!await AlgodIsHealthy())
            return;
        var txId = await MakePaymentTransaction(100000);
        var pendingResponse = await algod.GetPendingTransaction(txId);
        AssertResponseSuccess(pendingResponse);
        Debug.Log(pendingResponse.Raw.GetText());
    });
}

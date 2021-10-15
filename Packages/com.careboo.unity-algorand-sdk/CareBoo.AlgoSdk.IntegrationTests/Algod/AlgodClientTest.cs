using System.Collections;
using System.Linq;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class AlgodClientTest : AlgoApiClientTestFixture
{
    protected override AlgoServices RequiresServices => AlgoServices.Algod;

    protected override async UniTask SetUpAsync()
    {
        await CheckServices();
    }

    protected override UniTask TearDownAsync() => UniTask.CompletedTask;

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

    [UnityTearDown]
    public IEnumerator WaitForTransactions() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetPendingTransactions();
        while (response.Payload.TotalTransactions > 0)
        {
            await UniTask.Delay(100);
            response = await algod.GetPendingTransactions();
        }
    });

    [UnityTest]
    public IEnumerator GetGenesisInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetGenesisInformation();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetMetricsShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetMetrics();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetSwaggerSpecShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetSwaggerSpec();
        Debug.Log(response.GetText());
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
            Debug.Log(response.Raw.GetText());
            Assert.AreEqual(expected, actual);
        }
    });

    [UnityTest]
    public IEnumerator GetPendingTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txId = await MakePaymentTransaction(100_000);
        var response = await algod.GetPendingTransactions();
        Debug.Log(response.GetText());
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
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetCurrentStatusShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetCurrentStatus();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetStatusAfterWaitingForRoundShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetStatusAfterWaitingForRound(0);
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetVersionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetVersions();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetTransactionParamsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await algod.GetTransactionParams();
        Debug.Log(response.Raw.GetText());
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
        Debug.Log(response.Raw.GetText());
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

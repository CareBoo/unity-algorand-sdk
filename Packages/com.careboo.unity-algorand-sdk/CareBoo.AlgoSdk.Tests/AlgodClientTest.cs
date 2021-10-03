using System.Collections;
using System.Linq;
using System.Text;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;


[ConditionalIgnore(nameof(UnityEngine.Application.isBatchMode), "This test requires algod service to be running.")]
public class AlgodClientTest
{
    const string SandboxToken = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    const string SandBoxAddress = "http://localhost:4001";
    static readonly Mnemonic AccountMnemonic = "earth burst hero frown popular genius occur interest hobby push throw canoe orchard dish shed poem child frequent shop lecture female define state abstract tree";

    static readonly AlgodClient client = new AlgodClient(SandBoxAddress, SandboxToken);

    static async UniTask<Address[]> GetAddresses()
    {
        var genesisResponse = await client.GetGenesisInformation();
        var genesisJson = genesisResponse.GetText();
        var genesisInfo = JsonUtility.FromJson<GenesisInformation>(genesisJson);
        return genesisInfo.alloc
            .Where(a => a.comment.Contains("Wallet"))
            .Select(a => (Address)a.addr)
            .ToArray();
    }

    static void AssertResponseSuccess<T>(AlgoApiResponse<T> response) where T : struct
    {
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Raw.Status, response.Error.Message);
    }

    static void AssertResponseSuccess(AlgoApiResponse response)
    {
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status, response.GetText());
    }

    static async UniTask<TransactionId> MakePaymentTransaction(ulong amt)
    {
        using var keyPair = AccountMnemonic
            .ToPrivateKey()
            .ToKeyPair();
        var transactionParamsResponse = await client.GetTransactionParams();
        AssertResponseSuccess(transactionParamsResponse);
        var transactionParams = transactionParamsResponse.Payload;
        var txn = new Transaction.Payment(
            fee: transactionParams.MinFee,
            firstValidRound: transactionParams.LastRound + 1,
            genesisHash: transactionParams.GenesisHash,
            lastValidRound: transactionParams.LastRound + 1001,
            sender: keyPair.PublicKey,
            receiver: "RDSRVT3X6Y5POLDIN66TSTMUYIBVOMPEOCO4Y2CYACPFKDXZPDCZGVE4PQ",
            amount: amt
        );
        txn.Note = Encoding.UTF8.GetBytes("hello");
        txn.GenesisId = transactionParams.GenesisId;
        SignedTransaction signedTxn = txn.Sign(keyPair.SecretKey);
        var serialized = new NativeList<byte>(Allocator.Temp);
        AlgoApiSerializer.SerializeMessagePack(signedTxn, serialized);
        Debug.Log(System.Convert.ToBase64String(serialized.ToArray()));
        serialized.Dispose();
        var txidResponse = await client.SendTransaction(signedTxn);
        AssertResponseSuccess(txidResponse);
        Debug.Log(txidResponse.Raw.GetText());
        return txidResponse.Payload;
    }

    [UnityTest]
    public IEnumerator SandboxShouldBeHealthy() => UniTask.ToCoroutine(async () =>
    {
        var expected = "null\n";
        var response = await client.GetHealth();
        Assert.AreEqual(expected, response.GetText());
    });

    [UnityTest]
    public IEnumerator PlayException() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetAsync("/does_not_exist");
        Assert.AreEqual(UnityWebRequest.Result.ProtocolError, response.Status);
        Debug.Log(response.GetText());
    });

    [UnityTest]
    public IEnumerator GetGenesisInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetGenesisInformation();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetMetricsShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetMetrics();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetSwaggerSpecShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetSwaggerSpec();
        Debug.Log(response.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetAccountInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var addresses = await GetAddresses();
        foreach (var expected in addresses)
        {
            var response = await client.GetAccountInformation(expected);
            var account = response.Payload;
            var actual = account.Address;
            Debug.Log(response.Raw.GetText());
            Assert.AreEqual(expected, actual);
        }
    });

    [UnityTest]
    public IEnumerator GetPendingTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetPendingTransactions();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetBlockShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        TransactionId txId = await MakePaymentTransaction(100000);
        var pendingTxn = new PendingTransaction();
        while (pendingTxn.ConfirmedRound <= 0)
        {
            await UniTask.Delay(500);
            var pendingResponse = await client.GetPendingTransaction(txId);
            pendingTxn = pendingResponse.Payload;
        }
        var round = pendingTxn.ConfirmedRound;
        var blockResponse = await client.GetBlock(round);
        Debug.Log(blockResponse.Raw.GetText());
        AssertResponseSuccess(blockResponse);
    });

    [UnityTest]
    [Ignore("Register Participation keys isn't supported yet in the algod sandbox... :(")]
    public IEnumerator RegisterParticipationKeysShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var addresses = await GetAddresses();
        var response = await client.RegisterParticipationKeys(addresses[0]);
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetCurrentStatusShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetCurrentStatus();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetStatusAfterWaitingForRoundShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetStatusAfterWaitingForRound(0);
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetVersionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetVersions();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetTransactionParamsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await client.GetTransactionParams();
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator GetMerkleProofShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        TransactionId txId = await MakePaymentTransaction(100000);
        var pendingTxn = new PendingTransaction();
        while (pendingTxn.ConfirmedRound <= 0)
        {
            await UniTask.Delay(500);
            var pendingResponse = await client.GetPendingTransaction(txId);
            pendingTxn = pendingResponse.Payload;
        }
        var round = pendingTxn.ConfirmedRound;
        var response = await client.GetMerkleProof(round, txId);
        Debug.Log(response.Raw.GetText());
        AssertResponseSuccess(response);
    });

    [UnityTest]
    public IEnumerator TransferFundsShouldReturnTransactionId() => UniTask.ToCoroutine(async () =>
    {
        var txId = await MakePaymentTransaction(100000);
        var pendingResponse = await client.GetPendingTransaction(txId);
        AssertResponseSuccess(pendingResponse);
        Debug.Log(pendingResponse.Raw.GetText());
    });
}

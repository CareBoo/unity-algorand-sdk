using System.Text;
using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

[System.Flags]
public enum AlgoServices : byte
{
    Algod = 1,
    Kmd = 2,
    Indexer = 4
}

[TestFixture]
public abstract class AlgoApiClientTestFixture
{
    protected const string SandboxToken = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

    protected static readonly Mnemonic AccountMnemonic = "earth burst hero frown popular genius occur interest hobby push throw canoe orchard dish shed poem child frequent shop lecture female define state abstract tree";

    protected static readonly AlgodClient algod = new AlgodClient("http://localhost:4001", SandboxToken);

    protected static readonly KmdClient kmd = new KmdClient("http://localhost:4002", SandboxToken);

    protected static readonly IndexerClient indexer = new IndexerClient("http://localhost:8980", null);

    protected static void AssertResponseSuccess<T>(AlgoApiResponse<T> response) where T : struct
    {
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status, response.Error.Message);
    }

    protected static void AssertResponseSuccess(AlgoApiResponse response)
    {
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status, response.GetText());
    }

    protected static async UniTask<TransactionIdResponse> MakePaymentTransaction(ulong amt)
    {
        using var keyPair = AccountMnemonic
            .ToPrivateKey()
            .ToKeyPair();
        var transactionParamsResponse = await algod.GetTransactionParams();
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
        var serialized = AlgoApiSerializer.SerializeMessagePack(signedTxn, Allocator.Temp);
        Debug.Log(System.Convert.ToBase64String(serialized.ToArray()));
        var txidResponse = await algod.SendTransaction(signedTxn);
        AssertResponseSuccess(txidResponse);
        Debug.Log(txidResponse.GetText());
        return txidResponse.Payload;
    }

    abstract protected AlgoServices RequiresServices { get; }

    [UnitySetUp]
    public IEnumerator SetUpTest() => UniTask.ToCoroutine(SetUpAsync);

    [UnityTearDown]
    public IEnumerator TearDownTest() => UniTask.ToCoroutine(TearDownAsync);

    protected abstract UniTask SetUpAsync();

    protected abstract UniTask TearDownAsync();

    protected async UniTask CheckServices()
    {
        if (RequiresServices.HasFlag(AlgoServices.Algod))
            await CheckAlgodService();
        if (RequiresServices.HasFlag(AlgoServices.Indexer))
            await CheckIndexerService();
        if (RequiresServices.HasFlag(AlgoServices.Kmd))
            await CheckKmdService();
    }

    async static UniTask CheckAlgodService()
    {
        var healthResponse = await algod.GetHealth();
        if (healthResponse.Status != UnityWebRequest.Result.Success)
            Assert.Ignore($"Ignoring test because of {healthResponse.Status}: {healthResponse.ResponseCode} when trying to check health of algod service");
        var expected = "null\n";
        var actual = healthResponse.GetText();
        if (actual != expected)
            Assert.Ignore($"Ignoring test because algod is unhealthy:\n\"{actual}\"");
    }

    async static UniTask CheckIndexerService()
    {
        var healthResponse = await indexer.GetHealth();
        if (healthResponse.Status != UnityWebRequest.Result.Success)
            Assert.Ignore($"Ignoring test because of {healthResponse.Status}: {healthResponse.ResponseCode} when trying to check health of indexer service");
        var health = healthResponse.Payload;
        if (health.DatabaseAvailable)
            return;

        using var healthJson = AlgoApiSerializer.SerializeJson(health, Allocator.Persistent);
        Assert.Ignore($"Ignoring test because indexer is unhealthy:\n\"{healthJson}\"");
    }

    async static UniTask CheckKmdService()
    {
        var healthResponse = await kmd.Versions();
        if (healthResponse.Status != UnityWebRequest.Result.Success)
            Assert.Ignore($"Ignoring test because of {healthResponse.Status}: {healthResponse.ResponseCode} when trying to check health of kmd service");
    }
}

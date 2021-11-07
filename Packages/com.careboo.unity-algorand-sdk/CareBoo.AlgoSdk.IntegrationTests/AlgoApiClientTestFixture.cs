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
    protected static void AssertOkay(ErrorResponse error)
    {
        Assert.IsFalse(error.IsError, error.Message);
    }

    protected static async UniTask<PendingTransaction> WaitForTransaction(TransactionId txid)
    {
        async UniTask<bool> WaitMs(int ms)
        {
            await UniTask.Delay(ms);
            return true;
        }

        PendingTransaction pending = default;
        ErrorResponse error = default;
        do
        {
            (error, pending) = await AlgoApiClientSettings.Algod.GetPendingTransaction(txid);
            AssertOkay(error);
        }
        while (pending.ConfirmedRound == 0 && await WaitMs(1000));
        return pending;
    }

    protected static readonly PrivateKey AccountPrivateKey = AlgoApiClientSettings.AccountMnemonic.ToPrivateKey();

    protected static async UniTask<TransactionIdResponse> MakePaymentTransaction(ulong amt)
    {
        using var keyPair = AccountPrivateKey
            .ToKeyPair();
        var (error, txnParams) = await AlgoApiClientSettings.Algod.GetSuggestedParams();
        AssertOkay(error);
        var txn = Transaction.Payment(
            sender: keyPair.PublicKey,
            txnParams: txnParams,
            receiver: "RDSRVT3X6Y5POLDIN66TSTMUYIBVOMPEOCO4Y2CYACPFKDXZPDCZGVE4PQ",
            amount: amt
        );
        txn.Note = Encoding.UTF8.GetBytes("hello");
        var signedTxn = txn.Sign(keyPair.SecretKey);
        var serialized = AlgoApiSerializer.SerializeMessagePack(signedTxn, Allocator.Temp);
        Debug.Log(System.Convert.ToBase64String(serialized.ToArray()));
        TransactionIdResponse txidResponse = default;
        (error, txidResponse) = await AlgoApiClientSettings.Algod.SendTransaction(signedTxn);
        AssertOkay(error);
        return txidResponse;
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
        if (string.IsNullOrWhiteSpace(AlgoApiClientSettings.Algod.Address))
            Assert.Ignore("Ignoring test because Algod Service has no Address.");
        var healthResponse = await AlgoApiClientSettings.Algod.GetHealth();
        if (healthResponse.Status != UnityWebRequest.Result.Success)
            Assert.Ignore($"Ignoring test because of {healthResponse.Status}: {healthResponse.ResponseCode} when trying to check health of algod service\nError:\n{healthResponse.Error}");
        var expected = "null\n";
        var actual = healthResponse.GetText();
        if (actual != expected)
            Assert.Ignore($"Ignoring test because algod is unhealthy:\n\"{actual}\"");

        var (err, info) = await AlgoApiClientSettings.Algod.GetAccountInformation(AlgoApiClientSettings.AccountMnemonic.ToPrivateKey().ToAddress());
        if (err)
            Assert.Ignore($"Ignoring test because of error on {nameof(AlgoApiClientSettings.Algod.GetAccountInformation)}:\n{err}");

        if (info.Amount < 10_000)
            Assert.Ignore($"Ignoring test because account has less than the min algo");
    }

    async static UniTask CheckIndexerService()
    {
        if (string.IsNullOrWhiteSpace(AlgoApiClientSettings.Indexer.Address))
            Assert.Ignore("Ignoring test because Indexer Service has no Address.");
        var healthResponse = await AlgoApiClientSettings.Indexer.GetHealth();
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
        if (string.IsNullOrWhiteSpace(AlgoApiClientSettings.Kmd.Address))
            Assert.Ignore("Ignoring test because Kmd Service has no Address.");
        var healthResponse = await AlgoApiClientSettings.Kmd.Versions();
        if (healthResponse.Status != UnityWebRequest.Result.Success)
            Assert.Ignore($"Ignoring test because of {healthResponse.Status}: {healthResponse.ResponseCode} when trying to check health of kmd service");
    }
}

using System.Collections;
using System.Text;
using Algorand.Unity;
using Algorand.Unity.Algod;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.TestTools;

[System.Flags]
public enum AlgoServices : byte
{
    None = 0,
    Algod = 1,
    Kmd = 2,
    Indexer = 4
}

[TestFixture]
public abstract class AlgoApiClientTestFixture
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        CheckServices();
    }

    protected static void AssertOkay(ErrorResponse error)
    {
        Assert.IsFalse(error.IsError, error.Message);
    }

    protected static async UniTask<PendingTransactionResponse> WaitForTransaction(TransactionId txid)
    {
        async UniTask<bool> WaitMs(int ms)
        {
            await UniTask.Delay(ms);
            return true;
        }

        PendingTransactionResponse pending = default;
        ErrorResponse error = default;
        do
        {
            (error, pending) = await AlgoApiClientSettings.Algod.PendingTransactionInformation(txid.ToString());
            AssertOkay(error);
        }
        while (pending.ConfirmedRound == 0 && await WaitMs(1000));
        return pending;
    }

    virtual protected AlgoServices RequiresServices { get; }

    [UnitySetUp]
    public IEnumerator SetUpTest() => UniTask.ToCoroutine(SetUpAsync);

    [UnityTearDown]
    public IEnumerator TearDownTest() => UniTask.ToCoroutine(TearDownAsync);

    protected abstract UniTask SetUpAsync();

    protected abstract UniTask TearDownAsync();

    protected void CheckServices()
    {
        if (RequiresServices.HasFlag(AlgoServices.Algod))
            CheckAlgodService();
        if (RequiresServices.HasFlag(AlgoServices.Indexer))
            CheckIndexerService();
        if (RequiresServices.HasFlag(AlgoServices.Kmd))
            CheckKmdService();
    }

    private static void CheckAlgodService()
    {
        if (string.IsNullOrWhiteSpace(AlgoApiClientSettings.Algod.Address))
            Assert.Ignore("Ignoring test because Algod Service has no Address.");
    }

    private static void CheckIndexerService()
    {
        if (string.IsNullOrWhiteSpace(AlgoApiClientSettings.Indexer.Address))
            Assert.Ignore("Ignoring test because Indexer Service has no Address.");
    }

    private static void CheckKmdService()
    {
        if (string.IsNullOrWhiteSpace(AlgoApiClientSettings.Kmd.Address))
            Assert.Ignore("Ignoring test because Kmd Service has no Address.");
    }
}

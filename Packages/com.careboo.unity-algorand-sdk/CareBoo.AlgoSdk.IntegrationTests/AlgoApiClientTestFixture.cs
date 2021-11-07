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

    protected void CheckServices()
    {
        if (RequiresServices.HasFlag(AlgoServices.Algod))
            CheckAlgodService();
        if (RequiresServices.HasFlag(AlgoServices.Indexer))
            CheckIndexerService();
        if (RequiresServices.HasFlag(AlgoServices.Kmd))
            CheckKmdService();
    }

    static void CheckAlgodService()
    {
        if (string.IsNullOrWhiteSpace(AlgoApiClientSettings.Algod.Address))
            Assert.Ignore("Ignoring test because Algod Service has no Address.");
    }

    static void CheckIndexerService()
    {
        if (string.IsNullOrWhiteSpace(AlgoApiClientSettings.Indexer.Address))
            Assert.Ignore("Ignoring test because Indexer Service has no Address.");
    }

    static void CheckKmdService()
    {
        if (string.IsNullOrWhiteSpace(AlgoApiClientSettings.Kmd.Address))
            Assert.Ignore("Ignoring test because Kmd Service has no Address.");
    }
}

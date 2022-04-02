using System.Collections;
using System.Text;
using AlgoSdk;
using AlgoSdk.Algod;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

[TestFixture]
public class AlgodClientTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator GetGenesisInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Algod.GetGenesis();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetSwaggerSpecShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Algod.SwaggerJSON();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetAccountInformationShouldReturnOk() => UniTask.ToCoroutine(async () =>
    {
        var addresses = await GetAddresses();
        foreach (var expected in addresses)
        {
            var response = await AlgoApiClientSettings.Algod.AccountInformation(expected);
            var accountResponse = response.Payload;
            var actual = accountResponse.WrappedValue.Address;
            Assert.AreEqual(expected, actual);
        }
    });

    [UnityTest]
    public IEnumerator GetPendingTransactionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txId = await MakePaymentTransaction(100_000);
        var response = await AlgoApiClientSettings.Algod.GetPendingTransactions();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetBlockShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        PostTransactionsResponse txIdResponse = await MakePaymentTransaction(100000);
        var pendingTxn = new PendingTransactionResponse();
        while (pendingTxn.ConfirmedRound <= 0)
        {
            await UniTask.Delay(100);
            var pendingResponse = await AlgoApiClientSettings.Algod.PendingTransactionInformation(txIdResponse.TxId);
            pendingTxn = pendingResponse.Payload;
        }
        var round = pendingTxn.ConfirmedRound;
        var blockResponse = await AlgoApiClientSettings.Algod.GetBlock(round);
        AssertOkay(blockResponse.Error);
    });

    [UnityTest]
    public IEnumerator GetCurrentStatusShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Algod.GetStatus();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetVersionsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Algod.GetVersion();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetTransactionParamsShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Algod.TransactionParams();
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator GetMerkleProofShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        PostTransactionsResponse txid = await MakePaymentTransaction(100000);
        var pendingTxn = await WaitForTransaction(txid.TxId);
        var response = await AlgoApiClientSettings.Algod.GetProof(pendingTxn.ConfirmedRound, txid.TxId);
        AssertOkay(response.Error);
    });

    [UnityTest]
    public IEnumerator TransferFundsShouldReturnTransactionId() => UniTask.ToCoroutine(async () =>
    {
        var txId = await MakePaymentTransaction(100000);
        var pendingResponse = await AlgoApiClientSettings.Algod.PendingTransactionInformation(txId.TxId);
        AssertOkay(pendingResponse.Error);
    });

    [UnityTest]
    public IEnumerator TealCompileShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var response = await AlgoApiClientSettings.Algod.TealCompile(Encoding.UTF8.GetBytes(TealCodeCases.AtomicSwap.Src));
        AssertOkay(response.Error);
        Assert.AreEqual(TealCodeCases.AtomicSwap.CompiledResult, response.Payload.Result);
        Assert.AreEqual(TealCodeCases.AtomicSwap.CompiledHash, response.Payload.Hash);
    });

    [UnityTest]
    public IEnumerator SendTransactionGroupShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var (_, txnParams) = await AlgoApiClientSettings.Algod.TransactionParams();
        var receiver = AlgoSdk.Crypto.Random.Bytes<Address>();

        var txn1 = Transaction.Payment(PublicKey, txnParams, receiver, 100_000L);
        var txn2 = Transaction.Payment(PublicKey, txnParams, receiver, 200_000L);
        var groupId = TransactionGroup.Of(txn1, txn2).GetId();
        txn1.Group = groupId;
        txn2.Group = groupId;

        var signed1 = await Sign(txn1);
        var signed2 = await Sign(txn2);

        var combined = new byte[signed1.Length + signed2.Length];
        signed1.CopyTo(combined, 0);
        signed2.CopyTo(combined, signed1.Length);

        var (err, txid) = await AlgoApiClientSettings.Algod.RawTransaction(combined);
        AssertOkay(err);
        var pending = await WaitForTransaction(txid.TxId);
        UnityEngine.Debug.Log($"pending tx count: {pending.InnerTxns?.Length ?? 0}");
    });
}

using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
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

    [UnityTest]
    public IEnumerator SendTransactionGroupShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var (_, txnParams) = await algod.GetSuggestedParams();
        var receiver = AlgoSdk.Crypto.Random.Bytes<Address>();

        using var kp = AccountPrivateKey.ToKeyPair();
        var txn1 = Transaction.Payment(kp.PublicKey, txnParams, receiver, 100_000L);
        var txn2 = Transaction.Payment(kp.PublicKey, txnParams, receiver, 200_000L);
        var groupId = Transaction.GetGroupId(txn1.GetId(), txn2.GetId());
        txn1.Group = groupId;
        txn2.Group = groupId;

        var signed1 = txn1.Sign(kp.SecretKey);
        var signed2 = txn2.Sign(kp.SecretKey);

        var (err, txid) = await algod.SendTransactions(signed1, signed2);
        AssertOkay(err);
        var pending = await WaitForTransaction(txid);
        UnityEngine.Debug.Log($"pending tx count: {pending.InnerTransactions?.Length ?? 0}");
    });
}

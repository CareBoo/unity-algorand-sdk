using System.Text;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class AlgoApiClientTest
{
    protected const string SandboxToken = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

    protected static readonly Mnemonic AccountMnemonic = "earth burst hero frown popular genius occur interest hobby push throw canoe orchard dish shed poem child frequent shop lecture female define state abstract tree";

    protected static readonly AlgodClient algod = new AlgodClient("http://localhost:4001", SandboxToken);

    protected static readonly IndexerClient indexer = new IndexerClient("http://localhost:8980", SandboxToken);

    protected static void AssertResponseSuccess<T>(AlgoApiResponse<T> response) where T : struct
    {
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Raw.Status, response.Error.Message);
    }

    protected static void AssertResponseSuccess(AlgoApiResponse response)
    {
        Assert.AreEqual(UnityWebRequest.Result.Success, response.Status, response.GetText());
    }

    protected static async UniTask<TransactionId> MakePaymentTransaction(ulong amt)
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
        Debug.Log(txidResponse.Raw.GetText());
        return txidResponse.Payload;
    }
}

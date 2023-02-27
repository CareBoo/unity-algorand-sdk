using System.Collections;
using Algorand.Unity;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class AlgodClientAssetTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator CreatingConfiguringThenDeletingNftShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        var txnParams = (await AlgoApiClientSettings.Algod.TransactionParams()).Payload;
        var createAssetParams = new AssetParams
        {
            Total = 1,
            Decimals = 0,
            DefaultFrozen = false,
            UnitName = "UNALGO",
            Name = "Unity Algo Sdk",
            Manager = PublicKey
        };
        var createAssetTxn = Transaction.AssetCreate(PublicKey, txnParams, createAssetParams);
        var bytes = AlgoApiSerializer.SerializeMessagePack(createAssetTxn);
        Debug.Log(System.Convert.ToBase64String(bytes));
        var signedCreateAssetTxn = await Sign(createAssetTxn);
        var createResponse = await AlgoApiClientSettings.Algod.RawTransaction(signedCreateAssetTxn);
        AssertOkay(createResponse.Error);
        var txid = createResponse.Payload.TxId;
        var assetId = (await WaitForTransaction(txid)).AssetIndex;
        var configureAssetParams = new AssetParams
        {
            Freeze = PublicKey,
            Manager = PublicKey,
        };
        var configureAssetTxn = Transaction.AssetConfigure(PublicKey, txnParams, assetId.Value, configureAssetParams);
        var signedConfigureAssetTxn = await Sign(configureAssetTxn);
        var configureResponse = await AlgoApiClientSettings.Algod.RawTransaction(signedConfigureAssetTxn);
        AssertOkay(configureResponse.Error);
        await WaitForTransaction(configureResponse.Payload.TxId);
        var assetInfoResponse = await AlgoApiClientSettings.Algod.GetAssetByID(assetId);
        var deleteAssetTxn = Transaction.AssetDelete(PublicKey, txnParams, assetId.Value);
        var signedDeleteAssetTxn = await Sign(deleteAssetTxn);
        var deleteResponse = await AlgoApiClientSettings.Algod.RawTransaction(signedDeleteAssetTxn);
        AssertOkay(deleteResponse.Error);
        await WaitForTransaction(deleteResponse.Payload.TxId);
    });
}

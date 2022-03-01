using System.Collections;
using AlgoSdk;
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
        using var keyPair = AccountPrivateKey.ToKeyPair();
        var txnParams = (await AlgoApiClientSettings.Algod.GetSuggestedParams()).Payload;
        var createAssetParams = new AssetParams
        {
            Total = 1,
            Decimals = 0,
            DefaultFrozen = false,
            UnitName = "UNALGO",
            Name = "Unity Algo Sdk",
            Manager = keyPair.PublicKey
        };
        var createAssetTxn = Transaction
            .AssetCreate(keyPair.PublicKey, txnParams, createAssetParams)
            .Sign(keyPair.SecretKey);
        var bytes = AlgoApiSerializer.SerializeMessagePack(createAssetTxn);
        Debug.Log(System.Convert.ToBase64String(bytes));
        var createResponse = await AlgoApiClientSettings.Algod.SendTransaction(createAssetTxn);
        AssertOkay(createResponse.Error);
        var txid = createResponse.Payload.TxId;
        var assetId = (await WaitForTransaction(txid)).AssetIndex;
        var configureAssetParams = new AssetParams
        {
            Freeze = keyPair.PublicKey,
            Manager = keyPair.PublicKey,
        };
        var configureAssetTxn = Transaction
            .AssetConfigure(keyPair.PublicKey, txnParams, assetId, configureAssetParams)
            .Sign(keyPair.SecretKey);
        var configureResponse = await AlgoApiClientSettings.Algod.SendTransaction(configureAssetTxn);
        AssertOkay(configureResponse.Error);
        await WaitForTransaction(configureResponse.Payload.TxId);
        var assetInfoResponse = await AlgoApiClientSettings.Algod.GetAsset(assetId);
        var deleteAssetTxn = Transaction
            .AssetDelete(keyPair.PublicKey, txnParams, assetId)
            .Sign(keyPair.SecretKey);
        var deleteResponse = await AlgoApiClientSettings.Algod.SendTransaction(deleteAssetTxn);
        AssertOkay(deleteResponse.Error);
        await WaitForTransaction(deleteResponse.Payload.TxId);
    });
}

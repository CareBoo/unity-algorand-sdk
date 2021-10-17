using System.Collections;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class AlgodClientAssetsTest : AlgodClientTestFixture
{
    [UnityTest]
    public IEnumerator CreatingConfiguringThenDeletingNftShouldReturnOkay() => UniTask.ToCoroutine(async () =>
    {
        using var keyPair = AccountPrivateKey.ToKeyPair();
        var txnParams = (await algod.GetSuggestedParams()).Payload;
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
        var createResponse = await algod.SendTransaction(createAssetTxn);
        AssertResponseSuccess(createResponse);
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
        var configureResponse = await algod.SendTransaction(configureAssetTxn);
        AssertResponseSuccess(configureResponse);
        await WaitForTransaction(configureResponse.Payload.TxId);
        var assetInfoResponse = await algod.GetAsset(assetId);
        var deleteAssetTxn = Transaction
            .AssetDelete(keyPair.PublicKey, txnParams, assetId)
            .Sign(keyPair.SecretKey);
        var deleteResponse = await algod.SendTransaction(deleteAssetTxn);
        AssertResponseSuccess(deleteResponse);
        await WaitForTransaction(deleteResponse.Payload.TxId);
    });
}

using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Algorand.Unity.Editor
{
    public class AssetCreateWindow : EditorWindow
    {
        public AlgorandStandardAsset asset;

        public AccountAsset creatorAccount;

        public AlgodClientAsset algod;

        public static void Show(AlgorandStandardAsset asset)
        {
            var window = GetWindow<AssetCreateWindow>("Create ASA");
            window.asset = asset;
        }

        private void CreateGUI()
        {
            var root = rootVisualElement;

            var assetField = new PropertyField { bindingPath = nameof(asset) };
            assetField.SetEnabled(false);
            root.Add(assetField);
            root.Add(new PropertyField { bindingPath = nameof(creatorAccount) });
            root.Add(new PropertyField { bindingPath = nameof(algod) });
            root.Add(new Button(CreateAsa) { text = "Create" });

            root.Bind(new SerializedObject(this));
        }

        private void CreateAsa()
        {
            CreateAsaAsync().Forget();
        }

        private async UniTaskVoid CreateAsaAsync()
        {
            if (!asset)
                throw new ArgumentNullException(nameof(asset));
            if (!creatorAccount)
                throw new ArgumentNullException(nameof(creatorAccount));
            if (!algod)
                throw new ArgumentNullException(nameof(algod));

            // check algod health
            var healthResponse = await algod.client.HealthCheck();
            if (healthResponse.Error)
                throw new Exception($"Algod health check failed: {healthResponse.Error}");

            // get txn params
            var (txnParamsErr, txnParams) = await algod.client.TransactionParams();
            if (txnParamsErr)
                throw new Exception(txnParamsErr);

            // construct and sign the transaction
            var txn = Transaction.AssetCreate(
                sender: creatorAccount.Address,
                txnParams: txnParams,
                assetParams: asset.assetParams
            );
            var txns = new AssetConfigTxn[] { txn };
            var signedTxns = await creatorAccount.SignTxnsAsync(txns, TxnIndices.All);

            // send the transaction
            var (submitTxnErr, txnId) = await algod.client.SendTransaction(signedTxns[0]);
            if (submitTxnErr)
                throw new Exception(submitTxnErr);
            Debug.Log($"Submitted txn with id: {txnId.TxId}");

            // wait for confirmation
            var (txnConfirmErr, confirmedTxn) = await algod.client.WaitForConfirmation(txnId.TxId);
            if (txnConfirmErr)
                throw new Exception(txnConfirmErr);

            // Apply index and network to the ASA
            var serializedObject = new SerializedObject(asset);
            serializedObject.Update();
            asset.index = confirmedTxn.AssetIndex.Value;
            asset.network = algod.network;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            Debug.Log("Asset Created!");

            // close the window now that the asset is created.
            Close();
        }
    }
}

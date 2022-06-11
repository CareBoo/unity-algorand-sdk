using System;
using AlgoSdk;
using AlgoSdk.WalletConnect;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

public class WalletConnectManager : MonoBehaviour
{
    public ClientMeta DappMeta;

    public string BridgeUrl;

    HandshakeUrl handshake;

    Texture2D qrCode;

    bool shouldLaunchApp;

    AppEntry launchedApp;

    MicroAlgos currentBalance;

    WalletConnectAccount account;

    TransactionStatus txnStatus;

    AlgodClient algod = new AlgodClient("https://node.testnet.algoexplorerapi.io");

    IndexerClient indexer = new IndexerClient("https://algoindexer.testnet.algoexplorerapi.io");

    void Start()
    {
        StartWalletConnect().Forget();
        PollForBalance().Forget();
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 32;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.skin.button.fontSize = 48;
        GUI.skin.button.alignment = TextAnchor.MiddleCenter;
        GUI.skin.textField.fontSize = 32;
        GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height), new GUIStyle(GUI.skin.box) { normal = new GUIStyleState() { background = GUI.skin.button.normal.background } });
        GUILayout.FlexibleSpace();
        var status = account?.ConnectionStatus ?? SessionStatus.None;
        GUILayout.Label($"WalletConnect Connection Status: {status}");
        GUILayout.Space(20);
        if (status == SessionStatus.RequestingWalletConnection)
        {
            var supportedWallets = WalletRegistry.SupportedWalletsForCurrentPlatform;
            if (shouldLaunchApp && supportedWallets.Length > 0)
            {
                foreach (var wallet in supportedWallets)
                {
                    if (GUILayout.Button($"Connect to {wallet.Name}"))
                    {
                        launchedApp = wallet;
                        wallet.LaunchForConnect(handshake);
                    }
                    GUILayout.Space(5);
                    if (GUILayout.Button("Show QR Code"))
                    {
                        shouldLaunchApp = false;
                    }
                }
            }
            else
            {
                GUILayout.Button(qrCode, new GUIStyle() { alignment = TextAnchor.MiddleCenter });
                if (supportedWallets.Length > 0)
                {
                    GUILayout.Space(5);
                    if (GUILayout.Button("Connect With Wallet App"))
                        shouldLaunchApp = true;
                }
            }
        }

        if (status == SessionStatus.WalletConnected)
        {
            GUILayout.Label($"Connected Account: {account.Address}");
            GUILayout.Space(5);
            var balanceAlgos = currentBalance / (double)MicroAlgos.PerAlgo;
            GUILayout.Label($"Balance: {balanceAlgos:F} Algos");
            switch (txnStatus)
            {
                case TransactionStatus.None:
                    if (GUILayout.Button("Send Test Transaction"))
                    {
                        TestTransaction().Forget();
                        txnStatus = TransactionStatus.RequestingSignature;
                    }
                    break;
                default:
                    GUILayout.Label($"Transaction Status: {txnStatus}");
                    break;
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndArea();
    }

    async UniTaskVoid StartWalletConnect()
    {
        account = new WalletConnectAccount { DappMeta = DappMeta, BridgeUrl = BridgeUrl };
        await account.BeginSession();
        handshake = account.RequestWalletConnection();
        qrCode = handshake.ToQrCodeTexture();
        await account.WaitForWalletApproval();
        Debug.Log($"Connected account:\n{AlgoApiSerializer.SerializeJson(account.Address)}");
    }

    async UniTaskVoid PollForBalance()
    {
        while (true)
        {
            var status = account?.ConnectionStatus ?? SessionStatus.None;
            if (status == SessionStatus.WalletConnected)
            {
                var (err, response) = await indexer.LookupAccountByID(account.Address);
                if (err) Debug.LogError(err);
                else
                {
                    currentBalance = response.Account.Amount;
                }
            }
            await UniTask.Delay(4_000);
            await UniTask.Yield();
        }
    }

    async UniTaskVoid TestTransaction()
    {
        var (txnParamsErr, txnParams) = await algod.TransactionParams();
        if (txnParamsErr)
        {
            Debug.LogError((string)txnParamsErr);
            txnStatus = TransactionStatus.Failed;
            return;
        }

        var signing = Transaction.Atomic()
            .AddTxn(Transaction.Payment(account.Address, txnParams, account.Address, 0))
            .Build()
            .SignWithAsync(account)
            .FinishSigningAsync()
            ;
        if (shouldLaunchApp && launchedApp != null)
            launchedApp.LaunchForSigning();
        var signedTxns = await signing;
        txnStatus = TransactionStatus.AwaitingConfirmation;
        var (txnSendErr, txid) = await algod.RawTransaction(signedTxns);
        if (txnSendErr)
        {
            txnStatus = TransactionStatus.Failed;
            Debug.LogError(txnSendErr);
            return;
        }

        var (pendingErr, pending) = await algod.PendingTransactionInformation(txid.TxId);
        if (pendingErr)
        {
            txnStatus = TransactionStatus.Failed;
            Debug.LogError(pendingErr);
            return;
        }
        while (pending.ConfirmedRound <= 0)
        {
            await UniTask.Delay(1000);
            (pendingErr, pending) = await algod.PendingTransactionInformation(txid.TxId);
        }
        txnStatus = TransactionStatus.Confirmed;
    }

    enum TransactionStatus
    {
        None,
        RequestingSignature,
        AwaitingConfirmation,
        Confirmed,
        Failed
    }
}

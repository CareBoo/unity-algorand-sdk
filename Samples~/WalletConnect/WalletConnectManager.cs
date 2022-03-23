using System.Linq;
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

    bool launchApp;

    AlgorandWalletConnectSession session;

    TransactionStatus txnStatus;

    void Start()
    {
        StartWalletConnect().Forget();
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.skin.button.alignment = TextAnchor.MiddleCenter;
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height), new GUIStyle(GUI.skin.box) { normal = new GUIStyleState() { background = GUI.skin.button.normal.background } });
        GUILayout.FlexibleSpace();
        var status = session?.ConnectionStatus ?? AlgorandWalletConnectSession.Status.Unknown;
        GUILayout.Label($"WalletConnect Connection Status: {status}");
        GUILayout.Space(20);
        if (status == AlgorandWalletConnectSession.Status.RequestingConnection)
        {
            if (launchApp && WalletRegistry.SupportedWalletsForCurrentPlatform.Length > 0)
            {
                var wallet = WalletRegistry.SupportedWalletsForCurrentPlatform[0];
                if (GUILayout.Button($"Connect to {wallet.Name}"))
                {
                    WalletRegistry.PeraWallet.LaunchForConnect(handshake);
                }
                GUILayout.Space(5);
                if (GUILayout.Button("Show QR Code"))
                {
                    launchApp = false;
                }
            }
            else
            {
                GUILayout.Button(qrCode, new GUIStyle() { alignment = TextAnchor.MiddleCenter });
                if (WalletRegistry.SupportedWalletsForCurrentPlatform.Length > 0)
                {
                    GUILayout.Space(5);
                    if (GUILayout.Button("Open Wallet App"))
                        launchApp = true;
                }
            }
        }

        if (status == AlgorandWalletConnectSession.Status.Connected)
        {
            GUILayout.Label($"Connected Account: {session.Accounts[0]}");
            GUILayout.Space(5);
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
        session = new AlgorandWalletConnectSession(DappMeta, BridgeUrl);
        handshake = await session.StartConnection();
        qrCode = handshake.ToQrCodeTexture();
        await session.WaitForConnectionApproval();
        Debug.Log($"accounts:\n{AlgoApiSerializer.SerializeJson(session.Accounts)}");
    }

    async UniTaskVoid TestTransaction()
    {
        var algod = new AlgodClient("https://node.testnet.algoexplorerapi.io");
        var (txnParamsErr, txnParams) = await algod.GetSuggestedParams();
        if (txnParamsErr)
        {
            Debug.LogError((string)txnParamsErr);
            txnStatus = TransactionStatus.Failed;
            return;
        }

        var txn = Transaction.Payment(
            session.Accounts[0],
            txnParams,
            session.Accounts[0],
            0
        );

        var walletTxn = new WalletTransaction
        {
            Txn = AlgoApiSerializer.SerializeMessagePack(txn),
            Message = "This is a test"
        };

        var signingTransactions = session.SignTransactions(new[] { walletTxn });
        if (launchApp)
            WalletRegistry.PeraWallet.LaunchForSigning();
        var (err, signedTxns) = await session.SignTransactions(new[] { walletTxn });
        if (err)
        {
            txnStatus = TransactionStatus.Failed;
            Debug.LogError(err);
            return;
        }
        txnStatus = TransactionStatus.AwaitingConfirmation;
        using (var signedTxnData = new NativeArray<byte>(signedTxns[0], Allocator.Temp))
        {
            var signedTxn = AlgoApiSerializer.DeserializeMessagePack<SignedTxn>(signedTxnData);
            Debug.Log($"Got signed transactions:\n{AlgoApiSerializer.SerializeJson(signedTxns)}");
            Debug.Log($"Deserialized signed txn: {AlgoApiSerializer.SerializeJson(signedTxn)}");
        }

        var (txnSendErr, txid) = await algod.SendTransaction(signedTxns[0]);
        if (txnSendErr)
        {
            txnStatus = TransactionStatus.Failed;
            Debug.LogError(txnSendErr);
            return;
        }

        var (pendingErr, pending) = await algod.GetPendingTransaction(txid);
        if (pendingErr)
        {
            txnStatus = TransactionStatus.Failed;
            Debug.LogError(pendingErr);
            return;
        }
        while (pending.ConfirmedRound <= 0)
        {
            await UniTask.Delay(500);
            (pendingErr, pending) = await algod.GetPendingTransaction(txid);
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

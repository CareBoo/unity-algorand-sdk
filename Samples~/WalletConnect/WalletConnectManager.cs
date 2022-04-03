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

    AlgorandWalletConnectSession session;

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
        var status = session?.ConnectionStatus ?? AlgorandWalletConnectSession.Status.Unknown;
        GUILayout.Label($"WalletConnect Connection Status: {status}");
        GUILayout.Space(20);
        if (status == AlgorandWalletConnectSession.Status.RequestingConnection)
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

        if (status == AlgorandWalletConnectSession.Status.Connected)
        {
            GUILayout.Label($"Connected Account: {session.Accounts[0]}");
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
        session = new AlgorandWalletConnectSession(DappMeta, BridgeUrl);
        handshake = await session.StartConnection();
        qrCode = handshake.ToQrCodeTexture();
        await session.WaitForConnectionApproval();
        Debug.Log($"accounts:\n{AlgoApiSerializer.SerializeJson(session.Accounts)}");
    }

    async UniTaskVoid PollForBalance()
    {
        while (true)
        {
            var status = session?.ConnectionStatus ?? AlgorandWalletConnectSession.Status.Unknown;
            if (status == AlgorandWalletConnectSession.Status.Connected)
            {
                var (err, response) = await indexer.LookupAccountByID(session.Accounts[0]);
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

        if (shouldLaunchApp && launchedApp != null)
            launchedApp.LaunchForSigning();
        var signingTransactions = session.SignTransactions(new[] { walletTxn });
        var (err, signedTxns) = await signingTransactions;
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

        var (txnSendErr, txid) = await algod.RawTransaction(signedTxns[0]);
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

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

    string url;

    Texture2D qrCode;

    bool launchApp;

    AlgorandWalletConnectSession session;

    TransactionStatus txnStatus;

    AppEntry[] supportedWalletsForPlatform;

    void Start()
    {
        StartWalletConnect().Forget();
    }

    void OnGUI()
    {
        var status = session?.ConnectionStatus ?? AlgorandWalletConnectSession.Status.Unknown;
        GUILayout.Label($"WalletConnect Connection Status: {status}");
        if (status == AlgorandWalletConnectSession.Status.RequestingConnection)
        {
            if (launchApp && WalletRegistry.SupportedWalletsForCurrentPlatform.Length > 0)
            {
                foreach (var wallet in WalletRegistry.SupportedWalletsForCurrentPlatform)
                {
                    if (GUILayout.Button($"Connect to {wallet.Name}"))
                    {
                        var scheme = UnityEngine.Application.isMobilePlatform
                            ? wallet.Mobile.Native
                            : wallet.Desktop.Native
                            ;
                        UnityEngine.Application.OpenURL(url.Replace("wc:", scheme));
                    }
                    if (GUILayout.Button("Show QR Code"))
                    {
                        launchApp = false;
                    }
                }
            }
            else
            {
                GUILayout.Button(qrCode, GUIStyle.none);
                if (WalletRegistry.SupportedWalletsForCurrentPlatform.Length > 0
                    && GUILayout.Button("Open Wallet App"))
                {
                    launchApp = true;
                }
            }
        }

        if (status == AlgorandWalletConnectSession.Status.Connected)
        {
            GUILayout.Label($"Connected Account: {session.Accounts[0]}");
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
    }

    async UniTaskVoid StartWalletConnect()
    {
        session = new AlgorandWalletConnectSession(DappMeta, BridgeUrl);
        var handshakeUrl = await session.StartConnection();
        url = handshakeUrl.Url;
        qrCode = handshakeUrl.ToQrCodeTexture();
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
            var signedTxn = AlgoApiSerializer.DeserializeMessagePack<SignedTransaction>(signedTxnData);
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

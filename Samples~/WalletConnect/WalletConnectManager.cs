using Algorand.Unity;
using Algorand.Unity.WalletConnect;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

public class WalletConnectManager : MonoBehaviour
{
    public ClientMeta DappMeta;

    public string BridgeUrl;

    private HandshakeUrl handshake;

    private Texture2D qrCode;

    private bool shouldLaunchApp;

    private AppEntry launchedApp;

    private MicroAlgos currentBalance;

    private WalletConnectAccount account;

    private TransactionStatus txnStatus;

    public string AlgoClientURL = @"https://node.testnet.algoexplorerapi.io";

    public string IndexerURL = @"https://algoindexer.testnet.algoexplorerapi.io";

    private AlgodClient algod;

    private IndexerClient indexer;

    [SerializeField] private WalletConnectCanvas walletConnectCanvas;

    private void Start()
    {
        algod = new AlgodClient(@AlgoClientURL);

        indexer = new IndexerClient(IndexerURL);


        StartWalletConnect().Forget();
        PollForBalance().Forget();
    }

    private void Update()
    {
        var status = account?.ConnectionStatus ?? SessionStatus.None;
        var supportedWallets = WalletRegistry.SupportedWalletsForCurrentPlatform;
        switch (status)
        {
            case SessionStatus.RequestingWalletConnection:
                walletConnectCanvas.setConnectionStatus("Requesting Connection");

                if (qrCode != null)
                {
                    walletConnectCanvas.setQRCode(qrCode);
                    qrCode = null;
                }
                if (supportedWallets.Length > 0)
                {
                    if (!shouldLaunchApp)
                    {
                        walletConnectCanvas.qrCodeDisplay.gameObject.SetActive(false);
                        walletConnectCanvas.connectingTOWallet.gameObject.SetActive(true);
                        shouldLaunchApp = true;
                    }
                }
                break;
            case SessionStatus.WalletConnected:
                walletConnectCanvas.connectedAccount.text = $"Connected Account: {account.Address}";
                var balanceAlgos = currentBalance / (double)MicroAlgos.PerAlgo;
                walletConnectCanvas.amount.text = $"Balance: {balanceAlgos:F} Algos";
                switch (txnStatus)
                {
                    case TransactionStatus.None:
                        break;
                    default:
                        walletConnectCanvas.sendTestTransactionButton.gameObject.SetActive(false);
                        walletConnectCanvas.transactionStatus.text = $"Transaction Status: {txnStatus}";
                        break;
                }
                break;
        }

        walletConnectCanvas.setCanvasDisplay(status);
    }

    public void sendTestTransaction()
    {
        TestTransaction().Forget();
        txnStatus = TransactionStatus.RequestingSignature;
    }

    private async UniTaskVoid StartWalletConnect()
    {
        account = new WalletConnectAccount { DappMeta = DappMeta, BridgeUrl = BridgeUrl };
        await account.BeginSession();
        handshake = account.RequestWalletConnection();
        qrCode = handshake.ToQrCodeTexture();

        await account.WaitForWalletApproval();
        Debug.Log($"Connected account:\n{AlgoApiSerializer.SerializeJson(account.Address)}");
    }

    private async UniTaskVoid PollForBalance()
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

    private async UniTaskVoid TestTransaction()
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

    private enum TransactionStatus
    {
        None,
        RequestingSignature,
        AwaitingConfirmation,
        Confirmed,
        Failed
    }
}

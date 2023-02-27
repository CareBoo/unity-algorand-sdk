using Algorand.Unity.WalletConnect;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Algorand.Unity.Samples.WalletConnect
{
    public class WalletConnectManager : MonoBehaviour
    {
        [FormerlySerializedAs("DappMeta")] public ClientMeta dappMeta;

        [FormerlySerializedAs("BridgeUrl")] public string bridgeUrl;

        private HandshakeUrl handshake;

        private Texture2D qrCode;

        private bool shouldLaunchApp;

        private AppEntry launchedApp;

        private MicroAlgos currentBalance;

        private WalletConnectAccount account;

        private TransactionStatus txnStatus;

        [FormerlySerializedAs("algoClientURL")] [FormerlySerializedAs("AlgoClientURL")]
        public string algodClientURL = @"https://node.testnet.algoexplorerapi.io";

        [FormerlySerializedAs("IndexerURL")]
        public string indexerURL = @"https://algoindexer.testnet.algoexplorerapi.io";

        private AlgodClient algod;

        private IndexerClient indexer;

        [SerializeField] private WalletConnectCanvas walletConnectCanvas;

        private void Start()
        {
            algod = new AlgodClient(algodClientURL);
            indexer = new IndexerClient(indexerURL);

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
                    walletConnectCanvas.SetConnectionStatus("Requesting Connection");

                    if (qrCode != null)
                    {
                        walletConnectCanvas.SetQRCode(qrCode);
                        qrCode = null;
                    }

                    if (supportedWallets.Length > 0)
                    {
                        if (!shouldLaunchApp)
                        {
                            walletConnectCanvas.qrCodeDisplay.gameObject.SetActive(false);
                            walletConnectCanvas.connectingToWallet.gameObject.SetActive(true);
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

            walletConnectCanvas.SetCanvasDisplay(status);
        }

        public void sendTestTransaction()
        {
            TestTransaction().Forget();
            txnStatus = TransactionStatus.RequestingSignature;
        }

        private async UniTaskVoid StartWalletConnect()
        {
            account = new WalletConnectAccount { DappMeta = dappMeta, BridgeUrl = bridgeUrl };
            Debug.Log($"Beginning session. Status: {account}");
            await account.BeginSession();
            Debug.Log("Session began. Requesting Wallet Connection");
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
                    if (err)
                    {
                        currentBalance = 0;
                        if (!err.Message.Contains("no accounts found for address"))
                        {
                            Debug.LogError(err);
                        }
                    }
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
}
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Algorand.Unity.Samples.YourFirstTransaction
{
    public class YourFirstTransaction : MonoBehaviour
    {
        private static GUILayoutOption[] AddressSize = { GUILayout.Width(58) };
        private static GUILayoutOption[] MicroAlgosSize = { GUILayout.Width(50) };

        public Vector2 referenceScreenResolution = new Vector2(1280, 720);

        private AlgodClient algod = new AlgodClient("https://node.testnet.algoexplorerapi.io");

        private IndexerClient indexer = new IndexerClient("https://algoindexer.testnet.algoexplorerapi.io");

        private string algodHealth;

        private string indexerHealth;

        private MicroAlgos balance;

        private Account account;

        private string recipient;

        private MicroAlgos payAmount;

        private string txnStatus;

        public void Start()
        {
            CheckAlgodStatus().Forget();
            CheckIndexerStatus().Forget();
            var maxAlgoAmount = 10_000_000_000 * MicroAlgos.PerAlgo;
            Debug.Log(maxAlgoAmount);
        }

        public void OnGUI()
        {
            Vector2 scale;
            scale.x = Screen.width / referenceScreenResolution.x;
            scale.y = Screen.height / referenceScreenResolution.y;
            GUIUtility.ScaleAroundPivot(scale, Vector2.zero);

            var maxAddressSize = GUI.skin.textField.CalcSize(new GUIContent(Address.Empty.ToString()));
            AddressSize[0] = GUILayout.Width(maxAddressSize.x + 16);

            var maxAlgoAmount = 10_000_000_000 * MicroAlgos.PerAlgo;
            var maxMicroAlgoSize = GUI.skin.textField.CalcSize(new GUIContent(maxAlgoAmount.ToString()));
            MicroAlgosSize[0] = GUILayout.Width(maxMicroAlgoSize.x + 16);

            using var areaScope = new GUILayout.AreaScope(
                new Rect(
                    5,
                    5,
                    referenceScreenResolution.x - 10,
                    referenceScreenResolution.y - 10));
            GUILayout.Label($"Algod Status: {algodHealth}");
            GUILayout.Label($"Indexer Status: {indexerHealth}");

            GUILayout.Space(20);

            using (new GUILayout.HorizontalScope())
            {
                var generateAccountButtonText =
                    account.Address == Address.Empty ? "Generate Account" : "Regenerate Account";
                if (GUILayout.Button(generateAccountButtonText))
                    account = Account.GenerateAccount();

                GUILayout.FlexibleSpace();
            }

            GUILayout.Space(5);

            if (account.Address != Address.Empty)
            {
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Label("Account Address:");
                    GUILayout.TextField(account.Address.ToString(), AddressSize);
                    GUILayout.FlexibleSpace();
                }

                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Label("Balance (MicroAlgos):");
                    GUILayout.TextField(balance.Amount.ToString(), MicroAlgosSize);
                    GUILayout.FlexibleSpace();
                }

                GUILayout.Space(5);

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Copy address"))
                    {
                        GUIUtility.systemCopyBuffer = account.Address;
                    }

                    GUILayout.FlexibleSpace();
                }

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Deposit funds into Account"))
                    {
                        Application.OpenURL("https://bank.testnet.algorand.network");
                    }

                    GUILayout.FlexibleSpace();
                }

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Refresh Balance"))
                    {
                        CheckBalance().Forget();
                    }

                    GUILayout.FlexibleSpace();
                }
            }

            if (balance <= 0)
            {
                return;
            }

            GUILayout.Space(20);

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Recipient Address:");
                recipient = GUILayout.TextField(recipient, AddressSize);
                GUILayout.FlexibleSpace();
            }

            GUILayout.Space(5);

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Payment Amount (MicroAlgos):");
                var payAmountText = GUILayout.TextField(payAmount.Amount.ToString(), MicroAlgosSize);
                if (ulong.TryParse(payAmountText, out var amt))
                {
                    payAmount = amt;
                }

                GUILayout.FlexibleSpace();
            }

            GUILayout.Space(5);

            if (txnStatus != "awaiting confirmation..." && account.Address != Address.Empty)
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Send payment to recipient"))
                    {
                        MakePayment().Forget();
                    }

                    GUILayout.FlexibleSpace();
                }
            }

            if (txnStatus != null)
                GUILayout.Label($"Transaction Status: {txnStatus}");
        }

        public async UniTaskVoid CheckAlgodStatus()
        {
            var response = await algod.HealthCheck();
            if (response.Error) algodHealth = response.Error;
            else algodHealth = "Connected";
        }

        public async UniTaskVoid CheckIndexerStatus()
        {
            var response = await indexer.MakeHealthCheck();
            if (response.Error) indexerHealth = response.Error;
            else indexerHealth = "Connected";
        }

        public async UniTaskVoid CheckBalance()
        {
            var (err, resp) = await indexer.LookupAccountByID(account.Address);
            if (err)
            {
                balance = 0;
                if (!err.Message.Contains("no accounts found for address"))
                {
                    Debug.LogError(err);
                }
            }
            else
            {
                balance = resp.Account.Amount;
            }
        }

        public async UniTaskVoid MakePayment()
        {
            txnStatus = "awaiting confirmation...";

            // Get the suggested transaction params
            var (txnParamsError, txnParams) = await algod.TransactionParams();
            if (txnParamsError)
            {
                Debug.LogError(txnParamsError);
                txnStatus = $"error: {txnParamsError}";
                return;
            }

            // Construct and sign the payment transaction
            var paymentTxn = Transaction.Payment(
                sender: account.Address,
                txnParams: txnParams,
                receiver: recipient,
                amount: payAmount
            );
            var signedTxn = account.SignTxn(paymentTxn);

            // Send the transaction
            var (sendTxnError, txid) = await algod.SendTransaction(signedTxn);
            if (sendTxnError)
            {
                Debug.LogError(sendTxnError);
                txnStatus = $"error: {sendTxnError}";
                return;
            }

            // Wait for the transaction to be confirmed
            var (confirmErr, confirmed) = await algod.WaitForConfirmation(txid.TxId);
            if (confirmErr)
            {
                Debug.LogError(confirmErr);
                txnStatus = $"error: {confirmErr}";
                return;
            }

            txnStatus = $"confirmed on round {confirmed.ConfirmedRound.Value}";
        }
    }
}
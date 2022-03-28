using AlgoSdk;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class YourFirstTransaction : MonoBehaviour
{
    AlgodClient algod = new AlgodClient("https://node.testnet.algoexplorerapi.io");

    IndexerClient indexer = new IndexerClient("https://algoindexer.testnet.algoexplorerapi.io");

    string algodHealth;

    string indexerHealth;
    ulong balance;

    Account account;

    string recipient;

    ulong payAmount;

    string txnStatus;

    public void Start()
    {
        CheckAlgodStatus().Forget();
        CheckIndexerStatus().Forget();
    }

    public void OnGUI()
    {
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.skin.button.fontSize = 24;
        GUI.skin.button.alignment = TextAnchor.MiddleCenter;
        GUI.skin.textField.fontSize = 24;
        GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height), new GUIStyle(GUI.skin.box) { normal = new GUIStyleState() { background = GUI.skin.button.normal.background } });
        GUILayout.FlexibleSpace();

        GUILayout.Label($"Algod Status: {algodHealth}");
        GUILayout.Label($"Indexer Status: {indexerHealth}");

        GUILayout.Space(20);

        GUILayout.Label("Account Address:");
        GUILayout.TextField(account.Address.ToString());
        GUILayout.Label($"Balance (MicroAlgos):");
        GUILayout.TextField(balance.ToString());

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Regenerate Account"))
            account = Account.GenerateAccount();
        if (GUILayout.Button("Refresh Balance"))
            CheckBalance().Forget();

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.Label("Recipient Address:");
        recipient = GUILayout.TextField(recipient);

        GUILayout.Space(5);

        GUILayout.Label("Payment Amount (MicroAlgos):");
        if (ulong.TryParse(GUILayout.TextField(payAmount.ToString()), out var amt))
            payAmount = amt;

        GUILayout.Space(5);

        if (txnStatus != "awaiting confirmation..." && GUILayout.Button("Send payment to recipient"))
            MakePayment().Forget();

        if (txnStatus != null)
            GUILayout.Label($"Transaction Status: {txnStatus}");

        GUILayout.FlexibleSpace();
        GUILayout.EndArea();
    }

    public async UniTaskVoid CheckAlgodStatus()
    {
        var response = await algod.GetHealth();
        if (response.Error) algodHealth = response.Error;
        else algodHealth = "Connected";
    }

    public async UniTaskVoid CheckIndexerStatus()
    {
        var response = await indexer.GetHealth();
        if (response.Error) indexerHealth = response.Error;
        else indexerHealth = "Connected";
    }

    public async UniTaskVoid CheckBalance()
    {
        var (err, resp) = await indexer.GetAccount(account.Address);
        if (err)
        {
            balance = 0;
            Debug.LogError(err);
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
        var (txnParamsError, txnParams) = await algod.GetSuggestedParams();
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
        PendingTransaction pending = default;
        ErrorResponse error = default;
        while (pending.ConfirmedRound == 0)
        {
            (error, pending) = await algod.GetPendingTransaction(txid);
            if (error)
            {
                Debug.LogError(error);
                txnStatus = $"error: {error}";
                return;
            }
            await UniTask.Delay(2000);
        }
        txnStatus = $"confirmed on round {pending.ConfirmedRound}";
    }
}

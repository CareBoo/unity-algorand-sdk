using AlgoSdk;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AlgodCheck : MonoBehaviour
{
    AlgodClient algod;

    public void Start()
    {
        algod = new AlgodClient("testnet-algorand.api.purestake.io/ps2", ("X-API-Key", "my-secret-key"));
        CheckAlgodStatus().Forget();
        CheckBalance().Forget();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var privateKey = Mnemonic
                .FromString("find paddle girl crumble hammer usual obvious toy actual obscure decorate lock bag inmate author valve course ship burger denial sibling tissue nominee above remove")
                .ToPrivateKey();
            ulong amount = 100_000;
            Address receiver = "HZ57J3K46JIJXILONBBZOHX6BKPXEM2VVXNRFSUED6DKFD5ZD24PMJ3MVA";
            MakePayment(privateKey, receiver, amount).Forget();
        }
    }

    public async UniTaskVoid CheckAlgodStatus()
    {
        var response = await algod.GetHealth();
        if (response.Error)
        {
            Debug.LogError(response.Error);
        }
        else
        {
            Debug.Log("Connected to algod!");
        }
    }

    public async UniTaskVoid CheckBalance()
    {
        var accountAddress = "FLWI6UNTQ6CXTKSHOC7QPHYD2L3JVLIPWKNR5FECHX46VOE3DMY24BJASY";
        var (error, accountInfo) = await algod.GetAccountInformation(accountAddress);
        if (error)
        {
            Debug.LogError(error);
        }
        else
        {
            Debug.Log($"My account has {accountInfo.Amount / 1_000_000} algos");
        }
    }

    public async UniTaskVoid MakePayment(PrivateKey senderKey, Address receiver, ulong amount)
    {
        var senderAccount = new Account(senderKey);

        // Get the suggested transaction params
        var (txnParamsError, txnParams) = await algod.GetSuggestedParams();
        if (txnParamsError)
        {
            Debug.LogError(txnParamsError);
            return;
        }

        // Construct and sign the payment transaction
        var paymentTxn = Transaction.Payment(
            sender: senderAccount.Address,
            txnParams: txnParams,
            receiver: receiver,
            amount: amount
        );
        var signedTxn = senderAccount.SignTxn(paymentTxn);

        // Send the transaction
        var (sendTxnError, txid) = await algod.SendTransaction(signedTxn);
        if (sendTxnError)
        {
            Debug.LogError(sendTxnError);
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
                return;
            }
            await UniTask.Delay(1000);
        }

        Debug.Log($"Successfully made payment! Confirmed on round {pending.ConfirmedRound}");
    }
}

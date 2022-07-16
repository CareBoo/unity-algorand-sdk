using AlgoSdk;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class YourFirstTransaction
{
    public static async void Main()
    {

    }
}

public class AlgodCheck : MonoBehaviour
{
    AlgodClient algod;

    public void Start()
    {
        algod = new AlgodClient(
            address: "http://localhost:4001",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
        CheckAlgodStatus().Forget();
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
        var response = await algod.HealthCheck();
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
        var (error, accountInfoResponse) = await algod.AccountInformation(accountAddress);
        if (error)
        {
            Debug.LogError(error);
        }
        else
        {
            MicroAlgos amount = accountInfoResponse.WrappedValue.Amount;
            Debug.Log($"My account has {amount.ToAlgos()} algos");
        }
    }

    public async UniTaskVoid MakePayment(PrivateKey senderKey, Address receiver, ulong amount)
    {
        var senderAccount = new Account(senderKey);

        // Get the suggested transaction params
        var (txnParamsError, txnParams) = await algod.TransactionParams();
        txnParamsError.ThrowIfError();

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
        sendTxnError.ThrowIfError();

        // Wait for the transaction to be confirmed
        var (confirmErr, confirmed) = await algod.WaitForConfirmation(txid.TxId);
        confirmErr.ThrowIfError();

        Debug.Log($"Successfully made payment! Confirmed on round {confirmed.ConfirmedRound}");
    }
}

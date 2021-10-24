using AlgoSdk;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class AlgodMakePayment : MonoBehaviour
{
    public ulong Amount;

    public string Receiver;

    [NonSerialized]
    AlgodClient algod;

    [NonSerialized]
    PrivateKey privateKey;

    public void Start()
    {
        algod = new AlgodClient(
            address: "http://localhost:4001",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
        privateKey = Mnemonic
            .FromString("find paddle girl crumble hammer usual obvious toy actual obscure decorate lock bag inmate author valve course ship burger denial sibling tissue nominee above remove")
            .ToPrivateKey();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Making payment transaction of {Amount} microAlgos to account {Receiver}");
            MakePayment(Receiver, Amount).Forget();
        }
    }

    public async UniTaskVoid MakePayment(Address receiver, ulong amount)
    {
        // Get the secret key handle and the public key of the sender account.
        // We'll use the secret key handle to sign the transaction.
        // The public key will be used as the sender's Address.
        using var keyPair = privateKey.ToKeyPair();

        // Get the suggested transaction params
        var (txnParamsError, txnParams) = await algod.GetSuggestedParams();
        if (txnParamsError.IsError)
        {
            Debug.LogError(txnParamsError.Message);
            return;
        }

        // Construct and sign the payment transaction
        var paymentTxn = Transaction.Payment(
            sender: keyPair.PublicKey,
            txnParams: txnParams,
            receiver: receiver,
            amount: amount
        );
        var signedTxn = paymentTxn.Sign(keyPair.SecretKey);

        // Send the transaction
        var (sendTxnError, txid) = await algod.SendTransaction(signedTxn);
        if (sendTxnError.IsError)
        {
            Debug.LogError(sendTxnError.Message);
            return;
        }

        // Wait for the transaction to be confirmed
        PendingTransaction pending = default;
        ErrorResponse error = default;
        while (pending.ConfirmedRound == 0)
        {
            (error, pending) = await algod.GetPendingTransaction(txid);
            if (error.IsError)
            {
                Debug.LogError(error.Message);
                return;
            }
            await UniTask.Delay(1000);
        }
        Debug.Log($"Successfully made payment! Confirmed on round {pending.ConfirmedRound}");
    }
}

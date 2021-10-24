Send a Payment
--------------

Now that we've verified our connection with `AlgodClient` and have obtained the private key of an account with sufficient Algorand balance, we can make a payment transaction. Create the following `MonoBehaviour`:

```csharp
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
        // setup AlgodClient
    }

    public void Update()
    {
        // logic to call MakePayment
    }

    public async UniTaskVoid MakePayment(Address receiver, ulong amount)
    {
        // payment logic
    }
}
```

Let's add in our `AlgodClient` to the `Start()` method again. This time, let's also setup our `privateKey` using the mnemonic
from before.

```csharp
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
```

> [!Warning]
> Never share your private key or mnemonic. In a production environment, do not store your keys in a field like this. Instead, it's recommended to use an external wallet to store keys.

Now let's start building out our `MakePayment(Address receiver, ulong amount)` method. Most of the time, the process for sending a transaction will look like
1. Get the suggested `TransactionParams` from `AlgodClient.GetSuggestedParams()`. This contains information like the fee, the hash of the genesis block, and the latest round number.
2. Create a transaction using the static creation methods found in the `Transaction` class. For a payment transaction, we're going to use `Transaction.Payment(Address sender, TransactionParams txnParams, Address receiver, ulong amount)`.
3. Sign the transaction using either a secret key or a wallet.
4. Send the signed transaction with `AlgodClient.SendTransaction<T>(Signed<T> txn)` and save its `TransactionId`.
5. Wait for the transaction to be confirmed by polling `AlgodClient.GetPendingTransaction(TransactionId txid)` until it returns `PendingTransaction.ConfirmedRound > 0` marking that it was confirmed.

> [!Note]
> The only supported external wallet at the moment is the `kmd` service.

Incorporating the steps from above, our `MakePayment(Address receiver, ulong amount)` implementation will look like

```csharp
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
    )
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
```

> [!Note]
> The `Ed25519.SecretKeyHandle` obtained from a `PrivateKey` is `IDisposable`.
> It or its parent `Ed25519.KeyPair` should be disposed when no longer being used.

All that's left now is to call our `MakePayment` method. We'll specify the `amount` and `receiver` in the editor. Make a new scene
and attach your `AlgodMakePayment` component to a new `GameObject`. Set the `Amount` field to `100000`.
Set the `Receiver` field to a valid address; In this case, we'll use one of the addresses that was listed before,
`2HALGWMIGEFVZRWC3O536UNUY4ABVLZCSBKJI5RC7HE3H64FHGWFVYE6E4`. We'll keep things simple and trigger the payment with the
press of a spacebar. Add the following to your `Update` method:

```csharp
public void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Debug.Log($"Making payment transaction of {Amount} microAlgos to account {Receiver}");
        MakePayment(Receiver, Amount).Forget();
    }
}
```

Your `AlgodMakePayment` script should now look like

```csharp
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
```

Hit the **Play** button, wait for the scene to start, and press spacebar. You should see `Making payment transaction of [...]` then  `Successfully made payment! [...]` shortly after in the console. You just sent your first transaction!

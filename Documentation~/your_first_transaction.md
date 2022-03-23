# Your First Transaction

## Create an Account

The following code will generate a new account:

```csharp
// using AlgoSdk;
// using UnityEngine;

var (privateKey, address) = Account.GenerateAccount();
Debug.Log($"My address: {address}");
Debug.Log($"My private key: {privateKey}");
Debug.Log($"My passphrase (mnemonic): {privateKey.ToMnemonic()}");
```

Save your address and passphrase in a separate place.

> [!Warning]
> Never share your private key or mnemonic. Production environments require stringent private key management.
> For more information on key management in community Wallets, click
> [here](https://developer.algorand.org/ecosystem-projects/#wallets).
> See [WalletConnect](walletconnect.md) for instructions on how to connect to a mobile Wallet.

## Fund the Account

Before sending transactions to the Algorand network, the account must be funded to cover the minimal transaction fees that exist on Algorand. To fund the account use the [Algorand faucet](https://dispenser.testnet.aws.algodev.network/).

## Connect to a Node

> [!Note]
> Prerequisites:
>
> - [Install Docker Compose](https://docs.docker.com/compose/install/)
> - [Install Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git)

The easiest way to access a node in development is via the Algorand Sandbox. Alternatively, [AlgoExplorer](https://testnet.algoexplorer.io/)
provides [`algod`](https://testnet.algoexplorer.io/api-dev/v2) and [`indexer`](https://testnet.algoexplorer.io/api-dev/indexer-v2) nodes with limited features.

```bash
> git clone https://github.com/algorand/sandbox.git
> cd sandbox
> ./sandbox up testnet
```

The `testnet` option will configure the network to use the testnet.

> [!Warning]
> The sandbox installation may take a few minutes to startup in order to catch up to the current block round.
> To learn more about fast catchup, see [Sync Node Network using Fast Catchup](https://developer.algorand.org/docs/run-a-node/setup/install/#sync-node-network-using-fast-catchup)

Once the network is up and running, you should be able to connect to the `algod` service.
Verify that the node is healthy and you can connect to it. Create a new `AlgodCheck` component
that creates an `AlgodClient` on `Start()` and makes a `GetHealth()` request.

```csharp
using AlgoSdk;
using Cysharp.Threading.Tasks;
using UnityEngine;

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

    public async UniTaskVoid CheckAlgodStatus()
    {
        var response = await algod.GetHealth();
        if (response.Error.IsError)
        {
            Debug.LogError(response.Error.Message);
        }
        else
        {
            Debug.Log("Connected to algod!");
        }
    }
}
```

Add the `AlgodCheck` component to a `GameObject` in a new scene, and press **Play**. You should see the
`Connected to algod!` message in the editor console. If you cannot connect, or the node is not healthy,
then you should see an error message in the console.

> [!Note]
> It's possible the node you're connecting to may require different HTTP request headers for authentication.
> In that case you can specify the explicit HTTP headers using key-value string tuples:
>
> ```csharp
> algod = new AlgodClient(
>    "https://testnet-algorand.api.purestake.io/ps2",
>    ("X-API-Key", "my-super-secret-api-key")
> );
> ```

## Check Your Balance

To verify your funds were added to your account, add the following method to your `AlgodCheck` script.
Replace the address with the address of the account you generated earlier.

```csharp
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
```

Then call it in your `Start()` method.

```csharp
public void Start()
{
    CheckBalance().Forget();
}
```

Your balance should appear in the editor console when you press **Play** again.

## Send a Transaction

The process for sending a transaction will look like

1. Get the suggested `TransactionParams` from `AlgodClient.GetSuggestedParams()`. This contains information like the fee, the hash of the genesis block, and the latest round number.
2. Create a transaction using the static creation methods found in the `Transaction` class. For a payment transaction, we're going to use `Transaction.Payment(Address sender, TransactionParams txnParams, Address receiver, ulong amount)`.
3. Sign the transaction using either a secret key or a wallet.
4. Send the signed transaction with `AlgodClient.SendTransaction<T>(Signed<T> txn)` and save its `TransactionId`.
5. Wait for the transaction to be confirmed by polling `AlgodClient.GetPendingTransaction(TransactionId txid)` until it returns `PendingTransaction.ConfirmedRound > 0` marking that it was confirmed.

Add a new method, `MakePayment(PrivateKey senderKey, Address receiver, ulong amount)` that will send a `PaymentTxn`.

```csharp
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
```

To keep things simple, add a new `Update()` method that will invoke `MakePayment` whenever the player presses their spacebar.
Replace the passphrase with the passphrase you generated earlier.

```csharp
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
```

Now press **Play** again, then press the spacebar. After a couple seconds, your transaction should be confirmed and you should see a
"Successfully made payment!" message in the editor console.

## Complete Example

Here is the final code for `AlgodCheck` component.

```csharp
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
```

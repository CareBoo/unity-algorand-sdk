# WalletConnect

[WalletConnect](https://walletconnect.com/) is the web3 standard to connect blockchain wallets to dapps.
The Algorand Foundation's [Pera Wallet](https://algorandwallet.com/) supports this standard, and it's
the easiest way to handle signing transactions for the end user.

## Initiating a WalletConnect session

The process for creating a WalletConnect session is:

1. The Dapp shows the user a QR Code to initiate the handshake between Dapp and Wallet.
2. The user reads the QR Code with their Wallet, and chooses an account to connect with Dapp.
3. The Dapp receives the confirmation and can begin requesting the Wallet to sign transactions.

In code, this process is:

```csharp
using AlgoSdk;
using AlgoSdk.WalletConnect;
using UnityEngine;

// 1. Create the session and show the user the QR Code
var dappMeta = new ClientMeta
{
    Name = "<name of your dapp>",
    Description = "<description of your dapp>",
    Url = "<url of your dapp>",
    IconUrls = new[]
    {
        "<icon1 of your dapp>", "<icon2 of your dapp>"
    }
};
var session = new AlgorandWalletConnectSession(dappMeta);
// session is in no connection status
Debug.Assert(session.ConnectionStatus == AlgorandWalletConnectSession.Status.NoConnection);

var handshake = await session.StartConnection();
// session should now be in connecting status
Debug.Assert(session.ConnectionStatus == AlgorandWalletConnectSession.Status.RequestingConnection);

// show the user a QR Code
Texture2D qrCode = handshake.ToQrCodeTexture();

// 2. Wait for user to approve the connection
await session.WaitForConnectionApproval();
// session is now connected
Debug.Assert(session.ConnectionStatus == AlgorandWalletConnectSession.Status.Connected);

// 3. Send transactions to sign
var someTxnToSign = Transaction.Payment(...);
someTxnToSign.Sender = session.Accounts[0];
var walletTransaction = WalletTransaction.New(someTxnToSign);
var signedTxns = await session.SignTransactions(new[] { walletTransaction });
```

After sending every transaction to sign, the user will be requested to approve the transaction
in their Wallet application.

## Handling Session Updates

At any point during the session, the wallet may

- No longer authorize the Dapp to send transactions.
- Disconnect from the session.
- Update information about the accounts or Wallet metadata.

`AlgorandWalletConnectSession` provides events to handle these scenarios:

|        Events         | Event Type                                 | Description                                                |
| :-------------------: | :----------------------------------------- | :--------------------------------------------------------- |
|  `OnSessionConnect`   | `UnityEvent<AlgorandWalletConnectSession>` | Called when the Wallet approves the connection             |
|   `OnSessionUpdate`   | `UnityEvent<WalletConnectSessionData>`     | Called when the Wallet updates accounts or Wallet metadata |
| `OnSessionDisconnect` | `UnityEvent<string>`                       | Called when the Wallet disconnects from the session        |

## Persisting Sessions

WalletConnect sessions are designed to persist across dapp sessions. To do this, a session
can be saved as a `SavedSession`:

```csharp
var session = new AlgorandWalletConnectSession(...);
var savedSession = session.Save();
```

You can continue a `SavedSession` by starting the AlgorandWalletConnectSession:

```csharp
var continuedSession = new AlgorandWalletConnectSession(savedSession);
```

> [!Note]
> The handshake URL and QR Code generated from `StartConnection` are not persisted.
> Despite this, the session will still be able listen for the wallet's approval. This is because
> the handshake and session ids are persisted in the `SavedSession`.

`SavedSession` can be serialized by Unity software, so it's trivial to store it in `PlayerPrefs` for example:

```csharp
// storing a SavedSession
var savedSessionJson = JsonUtility.ToJson(savedSession);
PlayerPrefs.SetString("users_wallet_connect_session", savedSessionJson);
PlayerPrefs.Save();
```

## Additional Resources

Take a look at the [official Algorand documentation on WalletConnect](https://developer.algorand.org/docs/get-details/walletconnect/) to learn more.

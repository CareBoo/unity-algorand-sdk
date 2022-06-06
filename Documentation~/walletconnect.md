# WalletConnect

[WalletConnect](https://walletconnect.com/) is the web3 standard to connect blockchain wallets to dapps.
The Algorand Foundation's [Pera Wallet](https://algorandwallet.com/) supports this standard, and it's
the easiest way to handle signing transactions for the end user.

## Initiating a WalletConnect session

The general process for creating a WalletConnect session is:

1. The Dapp shows the user a QR Code to initiate the handshake between Dapp and Wallet. OR on mobile, the Dapp
   provides the user a list of wallets to open via [Mobile Linking](#mobile-linking).
2. The user reads the QR Code with their Wallet OR selects a wallet from a list, and chooses an address to connect with Dapp.
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
await session.Connect();
// session is in no connection status
Debug.Assert(session.ConnectionStatus == SessionStatus.NoConnection);

var handshake = session.RequestWalletConnection();
// session should now be in connecting status
Debug.Assert(session.ConnectionStatus == SessionStatus.RequestingConnection);

// show the user a QR Code
Texture2D qrCode = handshake.ToQrCodeTexture();
// OR launch a supported wallet app
WalletRegistry.PeraWallet.LaunchForConnect(handshake);

// 2. Wait for user to approve the connection
await session.WaitForWalletApproval();
// session is now connected
Debug.Assert(session.ConnectionStatus == SessionStatus.Connected);

// 3. Send transactions to sign
var someTxnToSign = Transaction.Payment(...);
someTxnToSign.Sender = session.Accounts[0];
var walletTransaction = WalletTransaction.New(someTxnToSign);
// In the case you used a QR Code, you can await immediately.
var signingTransactions = session.SignTransactions(new[] { walletTransaction });
// If you are using Mobile Linking, you need to launch the app
WalletRegistry.PeraWallet.LaunchForSigning();
var (signErr, signedTxns) = await signingTransactions;
```

After sending every transaction to sign, the user will be requested to approve the transaction
in their Wallet application. Keep in mind that when using [Mobile Linking](#mobile-linking),
you will need to launch the app.

## Handling Session Updates

At any point during the session, the wallet may

- No longer authorize the Dapp to send transactions.
- Disconnect from the session.
- Update information about the accounts or Wallet metadata.

`AlgorandWalletConnectSession` provides events to handle these scenarios:

|        Events         | Event Type                             | Description                                                |
| :-------------------: | :------------------------------------- | :--------------------------------------------------------- |
|  `OnSessionConnect`   | `Action<AlgorandWalletConnectSession>` | Called when the Wallet approves the connection             |
|   `OnSessionUpdate`   | `Action<WalletConnectSessionData>`     | Called when the Wallet updates accounts or Wallet metadata |
| `OnSessionDisconnect` | `Action<string>`                       | Called when the Wallet disconnects from the session        |

## Persisting Sessions

WalletConnect sessions are designed to persist across dapp sessions. To do this, a session
can be saved as a `SessionData`:

```csharp
var session = new AlgorandWalletConnectSession(...);
var savedSession = session.Save();
```

You can continue a saved session by starting the AlgorandWalletConnectSession:

```csharp
var continuedSession = new AlgorandWalletConnectSession(savedSession);
await continuedSession.Connect();
// session should be in connected state
Debug.Assert(session.ConnectionStatus == SessionStatus.Connected);
```

> [!Note]
> The handshake URL and QR Code generated from `RequestWalletConnection` are not persisted.
> Despite this, the session will still be able listen for the wallet's approval. This is because
> the handshake and session ids are persisted in the `SessionData`.

`SessionData` can be serialized by Unity software, so it's trivial to store it in `PlayerPrefs` for example:

```csharp
// storing a SavedSession
var savedSessionJson = JsonUtility.ToJson(savedSession);
PlayerPrefs.SetString("users_wallet_connect_session", savedSessionJson);
PlayerPrefs.Save();
```

## Mobile Linking

When users are using your Dapp from a mobile application, they may want to open a native wallet
on their device. [WalletConnect provides a protocol to do this](https://docs.walletconnect.com/mobile-linking).

To begin linking on mobile, you'll need to use the `WalletRegistry` static class to find supported
wallets for the current platform. Each time you need the user to approve a request from the wallet, you'll need
to launch the wallet app.

```csharp
using AlgoSdk;
using AlgoSdk.WalletConnect;

var supportedWallets = WalletRegistry.SupportedWalletsForCurrentPlatform;

var session = new AlgorandWalletConnectSession(...);
await session.Connect();
var handshakeUrl = session.RequestWalletConnection();
var chosenWallet = AskUserToChooseWallet(supportedWallets);
chosenWallet.LaunchForConnect(handshakeUrl);

await session.WaitForWalletApproval();

AppEntry AskUserToChooseWallet(AppEntry[] supportedWallets)
{
    // implement UI to allow user to choose wallet
}
```

Additionally, anytime you need to sign a transaction, you'll need to launch the wallet app again.

```csharp
var signing = await session.SignTransactions(...);
chosenWallet.LaunchForSigning(session.Version);
var (signErr, signedTxns) = await signing;
```

## Additional Resources

Take a look at the [official Algorand documentation on WalletConnect](https://developer.algorand.org/docs/get-details/walletconnect/) to learn more.
The [official WalletConnect documentation]("https://docs.walletconnect.com/") is useful as well.

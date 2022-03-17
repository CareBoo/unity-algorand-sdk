<h1 id="unity-algorand-sdk" align="center">
<img src="Documentation~/images/logo_256.png"/>
<br/>
Unity Algorand SDK

</h1>
<p align="center">
  <a href="LICENSE.md">
    <img src="https://img.shields.io/github/license/CareBoo/unity-algorand-sdk"/>
  </a>
  <a href="https://github.com/CareBoo/unity-algorand-sdk/actions/workflows/test.yaml">
    <img src="https://img.shields.io/github/workflow/status/careboo/unity-algorand-sdk/Unity%20Tests/main?label=tests"/>
  </a>
  <a href="https://www.npmjs.com/package/com.careboo.unity-algorand-sdk">
    <img src="https://img.shields.io/npm/v/com.careboo.unity-algorand-sdk"/>
  </a>
  <a href="https://openupm.com/packages/com.careboo.unity-algorand-sdk/">
    <img src="https://img.shields.io/npm/v/com.careboo.unity-algorand-sdk?label=openupm&registry_uri=https://package.openupm.com"/>
  </a>
</p>

</h1>

> [!Caution]
> This package has not been audited and isn't suitable for production use.

Integrate your game with [Algorand](https://www.algorand.com/), a Pure Proof-of-Stake blockchain overseen by the Algorand Foundation.
Create and sign Algorand transactions, use Algorand's [REST APIs](https://developer.algorand.org/docs/rest-apis/restendpoints/),
and connect to any Algorand wallet supporting [WalletConnect](https://developer.algorand.org/docs/get-details/walletconnect/).

## Requirements

This package supports the following build targets and Unity versions:

| Unity Version |      Windows       |       Mac OS       |       Linux        |      Android       |        iOS         |       WebGL        |
| :-----------: | :----------------: | :----------------: | :----------------: | :----------------: | :----------------: | :----------------: |
|    2020.3     | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: |
|    2021.2     | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: |

## Common Usages

Make a payment transaction:

```csharp
using AlgoSdk;

var sender = "<your sender address>";
var receiver = "<your receiver address>";
var algod = new AlgodClient("https://node.testnet.algoexplorerapi.io");
var suggestedTxnParams = await algod.GetSuggestedParams();
var microAlgosToSend = 1_000_000L;
var paymentTxn = Transaction.Payment(sender, suggestedTxnParams, receiver, microAlgosToSend);
```

Initiate a WalletConnect session and generate a QR Code:

```csharp
using AlgoSdk;
using AlgoSdk.WalletConnect;
using UnityEngine;

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
var handshake = await session.StartConnection();
Texture2D qrCode = handshake.ToQrCodeTexture();
```

Sign transactions with a private key, `kmd`, or WalletConnect:

```csharp
using AlgoSdk;
using AlgoSdk.Crypto;
using AlgoSdk.WalletConnect;

PaymentTxn paymentTxn = Transaction.Payment(...);
// Signing with a private key
var signedTxn = myPrivateKey.Sign(paymentTxn);

// Signing with kmd
var kmd = new KmdClient("host of kmd");
var walletToken = await kmd.InitWalletHandleToken("<your wallet id>", "<your wallet password>");
var signedTxn = await kmd.SignTransaction(paymentTxn.Sender, paymentTxn.ToSignatureMessage(), walletToken, "<your kmd wallet password>");

// Signing with WalletConnect
SavedSession savedSession = ...;
var session = new AlgorandWalletConnectSession(savedSession);
var walletTransaction = WalletTransaction.New(paymentTxn);
var signedTxns = await session.SignTransactions(new[] { walletTransaction });
var signedTxn = signedTxns[0];

// Send the transaction:
var algod = new AlgodClient("<your client host>");
await algod.SendTransaction(signedTxn);
```

## Installation

### Open UPM

The easiest way to install is to use Open UPM as it manages your scopes automatically.
You can [install Open UPM here](https://openupm.com/docs/getting-started.html).
Then use the Open UPM CLI at the root of your Unity project to install.

```sh
> cd <your unity project>
> openupm add com.careboo.unity-algorand-sdk
```

### Manually Adding UPM Scopes

If you don't want to use Open UPM, it's straightforward to manually add the UPM registry scopes
required for this package. See [Unity's official documentation on Scoped Registries](https://docs.unity3d.com/Manual/upm-scoped.html).

### Unity Asset Store

This SDK will soon be [available in the Unity Asset Store](https://u3d.as/2GBr).

## Getting Started

### Documentation

The documentation built for a specific version is hosted at:

```
https://careboo.github.io/unity-algorand-sdk/{version}/
```

Read [Your First Transaction](Documentation~/your_first_transaction.md) to learn the basics on creating, signing, and sending transactions.
The [WalletConnect guide](Documentation~/walletconnect.md) teaches about how to implement WalletConnect in your application.

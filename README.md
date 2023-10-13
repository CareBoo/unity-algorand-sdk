<h1 id="unity-algorand-sdk" align="center">
<picture>
  <img alt="The Algorand Logo" src="docs/images/algorand_logo_mark.svg"/>
</picture>
<br/>
Algorand SDK for Unity

</h1>
<p align="center">
  <a href="LICENSE.md">
    <img src="https://img.shields.io/github/license/CareBoo/unity-algorand-sdk"/>
  </a>
  <a href="https://github.com/CareBoo/unity-algorand-sdk/actions/workflows/test.yaml">
    <img src="https://img.shields.io/github/actions/workflow/status/CareBoo/unity-algorand-sdk/test.yaml?branch=main&label=tests"/>
  </a>
  <a href="https://www.npmjs.com/package/com.careboo.unity-algorand-sdk">
    <img src="https://img.shields.io/npm/v/com.careboo.unity-algorand-sdk"/>
  </a>
  <a href="https://openupm.com/packages/com.careboo.unity-algorand-sdk/">
    <img src="https://img.shields.io/npm/v/com.careboo.unity-algorand-sdk?label=openupm&registry_uri=https://package.openupm.com"/>
  </a>
</p>

</h1>

Integrate your game with [Algorand](https://www.algorand.com/), a Pure Proof-of-Stake blockchain overseen by the Algorand Foundation.
Create Algorand transactions, Algorand accounts, and use Algorand's [REST APIs](https://developer.algorand.org/docs/rest-apis/restendpoints/).

## Table of Contents

- [Table of Contents](#table-of-contents)
- [Requirements](#requirements)
- [Common Usages](#common-usages)
  - [Make a payment transaction](#make-a-payment-transaction)
    - [Sign the transaction with an account](#sign-the-transaction-with-an-account)
    - [Sign the transaction with `kmd`](#sign-the-transaction-with-kmd)
    - [Send the signed transaction](#send-the-signed-transaction)
- [Installation](#installation)
  - [Open UPM](#open-upm)
  - [Manually Adding UPM Scopes](#manually-adding-upm-scopes)
  - [Unity Asset Store](#unity-asset-store)
- [Getting Started](#getting-started)
  - [Documentation Site](#documentation-site)
  - [Samples](#samples)

## Requirements

This package supports the following build targets and Unity versions:

| Unity Version |      Windows       |       Mac OS       |       Linux        |      Android       |        iOS         |       WebGL        |
| :-----------: | :----------------: | :----------------: | :----------------: | :----------------: | :----------------: | :----------------: |
|    2021.3     | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: |
|    2022.3     | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: |

- :white_check_mark: All APIs are supported.
- :x: Not supported. Use at your own risk.

## Common Usages

### Make a payment transaction:

```csharp
using Algorand.Unity;

var sender = "<your sender address>";
var receiver = "<your receiver address>";
var algod = new AlgodClient("https://testnet-api.algonode.cloud");
var suggestedTxnParams = await algod.GetSuggestedParams();
var microAlgosToSend = 1_000_000L;
var paymentTxn = Transaction.Payment(sender, suggestedTxnParams, receiver, microAlgosToSend);
```

#### Sign the transaction with an account:

```csharp
var account = Account.GenerateAccount();
var signedTxn = account.SignTxn(paymentTxn);
```

#### Sign the transaction with `kmd`:

```csharp
var kmd = new KmdClient("<host of kmd>");
var walletToken = await kmd.InitWalletHandleToken("<your wallet id>", "<your wallet password>");
var signedTxn = await kmd.SignTransaction(paymentTxn.Sender, paymentTxn.ToSignatureMessage(), walletToken, "<your kmd wallet password>");
```

#### Send the signed transaction:

```csharp
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

[Algorand SDK for Unity](https://u3d.as/31Er) is now available!

## Getting Started

Read [Getting Started](docs/getting_started.md) to learn the basic workflows for developing on Algorand.

### Documentation Site

Docs for this version were generated at https://careboo.github.io/unity-algorand-sdk/5.0.

### Samples

Some of the samples are built on WebGL and hosted on GitHub Pages.

- [Your First Transaction](https://careboo.github.io/unity-algorand-sdk/your-first-transaction)
- [Local Wallet](https://careboo.github.io/unity-algorand-sdk/local-wallet)

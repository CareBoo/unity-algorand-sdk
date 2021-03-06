# Getting Started

## How do I connect to an Algorand Network?

All connections to an Algorand Network is via a node that exists on that network. Nodes provide three REST services:

| Service                       | Purpose                                                                                                                                                | Client Class                                  |
| ----------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------ | --------------------------------------------- |
| `algod` (Algo Daemon)         | Make and monitor [Transactions](xref:AlgoSdk.Transaction). Any sort of writing to the blockchain happens via this service.                             | [`AlgodClient`](xref:AlgoSdk.AlgodClient)     |
| `indexer`                     | Query the Algorand Blockchain. Used to determine things like account balance, whether or not an account has a token, or the state of a smart contract. | [`IndexerClient`](xref:AlgoSdk.IndexerClient) |
| `kmd` (Key Management Daemon) | Manage private keys securely in a wallet. Useful when you need basic wallet features, or when developing locally.                                      | [`KmdClient`](xref:AlgoSdk.KmdClient)         |

When developing locally, it's **very** important to have an Algorand node setup for quick iteration and testing. See [Developing with Algorand Sandbox](getting_started/developing_with_sandbox.md) for a guide on setting up a local Algorand network for this purpose.

## How do I write to an Algorand Blockchain?

A blockchain is a ledger made up of transactions. Any write to a blockchain (not just the Algorand blockchain) requires making a [Transaction](xref:AlgoSdk.Transaction). To make a transaction:

1. Define/construct your transaction using static methods on [`Transaction`](xref:AlgoSdk.Transaction) class.
2. Sign your transaction with a [`Signer`](xref:AlgoSdk.ISigner) or [`AsyncSigner`](xref:AlgoSdk.IAsyncSigner).
3. Send the transaction using `algod` service via [`AlgodClient.RawTransaction`](xref:AlgoSdk.AlgodClient.RawTransaction*).
4. Wait for the transaction to be confirmed via [`AlgodClient.WaitForConfirmation`](xref:AlgoSdk.AlgodClient.WaitForConfirmation*).

See [Your First Transaction](getting_started/your_first_transaction.md) for an in-depth guide on making your first transaction.

## How do I create an NFT or other kind of Token?

Tokens on the Algorand blockchain are represented by Algorand Standard Assets (ASAs). See [the guide on Algorand Standard Assets](algorand_standard_assets.md) to learn more about how to manipulate tokens with this SDK.

## What about smart contracts?

There are two kinds of smart contracts on the Algorand blockchain:

| Type of Smart Contract | Other Names                 | SDK entrypoint                                                 | Official Docs                                                                                       |
| ---------------------- | --------------------------- | -------------------------------------------------------------- | --------------------------------------------------------------------------------------------------- |
| Stateful               | Application, Smart Contract | [`Transaction.AppCreate`](xref:AlgoSdk.Transaction.AppCreate*) | [See docs](https://developer.algorand.org/docs/get-details/dapps/smart-contracts/#smart-contracts)  |
| Stateless              | Logicsig, Smart Signature   | [`LogicSig`](xref:AlgoSdk.LogicSig)                            | [See docs](https://developer.algorand.org/docs/get-details/dapps/smart-contracts/#smart-signatures) |

All smart contracts are written using [TEAL](https://developer.algorand.org/docs/get-details/dapps/avm/teal/). To use a smart contract:

1. Use your favorite tool to write TEAL source code. (Some may like [PyTEAL](https://pyteal.readthedocs.io/en/stable/) or [Reach](https://developer.algorand.org/docs/get-started/dapps/reach/)).
2. Compile the source code using [`AlgodClient.TealCompile`](xref:AlgoSdk.AlgodClient.TealCompile).
3. Use the compiled program in your [`LogicSig`](xref:AlgoSdk.LogicSig) or [`Transaction.AppCreate`](xref:AlgoSdk.Transaction.AppCreate*).

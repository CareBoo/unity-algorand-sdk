# Getting Started

## How do I connect to an Algorand Network?

All connections to an Algorand Network is via a node that exists on that network. Nodes provide three REST services:

| Service                       | Client                                               | Purpose                                                                                 |
| ----------------------------- | ---------------------------------------------------- | --------------------------------------------------------------------------------------- |
| `algod` (Algo Daemon)         | [`AlgodClient`](xref:Algorand.Unity.AlgodClient)     | Send and monitor [Transactions](xref:Algorand.Unity.Transaction).                       |
| `indexer`                     | [`IndexerClient`](xref:Algorand.Unity.IndexerClient) | Query the Algorand Blockchain and lookup account, asset, or smart contract information. |
| `kmd` (Key Management Daemon) | [`KmdClient`](xref:Algorand.Unity.KmdClient)         | Manage private keys and accounts securely in a wallet hosted by the node.               |

When developing locally, it's **very** important to have an Algorand node setup for quick iteration and testing. See [Developing with Algorand Sandbox](getting_started/developing_with_sandbox.md) for a guide on setting up a local Algorand network for this purpose.

## How do I write to an Algorand Blockchain?

A blockchain is a ledger made up of transactions. Any write to a blockchain (not just the Algorand blockchain) requires making a [Transaction](xref:Algorand.Unity.Transaction). To make a transaction:

1. Define/construct your transaction using static methods on [`Transaction`](xref:Algorand.Unity.Transaction) class.
2. Sign your transaction with a [`Signer`](xref:Algorand.Unity.ISigner) or [`AsyncSigner`](xref:Algorand.Unity.IAsyncSigner).
3. Send the transaction using `algod` service via [`AlgodClient.RawTransaction`](xref:Algorand.Unity.AlgodClient.RawTransaction*).
4. Wait for the transaction to be confirmed via [`AlgodClient.WaitForConfirmation`](xref:Algorand.Unity.AlgodClient.WaitForConfirmation*).

See [Your First Transaction](getting_started/your_first_transaction.md) for an in-depth guide on making your first transaction.

## How do I create an NFT or other kind of Token?

Tokens on the Algorand blockchain are represented by Algorand Standard Assets (ASAs). See [the guide on Algorand Standard Assets](algorand_standard_assets.md) to learn more about how to manipulate tokens with this SDK.

## What about smart contracts?

There are two kinds of smart contracts on the Algorand blockchain:

| Type of Smart Contract | Other Names                 | SDK entrypoint                                                        | Official Docs                                                                                       |
| ---------------------- | --------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------- |
| Stateful               | Application, Smart Contract | [`Transaction.AppCreate`](xref:Algorand.Unity.Transaction.AppCreate*) | [See docs](https://developer.algorand.org/docs/get-details/dapps/smart-contracts/#smart-contracts)  |
| Stateless              | Logicsig, Smart Signature   | [`LogicSig`](xref:Algorand.Unity.LogicSig)                            | [See docs](https://developer.algorand.org/docs/get-details/dapps/smart-contracts/#smart-signatures) |

All smart contracts are written using [TEAL](https://developer.algorand.org/docs/get-details/dapps/avm/teal/). To use a smart contract:

1. Use your favorite tool to write TEAL source code. (Some may like [PyTEAL](https://pyteal.readthedocs.io/en/stable/) or [Reach](https://developer.algorand.org/docs/get-started/dapps/reach/)).
2. Compile the source code using [`AlgodClient.TealCompile`](xref:Algorand.Unity.AlgodClient.TealCompile).
3. Use the compiled program in your [`LogicSig`](xref:Algorand.Unity.LogicSig) or [`Transaction.AppCreate`](xref:Algorand.Unity.Transaction.AppCreate*).

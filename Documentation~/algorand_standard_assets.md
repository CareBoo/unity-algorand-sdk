# Algorand Standard Assets

Algorand allows custom tokens and NFTs through [Algorand Standard Assets (ASAs)](https://developer.algorand.org/docs/get-details/asa/). ASAs can be managed through different `Asset[...]Txn` transaction types. See the following table for the available transaction types and how they can be used to manage ASAs:

| Transaction Method                                                       | Description                                          |
| ------------------------------------------------------------------------ | ---------------------------------------------------- |
| [`Transaction.AssetCreate`](xref:AlgoSdk.Transaction.AssetCreate*)       | Create a new ASA.                                    |
| [`Transaction.AssetConfigure`](xref:AlgoSdk.Transaction.AssetConfigure*) | Update an existing ASA.                              |
| [`Transaction.AssetDelete`](xref:AlgoSdk.Transaction.AssetDelete*)       | Delete an existing ASA.                              |
| [`Transaction.AssetTransfer`](xref:AlgoSdk.Transaction.AssetTransfer*)   | Transfer an ASA from one account to another.         |
| [`Transaction.AssetAccept`](xref:AlgoSdk.Transaction.AssetAccept*)       | Opt in to an ASA.                                    |
| [`Transaction.AssetClawback`](xref:AlgoSdk.Transaction.AssetClawback*)   | Clawback an ASA.                                     |
| [`Transaction.AssetFreeze`](xref:AlgoSdk.Transaction.AssetFreeze*)       | Freeze an ASA from being transferred from an account |

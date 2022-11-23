# Algorand Standard Assets

Algorand allows custom tokens and NFTs through [Algorand Standard Assets (ASAs)](https://developer.algorand.org/docs/get-details/asa/). ASAs can be managed through different `Asset[...]Txn` transaction types. See the following table for the available transaction types and how they can be used to manage ASAs:

| Transaction Method                                                              | Description                                          |
| ------------------------------------------------------------------------------- | ---------------------------------------------------- |
| [`Transaction.AssetCreate`](xref:Algorand.Unity.Transaction.AssetCreate*)       | Create a new ASA.                                    |
| [`Transaction.AssetConfigure`](xref:Algorand.Unity.Transaction.AssetConfigure*) | Update an existing ASA.                              |
| [`Transaction.AssetDelete`](xref:Algorand.Unity.Transaction.AssetDelete*)       | Delete an existing ASA.                              |
| [`Transaction.AssetTransfer`](xref:Algorand.Unity.Transaction.AssetTransfer*)   | Transfer an ASA from one account to another.         |
| [`Transaction.AssetAccept`](xref:Algorand.Unity.Transaction.AssetAccept*)       | Opt in to an ASA.                                    |
| [`Transaction.AssetClawback`](xref:Algorand.Unity.Transaction.AssetClawback*)   | Clawback an ASA.                                     |
| [`Transaction.AssetFreeze`](xref:Algorand.Unity.Transaction.AssetFreeze*)       | Freeze an ASA from being transferred from an account |

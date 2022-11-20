# Integration with the .NET SDK

This SDK embeds the latest [.NET Algorand SDK](https://github.com/FrankSzendzielarz/dotnet-algorand-sdk) for usage with its models and classes. Most models and clients in the `AlgoSdk.Algod` and `AlgoSdk.Indexer` namespace have explicit conversions to corresponding models and apis in the `Algorand.Algod` and `Algorand.Indexer` namespaces.

For example, you can convert `AlgoSdk.AlgodClient` to `Algorand.Algod.DefaultApi`:

```csharp
var sandboxAlgodClient = new AlgoSdk.AlgodClient(
    "http://localhost:4001",
    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
var sandboxAlgodDefaultApi = sandboxAlgodClient.ToDefaultApi();
```

or convert `AlgoSdk.IndexerClient` to `Algorand.Indexer.LookupApi`:

```csharp
var sandboxIndexerClient = new AlgoSdk.IndexerClient("http://localhost:8980");
var sandboxIndexerLookupApi = sandboxIndexerClient.ToLookupApi();
```

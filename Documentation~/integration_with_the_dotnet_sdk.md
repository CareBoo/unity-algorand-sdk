# Integration with the .NET SDK

This SDK embeds the latest [.NET Algorand SDK](https://github.com/FrankSzendzielarz/dotnet-algorand-sdk) for usage with its models and classes. Most models and clients in the `Algorand.Unity.Algod` and `Algorand.Unity.Indexer` namespace have explicit conversions to corresponding models and apis in the `Algorand.Algod` and `Algorand.Indexer` namespaces.

For example, you can convert `Algorand.Unity.AlgodClient` to `Algorand.Algod.DefaultApi`:

```csharp
var sandboxAlgodClient = new Algorand.Unity.AlgodClient(
    "http://localhost:4001",
    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
var sandboxAlgodDefaultApi = sandboxAlgodClient.ToDefaultApi();
```

or convert `Algorand.Unity.IndexerClient` to `Algorand.Indexer.LookupApi`:

```csharp
var sandboxIndexerClient = new Algorand.Unity.IndexerClient("http://localhost:8980");
var sandboxIndexerLookupApi = sandboxIndexerClient.ToLookupApi();
```

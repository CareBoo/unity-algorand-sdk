# Integration with the .NET SDK

This SDK embeds the latest [.NET Algorand SDK](https://github.com/FrankSzendzielarz/dotnet-algorand-sdk) for usage with its models and classes. Most models and clients in the `Algorand.Unity.Algod` and `Algorand.Unity.Indexer` namespace have `ToUnity` and `ToDotnet` extension methods implemented in the `Algorand.Unity.Net` assembly.

## Requirements

1. Your scripts must exist under an Assembly Definition that references the `Algorand.dll` assembly and the `Algorand.Unity` and `Algorand.Unity.Net` assembly definitions.
2. Enable the extension methods with

```csharp
using Algorand.Unity.Net;
```

> [!Warning]
> Using .NET SDK client types and signing methods are not supported for the WebGL build target. Because of this, the `Algorand.Unity.Net` assembly won't compile for the WebGL build target.

## Examples

### `Algorand.Unity.AlgodClient` to `Algorand.Algod.DefaultApi`:

```csharp
Algorand.Unity.AlgodClient localAlgodClient = new Algorand.Unity.AlgodClient(
    "http://localhost:4001",
    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
Algorand.Algod.DefaultApi localAlgodDefaultApi = localAlgodClient.ToDefaultApi();
```

### `Algorand.Unity.IndexerClient` to `Algorand.Indexer.LookupApi`:

```csharp
Algorand.Unity.IndexerClient localIndexerClient = new Algorand.Unity.IndexerClient("http://localhost:8980");
Algorand.Indexer.LookupApi localIndexerLookupApi = localIndexerClient.ToLookupApi();
```

### `Algorand.Unity.Address` to `Algorand.Address` (Read-Only)

```csharp
Algorand.Unity.Address myAddress = Algorand.Unity.Address.FromString("VHHZTDDT3GEPZ4LXCDHGBGFFJEGIKJLBFGXKQIDD6XK26P5B7ZWY5EFCNE");
Algorand.Address dotnetAddress = myAddress.ToDotnet();
```

### `Algorand.TEALProgram` to `Algorand.Unity.CompiledTeal`

```csharp
TEALProgram teal = new TEALProgram(new byte[] {/* ... */ });
CompiledTeal unityTeal = teal.ToUnity();
```

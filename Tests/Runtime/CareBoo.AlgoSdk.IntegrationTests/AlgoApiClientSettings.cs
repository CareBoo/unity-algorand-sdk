using AlgoSdk;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class AlgoApiClientSettings
{
    public static AlgodClient Algod => GetJson<AlgodClient>(nameof(Algod), GetSandboxAlgodClient());

    public static IndexerClient Indexer => GetJson<IndexerClient>(nameof(Indexer), GetSandboxIndexerClient());

    public static KmdClient Kmd => GetJson<KmdClient>(nameof(Kmd), GetSandboxKmdClient());

    public static T GetJson<T>(string propertyPath, T defaultVal = default)
    {
        var json = GetString(propertyPath);
        return string.IsNullOrEmpty(json) ? defaultVal : JsonUtility.FromJson<T>(json);
    }

    public static string GetString(string propertyPath)
    {
        var key = GetKey(propertyPath);
        var val = System.Environment.GetEnvironmentVariable(key);
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(val))
            val = EditorPrefs.GetString(key, null);
#endif
        return val;
    }

    public static string GetKey(string propertyPath)
    {
        return $"{nameof(AlgoApiClientSettings)}_{propertyPath}";
    }

    static AlgodClient GetSandboxAlgodClient()
    {
        return new AlgodClient(
            address: "http://localhost:4001",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
    }

    static IndexerClient GetSandboxIndexerClient()
    {
        return new IndexerClient(address: "http://localhost:8980");
    }

    static KmdClient GetSandboxKmdClient()
    {
        return new KmdClient(
            address: "http://localhost:4002",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
    }
}

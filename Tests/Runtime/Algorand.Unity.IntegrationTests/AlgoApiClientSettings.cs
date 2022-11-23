using Algorand.Unity;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class AlgoApiClientSettings
{
    public static IAlgodClient Algod { get; set; } = GetSandboxAlgodClient();
    public static IIndexerClient Indexer { get; set; } = GetSandboxIndexerClient();
    public static KmdClient Kmd { get; set; } = GetSandboxKmdClient();

    public static AlgodClient DefaultAlgod { get; set; } = GetSandboxAlgodClient();
    public static IndexerClient DefaultIndexer { get; set; } = GetSandboxIndexerClient();

    public static T GetJson<T>(string propertyPath, T defaultVal)
    {
        var json = GetString(propertyPath);
        return string.IsNullOrEmpty(json) ? defaultVal : JsonUtility.FromJson<T>(json);
    }

    public static Optional<T> GetJson<T>(string propertyPath)
        where T : struct, System.IEquatable<T>
    {
        var json = GetString(propertyPath);
        return string.IsNullOrEmpty(json) ? default : JsonUtility.FromJson<T>(json);
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

    private static AlgodClient GetSandboxAlgodClient()
    {
        return new AlgodClient(
            address: "http://localhost:4001",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
    }

    private static IndexerClient GetSandboxIndexerClient()
    {
        return new IndexerClient(address: "http://localhost:8980");
    }

    private static KmdClient GetSandboxKmdClient()
    {
        return new KmdClient(
            address: "http://localhost:4002",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
    }
}

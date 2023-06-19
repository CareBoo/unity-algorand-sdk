using Algorand.Unity;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class AlgoApiClientSettings
{
    public static IAlgodClient Algod { get; set; } = GetLocalAlgodClient();
    public static IIndexerClient Indexer { get; set; } = GetLocalIndexerClient();
    public static KmdClient Kmd { get; set; } = GetLocalKmdClient();

    public static AlgodClient DefaultAlgod { get; set; } = GetLocalAlgodClient();
    public static IndexerClient DefaultIndexer { get; set; } = GetLocalIndexerClient();

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

    private static AlgodClient GetLocalAlgodClient()
    {
        return new AlgodClient(
            address: "http://localhost:4001",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
    }

    private static IndexerClient GetLocalIndexerClient()
    {
        return new IndexerClient(address: "http://localhost:8980");
    }

    private static KmdClient GetLocalKmdClient()
    {
        return new KmdClient(
            address: "http://localhost:4002",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
    }
}

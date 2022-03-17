using AlgoSdk;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class AlgoApiClientSettings
{
    public static AlgodClient Algod => GetJson<AlgodClient>(nameof(Algod));

    public static IndexerClient Indexer => GetJson<IndexerClient>(nameof(Indexer));

    public static KmdClient Kmd => GetJson<KmdClient>(nameof(Kmd));

    public static T GetJson<T>(string propertyPath)
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
}

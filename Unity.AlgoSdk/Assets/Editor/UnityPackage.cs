using UnityEditor;

public static class UnityPackage
{
    const string packageName = "unity-algorand-sdk";

    static readonly string[] exportedAssets = new[]
    {
        "Packages/com.careboo.unity-algorand-sdk",
        "Packages/com.cysharp.unitask"
    };

    [MenuItem("AlgoSdk/Package")]
    public static void Build()
    {
        AssetDatabase.ExportPackage(
            assetPathNames: exportedAssets,
            fileName: packageName + ".unitypackage",
            ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies
        );
    }
}

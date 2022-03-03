using UnityEditor;

public static class UnityPackage
{
    const string packageName = "unity-algorand-sdk";

    static readonly string[] exportedAssets = new[]
    {
        "Assets/AlgoSdk",
        "Assets/UniTask"
    };

    public static void Build()
    {
        AssetDatabase.ExportPackage(
            assetPathNames: exportedAssets,
            fileName: packageName + ".unitypackage",
            ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies
        );
    }
}

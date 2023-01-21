using UnityEditor;

public static class UnityPackage
{
    const string packageName = "unity-algorand-sdk";

    private static readonly string[] exportPaths =
    {
        "Packages/com.careboo.unity-algorand-sdk",
        "Packages/com.cysharp.unitask"
    };

    static readonly ExportPackageOptions exportOptions = ExportPackageOptions.Recurse;


    [MenuItem("Algorand.Unity/Package")]
    public static void Build()
    {
        AssetDatabase.ExportPackage(
            assetPathNames: exportPaths,
            fileName: $"{packageName}.unitypackage",
            exportOptions
        );
    }
}

using UnityEditor;
using UnityEngine;

public static class UnityPackage
{
    const string packageName = "unity-algorand-sdk";

    static readonly ExportPackageOptions exportOptions = ExportPackageOptions.Recurse
        | ExportPackageOptions.IncludeDependencies
        ;


    [MenuItem("AlgoSdk/Package")]
    public static void Build()
    {
        AssetDatabase.ExportPackage(
            assetPathName: "Assets/AlgoSdk",
            fileName: packageName + ".unitypackage",
            exportOptions
        );
    }
}

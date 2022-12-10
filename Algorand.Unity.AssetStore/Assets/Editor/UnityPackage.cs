using UnityEditor;
using UnityEngine;

public static class UnityPackage
{
    const string packageName = "unity-algorand-sdk";

    static readonly ExportPackageOptions exportOptions = ExportPackageOptions.Recurse;


    [MenuItem("Algorand.Unity/Package")]
    public static void Build()
    {
        AssetDatabase.ExportPackage(
            assetPathName: "Assets/Algorand.Unity",
            fileName: packageName + ".unitypackage",
            exportOptions
        );
    }
}

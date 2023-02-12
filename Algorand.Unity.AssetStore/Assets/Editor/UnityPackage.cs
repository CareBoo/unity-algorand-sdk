using Algorand.Unity.Readme.Editor;
using UnityEditor;
using UnityEngine;

public static class UnityPackage
{
    const string packageName = "unity-algorand-sdk";

    private static readonly string[] exportPaths =
    {
        "Assets/Algorand.Unity",
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

    [MenuItem("Algorand.Unity/Create README asset")]
    public static void CreateReadme()
    {
        var readme = ScriptableObject.CreateInstance<ReadmeFile>();
        
        AssetDatabase.CreateAsset(readme, "Assets/Algorand.Unity/README.asset");
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = readme;
    }
}

using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public static class UnityPackage
{
    const string packageName = "unity-algorand-sdk";

    static readonly ImportAssetOptions refreshOptions = ImportAssetOptions.ForceUpdate
        | ImportAssetOptions.ImportRecursive
        | ImportAssetOptions.ForceSynchronousImport
        ;

    static readonly ExportPackageOptions exportOptions = ExportPackageOptions.Recurse
        | ExportPackageOptions.IncludeDependencies
        ;

    [MenuItem("AlgoSdk/Embed UniTask")]
    public static void EmbedUniTask()
    {
        var packageInfo = Client.Embed("com.cysharp.unitask").Result;
        RefreshAssets();
    }

    [MenuItem("AlgoSdk/Package")]
    public static void Build()
    {
        EmbedUniTask();
        MovePackagesIntoAssets();
        AssetDatabase.ExportPackage(
            assetPathName: "Assets/AlgoSdk",
            fileName: packageName + ".unitypackage",
            exportOptions
        );
        AssetDatabase.Refresh(refreshOptions);
        MovePackagesBackIntoPackages();
    }

    static void MovePackagesIntoAssets()
    {
        MoveAsset("Packages/com.careboo.unity-algorand-sdk", "Assets/AlgoSdk");

        CreateFolder("Assets/AlgoSdk", "Third Party");

        MoveAsset("Packages/com.cysharp.unitask", "Assets/AlgoSdk/Third Party/UniTask");
        MoveAsset("Assets/AlgoSdk/Runtime/websocket-sharp", "Assets/AlgoSdk/Third Party/websocket-sharp");
        MoveAsset("Assets/AlgoSdk/Runtime/zxing.unity", "Assets/AlgoSdk/Third Party/zxing.unity");
        MoveAsset("Assets/Samples", "Assets/AlgoSdk/Samples");
    }

    static void MovePackagesBackIntoPackages()
    {
        MoveAsset("Assets/AlgoSdk/Samples", "Assets/Samples");
        MoveAsset("Assets/AlgoSdk/Third Party/UniTask", "Packages/com.cysharp.unitask");
        MoveAsset("Assets/AlgoSdk/Third Party/websocket-sharp", "Assets/AlgoSdk/Runtime/websocket-sharp");
        MoveAsset("Assets/AlgoSdk/Third Party/zxing.unity", "Assets/AlgoSdk/Runtime/zxing.unity");

        DeleteAsset("Assets/AlgoSdk/Third Party");

        MoveAsset("Assets/AlgoSdk", "Packages/com.careboo.unity-algorand-sdk");
    }

    static void MoveAsset(string from, string to)
    {
        var error = AssetDatabase.MoveAsset(from, to);
        if (string.IsNullOrEmpty(error))
            RefreshAssets();
        else
            Debug.LogError(error);
    }

    static void CreateFolder(string parent, string folderName)
    {
        var error = AssetDatabase.CreateFolder(parent, folderName);
        if (string.IsNullOrEmpty(error))
            RefreshAssets();
        else
            Debug.LogError(error);
    }

    static void DeleteAsset(string path)
    {
        var deleted = AssetDatabase.DeleteAsset(path);
        if (deleted)
            RefreshAssets();
        else
            Debug.LogError($"Could not delete asset at path: {path}");
    }

    static void RefreshAssets()
    {
        AssetDatabase.Refresh(refreshOptions);
    }
}

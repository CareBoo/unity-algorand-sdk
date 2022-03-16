using UnityEditor;

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

    [MenuItem("AlgoSdk/Package")]
    public static void Build()
    {
        MovePackagesIntoAssets();
        AssetDatabase.ExportPackage(
            assetPathName: "Assets/AlgoSdk",
            fileName: packageName + ".unitypackage",
            exportOptions
        );
        MovePackagesBackIntoPackages();
    }

    static void MovePackagesIntoAssets()
    {
        AssetDatabase.MoveAsset("Packages/com.careboo.unity-algorand-sdk", "Assets/AlgoSdk");
        AssetDatabase.Refresh(refreshOptions);

        AssetDatabase.CreateFolder("Assets/AlgoSdk", "Third Party");
        AssetDatabase.Refresh(refreshOptions);

        AssetDatabase.MoveAsset("Packages/com.cysharp.unitask", "Assets/AlgoSdk/Third Party/UniTask");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Runtime/websocket-sharp", "Assets/AlgoSdk/Third Party/websocket-sharp");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Runtime/zxing.unity", "Assets/AlgoSdk/Third Party/zxing.unity");
        AssetDatabase.MoveAsset("Assets/Samples", "Assets/AlgoSdk/Samples");
        AssetDatabase.Refresh(refreshOptions);
    }

    static void MovePackagesBackIntoPackages()
    {
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Samples", "Assets/Samples");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Third Party/UniTask", "Packages/com.cysharp.unitask");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Third Party/websocket-sharp", "Assets/AlgoSdk/Runtime/websocket-sharp");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Third Party/zxing.unity", "Assets/AlgoSdk/Runtime/zxing.unity");
        AssetDatabase.Refresh(refreshOptions);

        AssetDatabase.DeleteAsset("Assets/AlgoSdk/Third Party");
        AssetDatabase.Refresh(refreshOptions);

        AssetDatabase.MoveAsset("Assets/AlgoSdk", "Packages/com.careboo.unity-algorand-sdk");
        AssetDatabase.Refresh(refreshOptions);
    }
}

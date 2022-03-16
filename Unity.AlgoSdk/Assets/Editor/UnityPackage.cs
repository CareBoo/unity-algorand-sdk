using UnityEditor;

public static class UnityPackage
{
    const string packageName = "unity-algorand-sdk";

    [MenuItem("AlgoSdk/Package")]
    public static void Build()
    {
        MovePackagesIntoAssets();
        AssetDatabase.Refresh();
        AssetDatabase.ExportPackage(
            assetPathName: "Assets/AlgoSdk",
            fileName: packageName + ".unitypackage",
            ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies
        );
        AssetDatabase.Refresh();
        MovePackagesBackIntoPackages();
        AssetDatabase.Refresh();
    }

    static void MovePackagesIntoAssets()
    {
        AssetDatabase.MoveAsset("Packages/com.careboo.unity-algorand-sdk", "Assets/AlgoSdk");
        AssetDatabase.CreateFolder("Assets/AlgoSdk/Third Party", "Third Party");

        AssetDatabase.MoveAsset("Packages/com.cysharp.unitask", "Assets/AlgoSdk/Third Party/UniTask");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Runtime/websocket-sharp", "Assets/AlgoSdk/Third Party/websocket-sharp");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Runtime/zxing.unity", "Assets/AlgoSdk/Third Party/zxing.unity");
        AssetDatabase.MoveAsset("Assets/Samples", "Assets/AlgoSdk/Samples");
    }

    static void MovePackagesBackIntoPackages()
    {
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Samples", "Assets/Samples");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Third Party/UniTask", "Packages/com.cysharp.unitask");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Third Party/websocket-sharp", "Assets/AlgoSdk/Runtime/websocket-sharp");
        AssetDatabase.MoveAsset("Assets/AlgoSdk/Third Party/zxing.unity", "Assets/AlgoSdk/Runtime/zxing.unity");

        AssetDatabase.DeleteAsset("Assets/AlgoSdk/Third Party");
        AssetDatabase.MoveAsset("Assets/AlgoSdk", "Packages/com.careboo.unity-algorand-sdk");
    }
}

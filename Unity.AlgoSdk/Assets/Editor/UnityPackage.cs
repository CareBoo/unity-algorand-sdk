using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public static class UnityPackage
{
    const string packageName = "unity-algorand-sdk";

    static string projectPath => UnityEngine.Application.persistentDataPath.Substring(0, UnityEngine.Application.persistentDataPath.Length - "Assets".Length);

    static readonly ImportAssetOptions refreshOptions = ImportAssetOptions.ImportRecursive
        | ImportAssetOptions.ForceSynchronousImport
        ;

    static readonly ExportPackageOptions exportOptions = ExportPackageOptions.Recurse
        | ExportPackageOptions.IncludeDependencies
        ;

    [MenuItem("AlgoSdk/Embed UniTask")]
    public static void EmbedUniTask()
    {
        EmbedUniTaskAsync().Forget();
    }

    [MenuItem("AlgoSdk/Package")]
    public static void Build()
    {
        BuildAsync().Forget();
    }

    static async UniTaskVoid BuildAsync()
    {
        var isEmbedded = await EmbedUniTaskAsync();
        if (!isEmbedded)
            return;

        MovePackagesIntoAssets();
        await ReinstallPackagesInAssets();
        AssetDatabase.ExportPackage(
            assetPathName: "Assets/AlgoSdk",
            fileName: packageName + ".unitypackage",
            exportOptions
        );
        AssetDatabase.Refresh(refreshOptions);
        MovePackagesBackIntoPackages();
        await ReinstallPackagesInPackages();
    }

    static async UniTask<bool> EmbedUniTaskAsync()
    {
        var embedRequest = Client.Embed("com.cysharp.unitask");
        await UniTask.WaitUntil(() => embedRequest.IsCompleted);
        if (embedRequest.Error != null)
            Debug.LogError(embedRequest.Error.message);
        else
            RefreshAssets();
        return embedRequest.Error == null;
    }

    static async UniTask ReinstallPackagesInAssets()
    {
        var addRequest = Client.Add($"file:{UnityEngine.Application.persistentDataPath}/AlgoSdk");
        await UniTask.WaitUntil(() => addRequest.IsCompleted);
        if (addRequest.Error != null)
            Debug.LogError(addRequest.Error.message);
        addRequest = Client.Add($"file:{UnityEngine.Application.persistentDataPath}/AlgoSdk/Third Party/UniTask");
        await UniTask.WaitUntil(() => addRequest.IsCompleted);
        if (addRequest.Error != null)
            Debug.LogError(addRequest.Error.message);
    }

    static async UniTask ReinstallPackagesInPackages()
    {
        var addRequest = Client.Add($"file:com.careboo.unity-algorand-sdk");
        await UniTask.WaitUntil(() => addRequest.IsCompleted);
        if (addRequest.Error != null)
            Debug.LogError(addRequest.Error.message);
        addRequest = Client.Add($"file:com.cysharp.unitask");
        await UniTask.WaitUntil(() => addRequest.IsCompleted);
        if (addRequest.Error != null)
            Debug.LogError(addRequest.Error.message);
    }

    static void MovePackagesIntoAssets()
    {
        FileUtil.MoveFileOrDirectory("Packages/com.careboo.unity-algorand-sdk", "Assets/AlgoSdk");
        RefreshAssets();

        CreateFolder("Assets/AlgoSdk", "Third Party");

        FileUtil.MoveFileOrDirectory("Packages/com.cysharp.unitask", "Assets/AlgoSdk/Third Party/UniTask");
        RefreshAssets();

        MoveAsset("Assets/AlgoSdk/Runtime/websocket-sharp", "Assets/AlgoSdk/Third Party/websocket-sharp");
        MoveAsset("Assets/AlgoSdk/Runtime/zxing.unity", "Assets/AlgoSdk/Third Party/zxing.unity");
        MoveAsset("Assets/Samples", "Assets/AlgoSdk/Samples");
    }

    static void MovePackagesBackIntoPackages()
    {
        MoveAsset("Assets/AlgoSdk/Samples", "Assets/Samples");
        MoveAsset("Assets/AlgoSdk/Third Party/websocket-sharp", "Assets/AlgoSdk/Runtime/websocket-sharp");
        MoveAsset("Assets/AlgoSdk/Third Party/zxing.unity", "Assets/AlgoSdk/Runtime/zxing.unity");

        FileUtil.MoveFileOrDirectory("Assets/AlgoSdk/Third Party/UniTask", "Packages/com.cysharp.unitask");
        RefreshAssets();

        DeleteAsset("Assets/AlgoSdk/Third Party");

        FileUtil.MoveFileOrDirectory("Assets/AlgoSdk", "Packages/com.careboo.unity-algorand-sdk");
        RefreshAssets();
    }

    static void MoveAsset(string from, string to)
    {
        FileUtil.MoveFileOrDirectory(from, to);
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

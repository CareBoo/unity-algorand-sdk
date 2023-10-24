using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi.Editor
{
    [ScriptedImporter(1, "interface.json")]
    public class InterfaceImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var fileName = Path.GetFileNameWithoutExtension(ctx.assetPath);
            var json = File.ReadAllText(ctx.assetPath);
            var asset = ScriptableObject.CreateInstance<InterfaceAsset>();
            asset.@interface = AlgoApiSerializer.DeserializeJson<Interface>(json);
            asset.name = fileName;
            ctx.AddObjectToAsset("interface", asset);
            ctx.SetMainObject(asset);
        }
    }
}

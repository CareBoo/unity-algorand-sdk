using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Algorand.Unity.Samples.CallingSmartContractAbi.Editor
{
    [ScriptedImporter(1, "teal")]
    public class TealImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var text = File.ReadAllText(ctx.assetPath);
            var textAsset = new TextAsset(text);
            ctx.AddObjectToAsset("textAsset", textAsset);
        }
    }
}
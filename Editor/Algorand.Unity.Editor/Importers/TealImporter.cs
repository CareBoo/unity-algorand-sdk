using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using System.IO;

namespace Algorand.Unity.Editor
{
    [ScriptedImporter(1, "teal")]
    public class TealImporter : ScriptedImporter
    {
        public const string IconGuid = "66494db6610044e4db0e8cd7faa194af";

        private static Texture2D Thumbnail => AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(IconGuid));

        public override void OnImportAsset(AssetImportContext ctx)
        {
            var fileName = Path.GetFileNameWithoutExtension(ctx.assetPath);
            var text = File.ReadAllText(ctx.assetPath);
            var script = new TextAsset(text);
            script.name = fileName;
            ctx.AddObjectToAsset("script", script, Thumbnail);
            ctx.SetMainObject(script);
        }
    }
}

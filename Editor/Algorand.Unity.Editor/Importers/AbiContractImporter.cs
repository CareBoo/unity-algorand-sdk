using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi.Editor
{
    [ScriptedImporter(1, "contract.json")]
    public class AbiContractImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var fileName = Path.GetFileNameWithoutExtension(ctx.assetPath);
            var json = File.ReadAllText(ctx.assetPath);
            var asset = ScriptableObject.CreateInstance<ContractAsset>();
            asset.contract = AlgoApiSerializer.DeserializeJson<Contract>(json);
            asset.name = fileName;
            ctx.AddObjectToAsset("contract", asset);
            ctx.SetMainObject(asset);
        }
    }
}

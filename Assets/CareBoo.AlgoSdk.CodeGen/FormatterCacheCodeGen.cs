using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using Microsoft.CSharp;
using UnityEditor;
using UnityEngine;

namespace AlgoSdk.Editor.CodeGen
{
    public class FormatterCacheCodeGen
    {
        const string OutputFileName = "AlgoApiFormatters.gen.cs";

        static string PathToProject => Path.GetPathRoot(UnityEngine.Application.dataPath);

        [MenuItem("AlgoSdk/GenerateFormatterCache")]
        public static void GenerateFormatterCache()
        {

            var algoApiObjCompileUnits = TypeCache.GetTypesWithAttribute(typeof(AlgoApiObjectAttribute))
                .OrderBy(t => t.Name)
                .Where(t => !t.IsNested)
                .Select(t => new AlgoApiObjectCompileUnit(t))
                .Cast<AlgoApiCompileUnit>()
                ;
            var algoApiFormatterCompileUnits = TypeCache.GetTypesWithAttribute(typeof(AlgoApiFormatterAttribute))
                .OrderBy(t => t.Name)
                .Where(t => !t.IsNested)
                .Select(t => new AlgoApiFormatterCompileUnit(t))
                .Cast<AlgoApiCompileUnit>()
                ;

            foreach (var compileUnit in algoApiObjCompileUnits.Concat(algoApiFormatterCompileUnits).Where(c => c.CompileUnit != null))
            {
                var createdPath = ExportToDirectory(compileUnit);
                if (createdPath == null) continue;
                var relPath = Path.GetRelativePath(PathToProject, createdPath);
                AssetDatabase.ImportAsset(relPath, ImportAssetOptions.ForceUpdate);
            }
            AssetDatabase.Refresh();
        }

        static string ExportToDirectory(AlgoApiCompileUnit compileUnit)
        {
            try
            {
                var sourcePath = compileUnit.SourceInfo.AbsoluteFilePath;
                var sourceDir = Path.GetDirectoryName(sourcePath);
                var filenameWithoutExtension = Path.GetFileNameWithoutExtension(sourcePath);
                var outputPath = Path.Combine(sourceDir, $"{filenameWithoutExtension}.{OutputFileName}");
                var codeProvider = new CSharpCodeProvider();
                using var stream = new StreamWriter(outputPath, append: false);
                var tw = new IndentedTextWriter(stream);
                var options = new CodeGeneratorOptions();
                options.BracingStyle = "C";
                codeProvider.GenerateCodeFromCompileUnit(compileUnit.CompileUnit, tw, options);
                return outputPath;
            }
            catch (Exception ex)
            {
                Debug.LogError($"got error while exporting type {compileUnit.Type}: {ex}");
                return null;
            }
        }
    }
}

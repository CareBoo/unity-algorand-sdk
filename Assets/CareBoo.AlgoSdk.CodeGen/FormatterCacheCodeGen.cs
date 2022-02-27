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

        static string PathToProject => Path.GetDirectoryName(UnityEngine.Application.dataPath);

        [MenuItem("AlgoSdk/GenerateFormatterCache")]
        public static void GenerateFormatterCache()
        {
            var createdFiles = TypeCache.GetTypesWithAttribute(typeof(AlgoApiObjectAttribute))
                .Concat(TypeCache.GetTypesWithAttribute(typeof(AlgoApiFormatterAttribute)))
                .Select(t => new AlgoApiCompileUnit(t))
                .Where(cu => cu.IsValid)
                .Select(ExportToDirectory)
                .Where(filePath => filePath != null)
                ;

            foreach (var filePath in createdFiles)
            {
                var relPath = Path.GetRelativePath(PathToProject, filePath);
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
                codeProvider.GenerateCodeFromCompileUnit(compileUnit, tw, options);
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

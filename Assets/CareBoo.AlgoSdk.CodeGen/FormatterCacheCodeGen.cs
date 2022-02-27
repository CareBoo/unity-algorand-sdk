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
                .Select(t => new AlgoApiObjectCompileUnit(t))
                .Cast<AlgoApiCompileUnit>()
                ;
            var algoApiFormatterCompileUnits = TypeCache.GetTypesWithAttribute(typeof(AlgoApiFormatterAttribute))
                .OrderBy(t => t.Name)
                .Select(t => new AlgoApiFormatterCompileUnit(t))
                .Cast<AlgoApiCompileUnit>()
                ;

            foreach (var compileUnit in algoApiObjCompileUnits.Concat(algoApiFormatterCompileUnits))
            {
                var createdPath = ExportToDirectory(compileUnit);
                var relPath = Path.GetRelativePath(PathToProject, createdPath);
                AssetDatabase.ImportAsset(relPath, ImportAssetOptions.ForceUpdate);
            }
            AssetDatabase.Refresh();
        }

        static string ExportToDirectory(AlgoApiCompileUnit compileUnit)
        {
            if (compileUnit.CompileUnit == null)
            {
                Debug.Log($"Skipping exporting compile unit for type {compileUnit.Type.FullName} because could not generate its compile unit.");
            }

            var sourcePath = compileUnit.SourceInfo.AbsoluteFilePath;
            Debug.Log($"Found attribute at {sourcePath}");
            Debug.Log($"DirectoryName: {Path.GetDirectoryName(sourcePath)}");
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
    }
}

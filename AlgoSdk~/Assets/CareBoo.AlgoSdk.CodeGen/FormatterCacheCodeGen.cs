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

        [MenuItem("AlgoSdk/GenerateFormatterCache")]
        public static void GenerateFormatterCache()
        {
            var createdFiles = TypeCache.GetTypesWithAttribute(typeof(AlgoApiObjectAttribute))
                .Concat(TypeCache.GetTypesWithAttribute(typeof(AlgoApiFormatterAttribute)))
                .Select(t => new AlgoApiCompileUnit(t))
                .Where(cu => cu.IsValid)
                .GroupBy(cu => cu.SourceInfo.AbsoluteFilePath)
                .Select(grouping => grouping.Aggregate(MergeCompileUnit))
                .Select(ExportToDirectory)
                .Where(filePath => filePath != null)
                .ToArray()
                ;

            AssetDatabase.Refresh();
        }

        static AlgoApiCompileUnit MergeCompileUnit(AlgoApiCompileUnit cu1, AlgoApiCompileUnit cu2)
        {
            cu1.Namespaces.AddRange(cu2.Namespaces);
            return cu1;
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

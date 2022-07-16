using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using Microsoft.CSharp;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace AlgoSdk.Editor.CodeGen
{
    public class FormatterCacheCodeGen
    {
        const string OutputFileName = "Formatters.gen.cs";
        const string FolderName = "Formatters.gen";

        [MenuItem("AlgoSdk/Generate Formatter Cache")]
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
                var formatterDir = GetFormatterDir(compileUnit);
                var filenameWithoutExtension = Path.GetFileNameWithoutExtension(sourcePath);
                var outputPath = Path.Combine(formatterDir, $"{filenameWithoutExtension}.{OutputFileName}");
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

        static string GetFormatterDir(AlgoApiCompileUnit compileUnit)
        {
            var assemblyPath = CompilationPipeline.GetAssemblyDefinitionFilePathFromScriptPath(compileUnit.SourceInfo.AssetPath);
            var assemblyDir = Path.GetDirectoryName(assemblyPath);
            var formatterDir = Path.Combine(assemblyDir, FolderName);
            if (!AssetDatabase.IsValidFolder(formatterDir))
            {
                AssetDatabase.CreateFolder(assemblyDir, FolderName);
            }
            return Path.Combine(Application.dataPath, "..", formatterDir);
        }
    }
}

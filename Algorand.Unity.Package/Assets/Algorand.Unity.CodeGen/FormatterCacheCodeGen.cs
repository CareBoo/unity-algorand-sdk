using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using Microsoft.CSharp;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Algorand.Unity.Editor.CodeGen
{
    public class FormatterCacheCodeGen
    {
        private const string OutputFileName = "Formatters.gen.cs";
        private const string FolderName = "Formatters.gen";

        [MenuItem("Algorand.Unity/Generate Formatter Cache")]
        public static void GenerateFormatterCache()
        {
            var createdFiles = TypeCache.GetTypesWithAttribute(typeof(AlgoApiObjectAttribute))
                .Concat(TypeCache.GetTypesWithAttribute(typeof(AlgoApiFormatterAttribute)))
                .Select(t => new AlgoApiCompileUnit(t))
                .Where(cu => cu.IsValid)
                .OrderBy(cu => cu.Type.Name)
                .GroupBy(cu => cu.SourceInfo.FilePath)
                .Select(grouping => grouping.Aggregate(MergeCompileUnit))
                .Select(ExportToDirectory)
                .Where(filePath => filePath != null)
                .ToArray()
                ;

            AssetDatabase.Refresh();
        }

        private static AlgoApiCompileUnit MergeCompileUnit(AlgoApiCompileUnit cu1, AlgoApiCompileUnit cu2)
        {
            var namespaces1 = cu1.Namespaces;
            var namespaces2 = cu2.Namespaces;

            foreach (CodeNamespace ns2 in namespaces2)
            {
                var existsAlready = false;
                foreach (CodeNamespace ns1 in namespaces1)
                {
                    if (ns1.Name == ns2.Name)
                    {
                        existsAlready = true;
                        ns1.Types.AddRange(ns2.Types);
                        break;
                    }
                }
                if (!existsAlready)
                {
                    namespaces1.Add(ns2);
                }
            }
            return cu1;
        }

        private static string ExportToDirectory(AlgoApiCompileUnit compileUnit)
        {
            try
            {
                var sourcePath = compileUnit.SourceInfo.FilePath;
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

        private static string GetFormatterDir(AlgoApiCompileUnit compileUnit)
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

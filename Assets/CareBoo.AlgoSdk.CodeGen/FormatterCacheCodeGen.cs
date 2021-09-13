using UnityEngine;
using System.CodeDom;
using UnityEditor;
using System.Linq;
using System.Reflection;
using AlgoSdk.MsgPack;
using System.Collections.Generic;
using System;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace AlgoSdk.Editor.CodeGen
{
    public class FormatterCacheCodeGen
    {
        const string outputFileName = "AlgoApiFormatterLookup.codegen.cs";
        CodeCompileUnit targetUnit;
        CodeTypeDeclaration targetClass;
        CodeFieldReferenceExpression lookupField;

        public FormatterCacheCodeGen()
        {
            targetUnit = new CodeCompileUnit();
            CodeNamespace ns = new CodeNamespace(typeof(AlgoApiFormatterLookup).Namespace);
            targetClass = new CodeTypeDeclaration(nameof(AlgoApiFormatterLookup));
            targetClass.IsClass = true;
            targetClass.IsPartial = true;
            targetClass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            ns.Types.Add(targetClass);
            targetUnit.Namespaces.Add(ns);
            lookupField = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(AlgoApiFormatterLookup)), AlgoApiFormatterLookup.LookupFieldName);
        }

        public void CreateEnsureFormatterLookup(IEnumerable<Type> algoApiObjTypes)
        {
            var ensureLookupsMethod = new CodeMemberMethod();
            ensureLookupsMethod.Name = AlgoApiFormatterLookup.EnsureLookupMethodName;
            ensureLookupsMethod.Attributes = MemberAttributes.Private | MemberAttributes.Static;
            ensureLookupsMethod.ReturnType = new CodeTypeReference(typeof(void));
            var initLookupMethod = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(AlgoApiFormatterLookup)), AlgoApiFormatterLookup.InitLookupMethodName, new CodeExpression[0]);
            ensureLookupsMethod.Statements.Add(initLookupMethod);
            foreach (var algoApiObjType in algoApiObjTypes)
                ensureLookupsMethod.Statements.Add(GetLookupAddExpressionForAlgoApiObjType(algoApiObjType));

            targetClass.Members.Add(ensureLookupsMethod);
        }

        public string ExportToDirectory(string dirPath)
        {
            var codeProvider = new CSharpCodeProvider();
            var filePath = Path.Combine(dirPath, outputFileName);
            using var stream = new StreamWriter(filePath, append: false);
            var tw = new IndentedTextWriter(stream);
            var options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            codeProvider.GenerateCodeFromCompileUnit(targetUnit, tw, options);
            return filePath;
        }

        CodeExpression GetLookupAddExpressionForAlgoApiObjType(Type algoApiObjType)
        {
            var createFormatterExpression = new CodeObjectCreateExpression(
                    typeof(AlgoApiObjectFormatter<>).MakeGenericType(algoApiObjType),
                    GetCreateMapExpression(algoApiObjType));

            return new CodeMethodInvokeExpression(
                lookupField,
                nameof(Dictionary<Type, object>.Add),
                new CodeTypeOfExpression(new CodeTypeReference(algoApiObjType)),
                createFormatterExpression);
        }

        CodeExpression GetCreateMapExpression(Type algoApiObjType)
        {
            var fieldKeys = GetKeyFields(algoApiObjType);
            CodeExpression createdMapExpression = new CodeObjectCreateExpression(
                typeof(AlgoApiField<>.Map).MakeGenericType(algoApiObjType));
            foreach (var (key, field) in fieldKeys)
            {
                createdMapExpression = new CodeMethodInvokeExpression(
                    createdMapExpression,
                    nameof(AlgoApiField<int>.Map.Assign),
                    GetAssignParamsExpressions(key, field));
            }
            return createdMapExpression;
        }

        CodeExpression[] GetAssignParamsExpressions(string key, FieldInfo field)
        {
            var expressions = new List<CodeExpression>()
            {
                new CodePrimitiveExpression(key),
                new CodeSnippetExpression($"(ref {field.DeclaringType.ToString().Replace('+', '.')} x) => ref x.{field.Name}")
            };
            if (field.FieldType.GetInterfaces().All(t => t != typeof(IEquatable<>).MakeGenericType(field.FieldType)))
            {
                var equalityComparerType = field.FieldType.IsArray
                    ? typeof(ArrayComparer<>).MakeGenericType(field.FieldType.GetElementType())
                    : equalityComparerLookup[field.FieldType]
                    ;
                var equalityComparer = new CodePropertyReferenceExpression(
                    new CodeTypeReferenceExpression(equalityComparerType),
                    "Instance");
                expressions.Add(equalityComparer);
            }
            return expressions.ToArray();
        }

        List<(string, FieldInfo)> GetKeyFields(Type algoApiObjType)
        {
            var fieldKeys = new List<(string, FieldInfo)>();
            foreach (var field in algoApiObjType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var keyAttr = (AlgoApiKeyAttribute)field
                    .GetCustomAttributes()
                    .SingleOrDefault(a => a.GetType() == typeof(AlgoApiKeyAttribute));
                if (keyAttr != null)
                    fieldKeys.Add((keyAttr.KeyName, field));
            }
            return fieldKeys;
        }

        [MenuItem("AlgoSdk/GenerateFormatterCache")]
        public static void GenerateFormatterCache()
        {
            var algoApiObjTypes = TypeCache.GetTypesWithAttribute(typeof(AlgoApiObjectAttribute)).OrderBy(t => t.Name);
            Debug.Log(Format(algoApiObjTypes));
            var dirPath = UnityEngine.Application.dataPath;
            dirPath = dirPath.Substring(0, dirPath.Length - "Assets".Length);
            dirPath = Path.Combine(dirPath, "Packages/com.careboo.unity-algorand-sdk/CareBoo.AlgoSdk/AlgoApi");

            var codegen = new FormatterCacheCodeGen();
            codegen.CreateEnsureFormatterLookup(algoApiObjTypes);
            var createdFile = codegen.ExportToDirectory(dirPath);
            AssetDatabase.ImportAsset(createdFile, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();
        }

        static string Format(IEnumerable<Type> types)
        {
            return string.Join(",\n", types.Select((t, i) => $"{i}. {t}"));
        }

        static readonly Dictionary<Type, Type> equalityComparerLookup = new Dictionary<Type, Type>()
        {
            {typeof(string), typeof(MsgPack.StringComparer)},
            {typeof(EvalDeltaAction), typeof(EvalDeltaActionComparer)},
            {typeof(TransactionType), typeof(TransactionTypeComparer)},
            {typeof(SignatureType), typeof(SignatureTypeComparer)}
        };
    }
}

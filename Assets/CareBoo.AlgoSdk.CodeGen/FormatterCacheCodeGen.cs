using System.CodeDom;
using UnityEditor;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using AlgoSdk.Formatters;

namespace AlgoSdk.Editor.CodeGen
{
    public class FormatterCacheCodeGen
    {
        const string outputFileName = "AlgoApiFormatterLookup.gen.cs";
        CodeCompileUnit targetUnit;
        CodeTypeDeclaration targetClass;
        CodeFieldReferenceExpression lookupField;
        HashSet<Type> AddedTypes = new HashSet<Type>();

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

        public void CreateEnsureFormatterLookup(
            IEnumerable<Type> algoApiObjTypes,
            IEnumerable<Type> algoApiFormatterTypes)
        {
            var ensureLookupsMethod = new CodeMemberMethod();
            ensureLookupsMethod.Name = AlgoApiFormatterLookup.EnsureLookupMethodName;
            ensureLookupsMethod.Attributes = MemberAttributes.Private | MemberAttributes.Static;
            ensureLookupsMethod.ReturnType = new CodeTypeReference(typeof(void));
            var initLookupMethod = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(typeof(AlgoApiFormatterLookup)),
                AlgoApiFormatterLookup.InitLookupMethodName);
            ensureLookupsMethod.Statements.Add(initLookupMethod);
            foreach (var statement in algoApiObjTypes.SelectMany(GetLookupAddExpressionForAlgoApiObjType))
                ensureLookupsMethod.Statements.Add(statement);
            foreach (var statement in algoApiFormatterTypes.SelectMany(GetLookupAddExpressionForAlgoApiFormatterType))
                ensureLookupsMethod.Statements.Add(statement);

            targetClass.Members.Add(ensureLookupsMethod);
        }

        public string ExportToDirectory(string filePath)
        {
            var codeProvider = new CSharpCodeProvider();
            using var stream = new StreamWriter(filePath, append: false);
            var tw = new IndentedTextWriter(stream);
            var options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            codeProvider.GenerateCodeFromCompileUnit(targetUnit, tw, options);
            return filePath;
        }

        IEnumerable<CodeExpression> GetLookupAddExpressionForAlgoApiObjType(Type algoApiObjType)
        {
            var result = new List<CodeExpression>();
            var fieldKeys = GetKeyProps(algoApiObjType);
            var createFormatterExpression = new CodeObjectCreateExpression(
                    typeof(AlgoApiObjectFormatter<>).MakeGenericType(algoApiObjType),
                    GetCreateMapExpression(algoApiObjType, fieldKeys));

            if (AddedTypes.Add(algoApiObjType))
            {
                result.Add(
                   AddFormatterExpression(
                       algoApiObjType,
                       createFormatterExpression)
               );
            }
            foreach (var type in fieldKeys.Select(x => x.Item3).Where(x => x.IsArray))
            {
                createFormatterExpression = new CodeObjectCreateExpression(
                    typeof(ArrayFormatter<>).MakeGenericType(type.GetElementType())
                );
                if (AddedTypes.Add(type))
                {
                    result.Add(
                       AddFormatterExpression(
                           type,
                           createFormatterExpression
                       )
                   );
                }
            }
            return result;
        }

        IEnumerable<CodeExpression> GetLookupAddExpressionForAlgoApiFormatterType(Type algoApiFormatterType)
        {
            var result = new List<CodeExpression>();
            var formatterAttributes = algoApiFormatterType.GetCustomAttributes<AlgoApiFormatterAttribute>();
            foreach (var formatterType in formatterAttributes.Select(x => x.FormatterType))
            {
                if (algoApiFormatterType.IsGenericType)
                {
                    algoApiFormatterType = formatterType.GetInterfaces()
                        .Single(t => t.GetGenericTypeDefinition() == typeof(IAlgoApiFormatter<>))
                        .GetGenericArguments()
                        .First();
                }
                var typeofExpression = new CodeTypeOfExpression(new CodeTypeReference(algoApiFormatterType));
                CodeExpression formatterExpression = new CodeObjectCreateExpression(formatterType);
                if (AddedTypes.Add(algoApiFormatterType))
                {
                    result.Add(AddFormatterExpression(algoApiFormatterType, formatterExpression));
                }
            }
            return result;
        }

        CodeExpression AddFormatterExpression(Type type, CodeExpression formatterExpression)
        {
            return new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(typeof(AlgoApiFormatterLookup)),
                AlgoApiFormatterLookup.AddFormatterMethodName,
                new CodeTypeOfExpression(new CodeTypeReference(type)),
                formatterExpression);
        }

        CodeExpression GetCreateMapExpression(Type algoApiObjType, List<(string, MemberInfo, Type)> fieldKeys)
        {
            CodeExpression createdMapExpression = new CodeObjectCreateExpression(
                typeof(AlgoApiField<>.Map).MakeGenericType(algoApiObjType));
            foreach (var (key, member, type) in fieldKeys)
            {
                createdMapExpression = new CodeMethodInvokeExpression(
                    createdMapExpression,
                    nameof(AlgoApiField<int>.Map.Assign),
                    GetAssignParamsExpressions(key, member, type));
            }
            return createdMapExpression;
        }

        CodeExpression[] GetAssignParamsExpressions(string key, MemberInfo member, Type memberType)
        {
            var declaringType = member.DeclaringType;
            var memberName = member.Name;
            var expressions = new List<CodeExpression>()
            {
                new CodePrimitiveExpression(key),
                new CodeSnippetExpression($"({Format(declaringType)} x) => x.{memberName}"),
                new CodeSnippetExpression($"(ref {Format(declaringType)} x, {Format(memberType)} value) => x.{memberName} = value")
            };
            if (memberType.GetInterfaces().All(t => t != typeof(IEquatable<>).MakeGenericType(memberType)))
            {
                var equalityComparerType = memberType.IsArray
                    ? typeof(ArrayComparer<>).MakeGenericType(memberType.GetElementType())
                    : equalityComparerLookup[memberType]
                    ;
                var equalityComparer = new CodePropertyReferenceExpression(
                    new CodeTypeReferenceExpression(equalityComparerType),
                    "Instance");
                expressions.Add(equalityComparer);
            }
            return expressions.ToArray();
        }

        List<(string, MemberInfo, Type)> GetKeyProps(Type algoApiObjType)
        {
            var fields = algoApiObjType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(f => ((MemberInfo)f, f.FieldType));
            var props = algoApiObjType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(p => ((MemberInfo)p, p.PropertyType));
            return props.Concat(fields)
                .Select(GetKeyProp)
                .Where(x => x.Item1 != null)
                .ToList();
        }

        (string, MemberInfo, Type) GetKeyProp((MemberInfo, Type) prop)
        {
            return (KeyFromMember(prop.Item1), prop.Item1, prop.Item2);
        }

        string Format(Type type)
        {
            string name;
            if (type.IsGenericType)
            {
                string genericArguments = type.GetGenericArguments()
                                    .Select(Format)
                                    .Aggregate((x1, x2) => $"{x1}, {x2}");
                name = $"{type.FullName.Substring(0, type.FullName.IndexOf("`"))}<{genericArguments}>";
            }
            else if (type.IsArray)
            {
                string elementType = Format(type.GetElementType());
                name = $"{elementType}[]";
            }
            else
                name = type.FullName;
            return name.Replace('+', '.');
        }

        string KeyFromMember(MemberInfo member)
        {
            var keyAttr = member.GetCustomAttribute<AlgoApiKeyAttribute>();
            return keyAttr?.KeyName;
        }

        [MenuItem("AlgoSdk/GenerateFormatterCache")]
        public static void GenerateFormatterCache()
        {
            var relPath = Path.Combine("Packages/com.careboo.unity-algorand-sdk/CareBoo.AlgoSdk/AlgoApi/", outputFileName);
            var fullPath = UnityEngine.Application.dataPath;
            fullPath = fullPath.Substring(0, fullPath.Length - "Assets".Length);
            fullPath = Path.Combine(fullPath, relPath);

            var algoApiObjTypes = TypeCache.GetTypesWithAttribute(typeof(AlgoApiObjectAttribute)).OrderBy(t => t.Name);
            var algoFormatterTypes = TypeCache.GetTypesWithAttribute(typeof(AlgoApiFormatterAttribute)).OrderBy(t => t.Name);
            var codegen = new FormatterCacheCodeGen();
            codegen.CreateEnsureFormatterLookup(
                algoApiObjTypes,
                algoFormatterTypes);
            var createdFile = codegen.ExportToDirectory(fullPath);
            AssetDatabase.ImportAsset(relPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();
        }

        static string Format(IEnumerable<Type> types)
        {
            return string.Join(",\n", types.Select((t, i) => $"{i}. {t}"));
        }

        static readonly Dictionary<Type, Type> equalityComparerLookup = new Dictionary<Type, Type>()
        {
            {typeof(string), typeof(StringComparer)},
            {typeof(EvalDeltaAction), typeof(EvalDeltaActionComparer)},
            {typeof(TransactionType), typeof(TransactionTypeComparer)},
            {typeof(SignatureType), typeof(SignatureTypeComparer)}
        };
    }
}

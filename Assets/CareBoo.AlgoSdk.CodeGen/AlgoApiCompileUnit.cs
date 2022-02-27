using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlgoSdk.Formatters;

namespace AlgoSdk.Editor.CodeGen
{
    /// <summary>
    /// Extends a partial class or struct
    /// </summary>
    public abstract class AlgoApiCompileUnit
    {
        const string InitializeFormatterStaticMethodName = "__generated__InitializeAlgoApiFormatter";
        const string FormatterStaticFieldName = "__generated__DidGenerateFields";

        public Type Type { get; protected set; }
        public ProvideSourceInfoAttribute SourceInfo { get; protected set; }
        public CodeCompileUnit CompileUnit { get; protected set; }

        public AlgoApiCompileUnit(Type type)
        {
            Type = type;
            SourceInfo = GetSourceInfo(type);
            CompileUnit = GenCodeCompileUnit(type);
        }

        protected ProvideSourceInfoAttribute GetSourceInfo(Type type) =>
            type.GetCustomAttributes<ProvideSourceInfoAttribute>()
                .First()
                ;

        protected CodeCompileUnit GenCodeCompileUnit(Type type)
        {
            if (type.IsEnum)
                return null;
            var unit = new CodeCompileUnit();
            var ns = new CodeNamespace(type.Namespace);
            var targetType = ExtendPartialClassOrStructTypeDeclaration(type);
            targetType.Members.Add(FormatterStaticFieldExpression());
            targetType.Members.Add(InitializeFormatterStaticMethodExpression(type));
            ns.Types.Add(targetType);
            unit.Namespaces.Add(ns);
            return unit;
        }

        protected CodeTypeDeclaration ExtendEnumTypeDeclaration(Type type)
        {
            var targetType = new CodeTypeDeclaration($"__generated__{type.Name}__AlgoApiExtensions");
            return targetType;
        }

        protected CodeTypeDeclaration ExtendPartialClassOrStructTypeDeclaration(Type type)
        {
            var targetType = new CodeTypeDeclaration(type.Name);
            targetType.IsClass = type.IsClass;
            targetType.IsEnum = false;
            targetType.IsStruct = type.IsValueType;
            targetType.IsPartial = true;
            if (type.IsGenericType)
            {
                var typeParams = type.GetGenericArguments()
                    .Select((t, index) => $"T{index}")
                    .Select(s => new CodeTypeParameter(s))
                    ;
                foreach (var typeParam in typeParams)
                    targetType.TypeParameters.Add(typeParam);
            }
            return targetType;
        }

        protected CodeMemberField FormatterStaticFieldExpression()
        {
            var field = new CodeMemberField();
            field.Name = FormatterStaticFieldName;
            field.Attributes = MemberAttributes.Private | MemberAttributes.Static;
            field.Type = new CodeTypeReference(typeof(bool));
            field.InitExpression = new CodeMethodInvokeExpression(
                targetObject: new CodeThisReferenceExpression(),
                methodName: InitializeFormatterStaticMethodName
            );
            return field;
        }

        protected CodeMemberMethod InitializeFormatterStaticMethodExpression(Type type)
        {
            var method = new CodeMemberMethod();
            method.Name = InitializeFormatterStaticMethodName;
            method.Attributes = MemberAttributes.Private | MemberAttributes.Static | MemberAttributes.Final;
            method.ReturnType = new CodeTypeReference(typeof(bool));
            method.Statements.Add(AddFormatterExpression(type));
            method.Statements.Add(AddArrayFormatterExpression(type));
            foreach (var addEnumFormatterExpression in AddEnumFormatterExpressions(type))
                method.Statements.Add(addEnumFormatterExpression);
            method.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(true)));
            return method;
        }

        protected CodeExpression AddFormatterExpression(Type type) => GenAddFormatterExpression(type, ConstructFormatterExpression(type));

        protected CodeExpression AddArrayFormatterExpression(Type type)
        {
            var arrayType = type.MakeArrayType();
            return GenAddFormatterExpression(arrayType, GetArrayFormatterExpression(arrayType));
        }

        protected IEnumerable<CodeExpression> AddEnumFormatterExpressions(Type type) =>
            GetReferencedEnumFormatterExpressions(type)
                .Select(x => GenAddFormatterExpression(x.enumType, x.enumFormatterExpression));

        protected CodeExpression GenAddFormatterExpression(Type type, CodeExpression constructFormatterExpression)
        {
            return new CodeMethodInvokeExpression(
                targetObject: new CodeTypeReferenceExpression(typeof(AlgoApiFormatterLookup)),
                methodName: AlgoApiFormatterLookup.AddFormatterMethodName,
                // params
                new CodeTypeOfExpression(new CodeTypeReference(type)),
                constructFormatterExpression
            );
        }

        protected abstract CodeExpression ConstructFormatterExpression(Type type);

        /// <summary>
        /// Enums are a special case where we cannot add a static constructor,
        /// so we need to initialize them in parent objects.
        /// </summary>
        /// <param name="type">type which has reference enums.</param>
        /// <returns>A formatter expression for referencing in <see cref="GenAddFormatterExpression"/></returns>
        protected abstract IEnumerable<(Type enumType, CodeExpression enumFormatterExpression)> GetReferencedEnumFormatterExpressions(Type type);

        protected CodeExpression GetArrayFormatterExpression(Type arrayType)
        {
            return new CodeFieldReferenceExpression(
                targetObject: new CodeTypeReferenceExpression(typeof(ArrayFormatter<>).MakeGenericType(arrayType.GetElementType())),
                nameof(ArrayFormatter<int>.Instance)
            );
        }

        protected CodeExpression FormatterExpression(Type type)
        {
            var formatterAttribute = type.GetCustomAttributes<AlgoApiFormatterAttribute>().First();
            var formatterType = formatterAttribute.FormatterType;
            var formatterTypeReference = new CodeTypeReference(formatterType);
            if (formatterType.IsGenericTypeDefinition)
                formatterTypeReference.TypeArguments.AddRange(FormatterTypeParameters(type, formatterType));
            return new CodeObjectCreateExpression(formatterTypeReference);
        }

        protected CodeTypeReference[] FormatterTypeParameters(Type type, Type formatterType)
        {
            if (!formatterType.IsGenericTypeDefinition)
                return new CodeTypeReference[0];

            var formatterTypeArgumentCount = formatterType.GenericTypeArguments.Length;
            if (type.IsGenericType)
            {
                var typeArgs = new List<CodeTypeReference>();
                var typeArgumentCount = type.GenericTypeArguments.Length;
                var indexedTypeParams = GetIndexedTypeParameters(typeArgumentCount)
                    .Select(tp => new CodeTypeReference(tp))
                    .ToArray()
                    ;

                if (formatterTypeArgumentCount == typeArgumentCount + 1)
                    typeArgs.Add(new CodeTypeReference(type.Name, indexedTypeParams));
                typeArgs.AddRange(indexedTypeParams);
                return typeArgs.ToArray();
            }
            else if (formatterTypeArgumentCount == 1)
            {
                return new[] { new CodeTypeReference(type) };
            }
            throw new ArgumentException($"Got incorrect number of type arguments {formatterTypeArgumentCount} for formatter {formatterType.FullName}", nameof(formatterType));
        }

        protected IEnumerable<CodeTypeParameter> GetIndexedTypeParameters(int count) =>
            Enumerable.Range(0, count)
                .Select(i => $"T{i}")
                .Select(name => new CodeTypeParameter(name))
                ;
    }
}

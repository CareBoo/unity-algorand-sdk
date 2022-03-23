using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace AlgoSdk.Editor.CodeGen
{
    public class AlgoApiObjectFormatterCreateExpression
    {
        static readonly Dictionary<Type, Type> equalityComparerLookup = new Dictionary<Type, Type>()
        {
            {typeof(string), typeof(StringComparer)}
        };

        CodeExpression expression;

        public AlgoApiObjectFormatterCreateExpression(Type type, IEnumerable<AlgoApiObjectFieldKey> fields)
        {
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));

            var formatterTypeReference = new CodeTypeReference(typeof(AlgoApiObjectFormatter<>).SafeFullName(), new CodeTypeReference(type.FullNameExpression()));
            expression = new CodeObjectCreateExpression(formatterTypeReference);
            foreach (var field in fields)
            {
                expression = new CodeMethodInvokeExpression(
                    expression,
                    nameof(AlgoApiObjectFormatter<int>.Assign),
                    GetAssignParamsExpressions(type, field)
                );
            }
        }

        public static implicit operator CodeExpression(AlgoApiObjectFormatterCreateExpression src)
        {
            return src?.expression;
        }

        static CodeExpression[] GetAssignParamsExpressions(Type type, AlgoApiObjectFieldKey field)
        {
            var memberName = field.MemberInfo.Name;
            var memberType = field.MemberType;
            var expressions = new List<CodeExpression>()
            {
                new CodePrimitiveExpression(field.Attribute.JsonKeyName),
                new CodePrimitiveExpression(field.Attribute.MessagePackKeyName),
                new CodeSnippetExpression($"({type.FullNameExpression()} x) => x.{memberName}"),
                new CodeSnippetExpression($"(ref {type.FullNameExpression()} x, {memberType.FullNameExpression()} value) => x.{memberName} = value"),
            };
            var equalityComparerType = GetEqualityComparerType(memberType);
            if (equalityComparerType != null)
            {
                var equalityComparer = new CodePropertyReferenceExpression(
                    new CodeTypeReferenceExpression(equalityComparerType.FullNameExpression()),
                    "Instance");
                expressions.Add(equalityComparer);
            }
            expressions.Add(new CodePrimitiveExpression(field.Attribute.ReadOnly));
            return expressions.ToArray();
        }

        static Type GetEqualityComparerType(Type type)
        {
            if (equalityComparerLookup.TryGetValue(type, out var elementComparerType))
                return elementComparerType;

            var equatableType = typeof(IEquatable<>).MakeGenericType(type);
            if (type.GetInterfaces().Any(t => t == equatableType))
                return null;

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                elementComparerType = GetEqualityComparerType(elementType);
                return elementComparerType == null
                    ? typeof(ArrayComparer<>).MakeGenericType(elementType)
                    : typeof(ArrayComparer<,>).MakeGenericType(elementType, elementComparerType)
                    ;
            }

            if (type.IsEnum)
            {
                var underlyingTypeCode = Type.GetTypeCode(type.GetEnumUnderlyingType());
                return underlyingTypeCode switch
                {
                    TypeCode.Byte => typeof(ByteEnumComparer<>).MakeGenericType(type),
                    TypeCode.Int32 => typeof(IntEnumComparer<>).MakeGenericType(type),
                    _ => throw new NotSupportedException($"{underlyingTypeCode} doesn't have an enum comparer...")
                };
            }

            throw new NotSupportedException($"Could not find equality comparer or it doesn't implement `IEquatable<>` for type '{type.Namespace + "." + type.Name}'");
        }
    }
}

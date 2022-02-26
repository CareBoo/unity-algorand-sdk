using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlgoSdk.Editor.CodeGen
{
    public class AlgoApiObjectCompileUnit : AlgoApiCompileUnit
    {
        static readonly Dictionary<Type, Type> equalityComparerLookup = new Dictionary<Type, Type>()
        {
            {typeof(string), typeof(StringComparer)}
        };

        public AlgoApiObjectCompileUnit(Type type) : base(type) { }

        protected override CodeExpression ConstructFormatterExpression(Type type)
        {
            var fieldKeys = GetKeyProps(type);
            return GetCreateFormatterExpression(type, fieldKeys);
        }

        protected override IEnumerable<(Type enumType, CodeExpression enumFormatterExpression)> GetReferencedEnumFormatterExpressions(Type type) =>
            GetKeyProps(type)
                .Select(x => x.type)
                .Where(t => t.IsEnum)
                .Select(t => (t, FormatterExpression(t)))
                ;

        CodeExpression GetCreateFormatterExpression(Type algoApiObjType, IEnumerable<(AlgoApiField, MemberInfo, Type)> fieldKeys)
        {
            CodeExpression createdFormatterExpression = new CodeObjectCreateExpression(
                typeof(AlgoApiObjectFormatter<>).MakeGenericType(algoApiObjType));
            foreach (var (key, member, type) in fieldKeys)
            {
                createdFormatterExpression = new CodeMethodInvokeExpression(
                    createdFormatterExpression,
                    nameof(AlgoApiObjectFormatter<int>.Assign),
                    GetAssignParamsExpressions(key, member, type));
            }
            return createdFormatterExpression;
        }

        CodeExpression[] GetAssignParamsExpressions(AlgoApiField key, MemberInfo member, Type memberType)
        {
            var declaringType = member.DeclaringType;
            var memberName = member.Name;
            var expressions = new List<CodeExpression>()
            {
                new CodePrimitiveExpression(key.JsonKeyName),
                new CodePrimitiveExpression(key.MessagePackKeyName),
                new CodeSnippetExpression($"({Format(declaringType)} x) => x.{memberName}"),
                new CodeSnippetExpression($"(ref {Format(declaringType)} x, {Format(memberType)} value) => x.{memberName} = value"),
            };
            var equalityComparerType = GetEqualityComparerType(memberType);
            if (equalityComparerType != null)
            {
                var equalityComparer = new CodePropertyReferenceExpression(
                    new CodeTypeReferenceExpression(equalityComparerType),
                    "Instance");
                expressions.Add(equalityComparer);
            }
            expressions.Add(new CodePrimitiveExpression(key.ReadOnly));
            return expressions.ToArray();
        }

        IEnumerable<(AlgoApiField key, MemberInfo member, Type type)> GetKeyProps(Type algoApiObjType)
        {
            var fields = algoApiObjType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(f => ((MemberInfo)f, f.FieldType));
            var props = algoApiObjType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(p => ((MemberInfo)p, p.PropertyType));
            return props.Concat(fields)
                .Select(GetKeyProp)
                .Where(x => x.key != null)
                ;
        }

        (AlgoApiField key, MemberInfo member, Type type) GetKeyProp((MemberInfo member, Type type) prop)
        {
            return (KeyFromMember(prop.member), prop.member, prop.type);
        }

        AlgoApiField KeyFromMember(MemberInfo member)
        {
            return member.GetCustomAttribute<AlgoApiField>();
        }

        Type GetEqualityComparerType(Type type)
        {
            if (equalityComparerLookup.TryGetValue(type, out var elementComparerType))
                return elementComparerType;

            var equatableType = typeof(IEquatable<>).MakeGenericType(type);
            if (type.IsValueType && type.GetInterfaces().Any(t => t == equatableType))
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

            throw new NotSupportedException($"Could not find equality comparer or it doesn't implement `IEquatable<>` for type '{type.FullName}'");
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
    }
}

using System;
using System.CodeDom;
using System.Linq;

namespace AlgoSdk.Editor.CodeGen
{
    public class FormatterCodeTypeDeclaration : CodeTypeDeclaration
    {
        public bool IsValid { get; protected set; }

        public FormatterCodeTypeDeclaration(Type type)
        {
            if (type.IsEnum) return;

            Name = type.NameExpression();

            IsClass = type.IsClass;
            IsEnum = false;
            IsStruct = type.IsValueType;
            IsPartial = true;

            if (type.IsGenericType)
                TypeParameters.AddRange(new IndexedTypeParameters(type.GenericTypeArguments.Length));

            Members.Add(new FormatterStaticFieldInitializerExpression());
            Members.Add(new InitFormattersCodeMemberMethod(type));

            var nestedTypeDeclarations = type.GetNestedTypes()
                .Select(t => new FormatterCodeTypeDeclaration(t))
                .Where(t => t.IsValid)
                .ToArray()
                ;
            Members.AddRange(nestedTypeDeclarations);

            IsValid = true;
        }
    }
}

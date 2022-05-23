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

            var initFormattersCodeMemberMethod = new InitFormattersCodeMemberMethod(type);
            if (!initFormattersCodeMemberMethod.HasAddedFormatters)
            {
                IsValid = false;
                return;
            }
            Members.Add(initFormattersCodeMemberMethod);
            Members.Add(new FormatterStaticFieldInitializerExpression(type));

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

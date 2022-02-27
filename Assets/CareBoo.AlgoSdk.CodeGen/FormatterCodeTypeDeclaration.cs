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
            {
                var typeParams = new IndexedTypeParameters(type);
                TypeParameters.AddRange(typeParams);
                var outputTypeParams = new CodeTypeParameter[TypeParameters.Count];
                for (var i = 0; i < outputTypeParams.Length; i++)
                    outputTypeParams[i] = TypeParameters[i];

                UnityEngine.Debug.Log($"{type.FullNameExpression()} TypeParameters:\n{string.Join(", ", outputTypeParams.Select(t => t.Name))}");
            }

            Members.Add(new FormatterStaticFieldInitializerExpression(type));
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

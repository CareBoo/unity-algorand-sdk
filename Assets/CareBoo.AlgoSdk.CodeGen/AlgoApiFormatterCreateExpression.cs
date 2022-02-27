using System;
using System.CodeDom;
using System.Collections.Generic;

namespace AlgoSdk.Editor.CodeGen
{
    public class AlgoApiFormatterCreateExpression : CodeObjectCreateExpression
    {
        public AlgoApiFormatterCreateExpression(
            Type type,
            Type formatterType
        )
        {
            var formatterTypeReference = new CodeTypeReference(formatterType);
            if (formatterType.IsGenericTypeDefinition)
                formatterTypeReference.TypeArguments.AddRange(FormatterTypeParameters(type, formatterType));
            CreateType = formatterTypeReference;
        }

        public static CodeTypeReference[] FormatterTypeParameters(Type type, Type formatterType)
        {
            if (!formatterType.IsGenericTypeDefinition)
                return new CodeTypeReference[0];

            var formatterTypeArgumentCount = formatterType.GenericTypeArguments.Length;
            if (type.IsGenericType)
            {
                var typeArgs = new List<CodeTypeReference>();
                var typeArgumentCount = type.GenericTypeArguments.Length;
                var indexedTypeParams = new IndexedTypeParameters(typeArgumentCount);

                if (formatterTypeArgumentCount == typeArgumentCount + 1)
                    typeArgs.Add(new CodeTypeReference(type.Name, indexedTypeParams));
                typeArgs.AddRange(indexedTypeParams.AsReferences());
                return typeArgs.ToArray();
            }
            else if (formatterTypeArgumentCount == 1)
            {
                return new[] { new CodeTypeReference(type) };
            }
            throw new ArgumentException($"Got incorrect number of type arguments {formatterTypeArgumentCount} for formatter {formatterType.FullName}", nameof(formatterType));
        }
    }
}

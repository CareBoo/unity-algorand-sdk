using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace AlgoSdk.Editor.CodeGen
{
    public class AlgoApiFormatterCreateExpression : CodeObjectCreateExpression
    {
        public AlgoApiFormatterCreateExpression(
            Type type,
            Type formatterType
        )
        {
            var constructedFormatterType = ConstructFormatterType(type, formatterType);
            CreateType = new CodeTypeReference(constructedFormatterType.FullNameExpression());
        }

        public static Type ConstructFormatterType(Type type, Type formatterType)
        {
            if (!formatterType.IsGenericTypeDefinition)
                return formatterType;

            var formatterTypeArgumentCount = formatterType.GenericTypeArguments.Length;
            var typeArgs = new List<Type>();
            var typeArgumentCount = type.GenericTypeArguments.Length;

            if (formatterTypeArgumentCount == typeArgumentCount + 1)
            {
                typeArgs.Add(type);
            }
            typeArgs.AddRange(type.GenericTypeArguments);
            if (typeArgs.Count != formatterTypeArgumentCount)
                throw new ArgumentException($"Got {typeArgs.Count} type arguments instead of {formatterTypeArgumentCount} for formatter {formatterType.FullName}", nameof(formatterType));
            return formatterType.MakeGenericType(typeArgs.ToArray());
        }
    }
}

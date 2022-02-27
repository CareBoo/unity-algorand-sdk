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
            if (type.IsGenericType)
            {
                var typeArgs = new List<Type>();
                var typeArgumentCount = type.GenericTypeArguments.Length;

                if (formatterTypeArgumentCount == typeArgumentCount + 1)
                {
                    typeArgs.Add(type);
                }
                typeArgs.AddRange(type.GenericTypeArguments);
                return formatterType.MakeGenericType(typeArgs.ToArray());
            }
            else if (formatterTypeArgumentCount == 1)
            {
                return formatterType.MakeGenericType(type);
            }
            throw new ArgumentException($"Got incorrect number of type arguments {formatterTypeArgumentCount} for formatter {formatterType.FullName}", nameof(formatterType));
        }
    }
}

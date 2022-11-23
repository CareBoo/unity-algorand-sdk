using System;
using System.CodeDom;

namespace Algorand.Unity.Editor.CodeGen
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
            var typeArgumentCount = type.GenericTypeArguments.Length;

            var offset = 0;
            if (formatterTypeArgumentCount == typeArgumentCount + 1)
            {
                offset = 1;
                formatterType.GenericTypeArguments[0] = type;
            }
            for (var i = 0; i < type.GenericTypeArguments.Length; i++)
            {
                formatterType.GenericTypeArguments[i + offset] = type.GenericTypeArguments[i];
            }
            return formatterType;
        }
    }
}

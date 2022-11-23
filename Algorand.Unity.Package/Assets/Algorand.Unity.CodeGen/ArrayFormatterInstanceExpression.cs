using System;
using System.CodeDom;
using Algorand.Unity.Formatters;

namespace Algorand.Unity.Editor.CodeGen
{
    public class ArrayFormatterInstanceExpression
        : CodeFieldReferenceExpression
    {
        public ArrayFormatterInstanceExpression(Type arrayType)
            : base(
                targetObject: ArrayFormatterTypeReference(arrayType),
                fieldName: nameof(ArrayFormatter<int>.Instance)
            )
        { }

        private static CodeTypeReferenceExpression ArrayFormatterTypeReference(Type arrayType)
        {
            var elementType = arrayType.GetElementType();
            var formatterType = typeof(ArrayFormatter<>).MakeGenericType(elementType);
            return new CodeTypeReferenceExpression(formatterType.FullNameExpression());
        }
    }
}

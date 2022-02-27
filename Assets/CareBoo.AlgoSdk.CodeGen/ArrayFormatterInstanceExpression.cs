using System;
using System.CodeDom;
using AlgoSdk.Formatters;

namespace AlgoSdk.Editor.CodeGen
{
    public class ArrayFormatterInstanceExpression
        : CodeFieldReferenceExpression
    {
        public ArrayFormatterInstanceExpression(Type arrayType)
            : base(
                targetObject: new CodeTypeReferenceExpression(typeof(ArrayFormatter<>).MakeGenericType(arrayType.GetElementType()).FullNameExpression()),
                fieldName: nameof(ArrayFormatter<int>.Instance)
            )
        { }

        static CodeTypeReferenceExpression(Type arrayType)
        {
            var formatterType = typeof(ArrayFormatter<>).MakeGenericType(arrayType.GetElementType());
        }
    }
}

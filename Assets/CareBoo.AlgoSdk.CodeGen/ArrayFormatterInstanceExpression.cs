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
                targetObject: new CodeTypeReferenceExpression(typeof(ArrayFormatter<>).MakeGenericType(arrayType.GetElementType())),
                fieldName: nameof(ArrayFormatter<int>.Instance)
            )
        { }
    }
}

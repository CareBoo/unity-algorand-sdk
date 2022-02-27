using System;
using System.CodeDom;

namespace AlgoSdk.Editor.CodeGen
{
    public class AddFormatterCodeMethodInvokeExpression : CodeMethodInvokeExpression
    {
        public AddFormatterCodeMethodInvokeExpression(Type type, CodeExpression formatterExpression)
            : base(
                targetObject: new CodeTypeReferenceExpression(typeof(AlgoApiFormatterLookup)),
                methodName: AlgoApiFormatterLookup.AddFormatterMethodName,
                // params
                new CodeTypeOfExpression(new CodeTypeReference(type.FullNameExpression())),
                formatterExpression
            )
        { }
    }
}

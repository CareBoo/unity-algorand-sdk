using System;
using System.CodeDom;

namespace AlgoSdk.Editor.CodeGen
{
    public class AddFormatterCodeMethodInvokeExpression : CodeMethodInvokeExpression
    {
        public AddFormatterCodeMethodInvokeExpression(Type type, CodeExpression formatterExpression)
            : base(
                GetCodeMethodReference(type),
                formatterExpression
            )
        { }


        static CodeMethodReferenceExpression GetCodeMethodReference(Type type) =>
            new CodeMethodReferenceExpression(
                targetObject: new CodeTypeReferenceExpression(typeof(AlgoApiFormatterLookup)),
                methodName: AlgoApiFormatterLookup.AddFormatterMethodName,
                new CodeTypeReference(type.FullNameExpression())
            );
    }
}

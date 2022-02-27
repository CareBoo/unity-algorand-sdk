using System.CodeDom;

namespace AlgoSdk.Editor.CodeGen
{
    public class FormatterStaticFieldInitializerExpression : CodeMemberField
    {
        public const string FieldName = "__generated__IsValid";

        public FormatterStaticFieldInitializerExpression()
        {
            Name = FieldName;
            Attributes = MemberAttributes.Private | MemberAttributes.Static | MemberAttributes.Final;
            Type = new CodeTypeReference(typeof(bool));
            InitExpression = new CodeMethodInvokeExpression(
                targetObject: new CodeThisReferenceExpression(),
                methodName: InitFormattersCodeMemberMethod.MethodName
            );
        }
    }
}

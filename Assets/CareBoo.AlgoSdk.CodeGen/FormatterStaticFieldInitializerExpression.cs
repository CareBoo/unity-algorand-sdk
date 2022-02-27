using System;
using System.CodeDom;

namespace AlgoSdk.Editor.CodeGen
{
    public class FormatterStaticFieldInitializerExpression : CodeMemberField
    {
        public const string FieldName = "__generated__IsValid";

        public FormatterStaticFieldInitializerExpression(Type type)
        {
            Name = FieldName;
            Attributes = MemberAttributes.Private | MemberAttributes.Static | MemberAttributes.Final;

            InitExpression = new CodeMethodInvokeExpression(
                targetObject: new CodeSnippetExpression(type.NameExpression()),
                methodName: InitFormattersCodeMemberMethod.MethodName
            );
        }
    }
}

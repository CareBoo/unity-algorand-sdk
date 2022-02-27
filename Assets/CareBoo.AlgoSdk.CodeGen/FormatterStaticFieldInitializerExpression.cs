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
            Type = new CodeTypeReference(typeof(bool));

            var targetType = new CodeTypeReferenceExpression(type);
            if (type.IsGenericTypeDefinition)
                targetType.Type.TypeArguments.AddRange(new IndexedTypeParameters(type));
            InitExpression = new CodeMethodInvokeExpression(
                targetObject: targetType,
                methodName: InitFormattersCodeMemberMethod.MethodName
            );
        }
    }
}

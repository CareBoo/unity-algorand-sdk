using System;
using System.CodeDom;
using System.Linq;
using System.Reflection;

namespace Algorand.Unity.Editor.CodeGen
{
    public class InitFormattersCodeMemberMethod : CodeMemberMethod
    {
        public const string MethodName = "__generated__InitializeAlgoApiFormatters";

        public bool HasAddedFormatters { get; protected set; }

        public InitFormattersCodeMemberMethod(Type type)
        {
            Name = MethodName;
            Attributes = MemberAttributes.Private | MemberAttributes.Static;
            ReturnType = new CodeTypeReference(typeof(bool));

            HasAddedFormatters = AddFormatterInitStatements(type);
            Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(HasAddedFormatters)));
        }

        private bool AddFormatterInitStatements(Type type)
        {
            var algoApiObjectAttribute = type.GetCustomAttributes<AlgoApiObjectAttribute>().FirstOrDefault();
            var algoApiFormatterAttribute = type.GetCustomAttributes<AlgoApiFormatterAttribute>().FirstOrDefault();

            if (algoApiObjectAttribute != null)
            {
                var fields = AlgoApiObjectFieldKey.GetFields(type);
                var expression = new AlgoApiObjectFormatterCreateExpression(algoApiObjectAttribute, type, fields);
                Statements.Add(new AddFormatterCodeMethodInvokeExpression(type, expression));
                foreach (var enumType in fields.Select(f => f.MemberType).Where(t => t.IsEnum))
                    AddFormatterInitStatements(enumType);
            }
            else if (algoApiFormatterAttribute != null)
            {
                var formatterType = algoApiFormatterAttribute.FormatterType;
                var expression = new AlgoApiFormatterCreateExpression(type, formatterType);
                Statements.Add(new AddFormatterCodeMethodInvokeExpression(type, expression));
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}

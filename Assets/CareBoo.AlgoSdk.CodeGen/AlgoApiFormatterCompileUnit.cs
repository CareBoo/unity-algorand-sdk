using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace AlgoSdk.Editor.CodeGen
{
    public class AlgoApiFormatterCompileUnit : AlgoApiCompileUnit
    {
        public AlgoApiFormatterCompileUnit(Type type) : base(type) { }

        protected override CodeExpression ConstructFormatterExpression(Type type)
        {
            return FormatterExpression(type);
        }

        protected override IEnumerable<(Type enumType, CodeExpression enumFormatterExpression)> GetReferencedEnumFormatterExpressions(Type type)
        {
            return Enumerable.Empty<(Type enumType, CodeExpression enumFormatterExpression)>();
        }
    }
}

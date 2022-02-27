using System;
using System.CodeDom;
using System.Linq;
using System.Reflection;

namespace AlgoSdk.Editor.CodeGen
{
    /// <summary>
    /// Extends a partial class or struct
    /// </summary>
    public class AlgoApiCompileUnit : CodeCompileUnit
    {
        public Type Type { get; protected set; }
        public ProvideSourceInfoAttribute SourceInfo { get; protected set; }
        public bool IsValid { get; protected set; }

        public AlgoApiCompileUnit(Type type)
        {
            Type = type;
            SourceInfo = GetSourceInfo(type);

            if (type.IsNested) return;

            var typeDeclaration = new FormatterCodeTypeDeclaration(type);
            if (!typeDeclaration.IsValid) return;

            var ns = new CodeNamespace(type.Namespace);
            ns.Types.Add(typeDeclaration);
            Namespaces.Add(ns);
        }

        protected ProvideSourceInfoAttribute GetSourceInfo(Type type) =>
            type.GetCustomAttributes<ProvideSourceInfoAttribute>()
                .First()
                ;
    }
}

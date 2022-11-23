using System;
using System.CodeDom;
using System.Linq;
using System.Reflection;

namespace Algorand.Unity.Editor.CodeGen
{
    /// <summary>
    /// Extends a partial class or struct
    /// </summary>
    public class AlgoApiCompileUnit : CodeCompileUnit
    {
        public Type Type { get; protected set; }
        public ProvideSourceInfoAttribute SourceInfo { get; protected set; }
        public bool IsValid { get; protected set; }

        public FormatterCodeTypeDeclaration TypeDeclaration { get; protected set; }

        public AlgoApiCompileUnit(Type type)
        {
            Type = type;
            SourceInfo = GetSourceInfo(type);

            if (type.IsNested) return;

            TypeDeclaration = new FormatterCodeTypeDeclaration(type);
            if (!TypeDeclaration.IsValid) return;

            var globalNs = new CodeNamespace();
            globalNs.Imports.Add(new CodeNamespaceImport("UnityCollections = Unity.Collections"));
            var ns = new CodeNamespace(type.Namespace);
            ns.Types.Add(TypeDeclaration);
            Namespaces.Add(globalNs);
            Namespaces.Add(ns);
            IsValid = true;
        }

        protected ProvideSourceInfoAttribute GetSourceInfo(Type type) =>
            type.GetCustomAttributes<ProvideSourceInfoAttribute>()
                .First()
                ;
    }
}

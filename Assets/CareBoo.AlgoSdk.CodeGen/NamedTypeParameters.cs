using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgoSdk.Editor.CodeGen
{
    public class NamedTypeParameters
        : IEnumerable<CodeTypeParameter>
    {
        readonly IEnumerable<CodeTypeParameter> typeParams;

        public NamedTypeParameters(Type type)
        {
            typeParams = type.GenericTypeArguments
                .Select(t => new CodeTypeParameter(t.Name))
                ;
        }

        public NamedTypeParameters(int count)
        {
            typeParams = Enumerable.Range(0, count)
                .Select(i => $"T{i}")
                .Select(name => new CodeTypeParameter(name))
                ;
        }

        public IEnumerator<CodeTypeParameter> GetEnumerator()
        {
            return typeParams.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return typeParams.GetEnumerator();
        }

        public IEnumerable<CodeTypeReference> AsReferences() => typeParams.Select(x => new CodeTypeReference(x));

        public static implicit operator CodeTypeParameter[](NamedTypeParameters named)
        {
            return named.ToArray();
        }

        public static implicit operator CodeTypeReference[](NamedTypeParameters named)
        {
            return named.AsReferences().ToArray();
        }
    }
}

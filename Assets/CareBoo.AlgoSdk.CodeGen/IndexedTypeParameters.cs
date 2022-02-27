using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgoSdk.Editor.CodeGen
{
    public class IndexedTypeParameters
        : IEnumerable<CodeTypeParameter>
    {
        readonly IEnumerable<CodeTypeParameter> typeParams;

        public IndexedTypeParameters(Type type) : this(type.GenericTypeArguments.Length)
        {
        }

        public IndexedTypeParameters(int count)
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

        public static implicit operator CodeTypeParameter[](IndexedTypeParameters indexed)
        {
            return indexed.ToArray();
        }

        public static implicit operator CodeTypeReference[](IndexedTypeParameters indexed)
        {
            return indexed.AsReferences().ToArray();
        }
    }
}

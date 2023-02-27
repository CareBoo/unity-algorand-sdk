using System;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    [Serializable]
    public class FixedArrayType : IAbiType
    {
        [SerializeField, SerializeReference] private IAbiType elementType;

        [SerializeField, Min(0)] private int length;

        public FixedArrayType(IAbiType elementType, int length)
        {
            this.elementType = elementType;

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            this.length = length;
        }

        public string Name => $"{elementType?.Name ?? "void"}[{length}]";

        public AbiValueType ValueType => AbiValueType.Array;

        public bool IsStatic => elementType?.IsStatic ?? true;

        public int StaticLength => length * (elementType?.StaticLength ?? 0);

        public int N => length;

        public int M => throw new System.NotImplementedException();

        public IAbiType[] NestedTypes => new IAbiType[] { elementType };

        public IAbiType ElementType => elementType;

        public int ArrayLength => length;

        public bool IsFixedArray => true;

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType ReferenceType => default;

        public (string decodeError, IAbiValue abiValue) Decode(byte[] bytes)
        {
            var decodeError = this.CheckDecodeLength(bytes);
            if (decodeError != null) return (decodeError, null);

            var nestedTypes = new IAbiType[ArrayLength];
            for (var i = 0; i < ArrayLength; i++)
                nestedTypes[i] = ElementType;
            return AbiType.Tuple(nestedTypes).Decode(bytes);
        }
    }
}

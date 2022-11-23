using System;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    [Serializable]
    public class ReferenceType : IAbiType
    {
        [SerializeField] private AbiReferenceType referenceType;

        public ReferenceType(AbiReferenceType referenceType)
        {
            this.referenceType = referenceType;
        }

        public string Name => referenceType switch
        {
            AbiReferenceType.Asset => "asset",
            AbiReferenceType.Application => "application",
            AbiReferenceType.Account => "account",
            _ => throw new NotImplementedException()
        };

        public AbiValueType ValueType => AbiValueType.UIntN;

        public bool IsStatic => true;

        public int StaticLength => 1;

        public int N => 8;

        public int M => throw new NotImplementedException();

        public IAbiType[] NestedTypes => throw new NotImplementedException();

        public IAbiType ElementType => throw new NotImplementedException();

        public int ArrayLength => throw new NotImplementedException();

        public bool IsFixedArray => throw new NotImplementedException();

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType Type => referenceType;

        AbiReferenceType IAbiType.ReferenceType => referenceType;

        public (string decodeError, IAbiValue abiValue) Decode(byte[] bytes)
        {
            throw new NotSupportedException("Reference types cannot be used as return types");
        }
    }
}

using System;
using UnityEngine;

namespace AlgoSdk.Experimental.Abi
{
    [Serializable]
    public class ReferenceType : IAbiType
    {
        [SerializeField]
        AbiReferenceType referenceType;

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

        AbiReferenceType IAbiType.ReferenceType => referenceType;
    }
}

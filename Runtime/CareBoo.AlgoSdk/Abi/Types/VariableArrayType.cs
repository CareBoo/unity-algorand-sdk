using System;
using UnityEngine;

namespace AlgoSdk.Abi
{
    [Serializable]
    public class VariableArrayType : IAbiType
    {
        [SerializeField, SerializeReference]
        IAbiType elementType;

        public VariableArrayType(IAbiType elementType)
        {
            this.elementType = elementType;
        }

        public string Name => $"{elementType.Name}[]";

        public AbiValueType ValueType => AbiValueType.Array;

        public bool IsStatic => false;

        public int StaticLength => throw new System.NotImplementedException();

        public int N => throw new System.NotImplementedException();

        public int M => throw new System.NotImplementedException();

        public IAbiType[] NestedTypes => new IAbiType[] { elementType };

        public IAbiType ElementType => elementType;

        public int ArrayLength => throw new System.NotImplementedException();

        public bool IsFixedArray => false;

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType ReferenceType => default;
    }
}

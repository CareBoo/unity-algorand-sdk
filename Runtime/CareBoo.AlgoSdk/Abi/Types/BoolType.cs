using System;

namespace AlgoSdk.Abi
{
    [Serializable]
    public class BoolType : IAbiType
    {
        public string Name => "bool";

        public AbiValueType ValueType => AbiValueType.Boolean;

        public bool IsStatic => true;

        public int StaticLength => 1;

        public int N => throw new System.NotImplementedException();

        public int M => throw new System.NotImplementedException();

        public IAbiType[] NestedTypes => throw new System.NotImplementedException();

        public IAbiType ElementType => throw new System.NotImplementedException();

        public int ArrayLength => throw new System.NotImplementedException();

        public bool IsFixedArray => false;

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType ReferenceType => default;
    }
}

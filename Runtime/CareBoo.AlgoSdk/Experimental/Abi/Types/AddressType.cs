using System;

namespace AlgoSdk.Experimental.Abi
{
    [Serializable]
    public class AddressType : IAbiType
    {
        public string Name => "address";

        public AbiValueType ValueType => AbiValueType.Array;

        public bool IsStatic => true;

        public int StaticLength => 4;

        public int N => 32;

        public int M => throw new System.NotImplementedException();

        public IAbiType[] NestedTypes => new IAbiType[] { default(ByteType) };

        public IAbiType ElementType => default(ByteType);

        public int ArrayLength => 32;

        public bool IsFixedArray => true;

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType ReferenceType => default;
    }
}

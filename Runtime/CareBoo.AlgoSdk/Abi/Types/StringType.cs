using System;

namespace AlgoSdk.Abi
{
    [Serializable]
    public class StringType : IAbiType
    {
        public string Name => "string";

        public AbiValueType ValueType => AbiValueType.Array;

        public bool IsStatic => false;

        public int StaticLength => 0;

        public int N => throw new NotImplementedException();

        public int M => throw new NotImplementedException();

        public IAbiType[] NestedTypes => new IAbiType[] { default(ByteType) };

        public IAbiType ElementType => default(ByteType);

        public int ArrayLength => throw new NotImplementedException();

        public bool IsFixedArray => false;

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType ReferenceType => default;
    }
}

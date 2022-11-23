using System;

namespace Algorand.Unity.Experimental.Abi
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

        public IAbiType[] NestedTypes => new IAbiType[] { AbiType.Byte };

        public IAbiType ElementType => AbiType.Byte;

        public int ArrayLength => throw new NotImplementedException();

        public bool IsFixedArray => false;

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType ReferenceType => default;

        public (string decodeError, IAbiValue abiValue) Decode(byte[] bytes)
        {
            var (decodeError, tupleValue) = new VariableArrayType(new UIntType(8)).Decode(bytes);

            if (decodeError != null) return (decodeError, null);
            return (null, (String)((Tuple<ArgsArray>)tupleValue));
        }
    }
}

using System;

namespace Algorand.Unity.Experimental.Abi
{
    [Serializable]
    public class ByteType : IAbiType
    {
        public string Name => "byte";

        public AbiValueType ValueType => AbiValueType.UIntN;

        public bool IsStatic => true;

        public int StaticLength => 1;

        public int N => 8;

        public int M => throw new System.NotImplementedException();

        public IAbiType[] NestedTypes => throw new System.NotImplementedException();

        public IAbiType ElementType => throw new System.NotImplementedException();

        public int ArrayLength => throw new System.NotImplementedException();

        public bool IsFixedArray => throw new System.NotImplementedException();

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType ReferenceType => default;

        public (string decodeError, IAbiValue abiValue) Decode(byte[] bytes)
        {
            var decodeError = this.CheckDecodeLength(bytes);
            if (decodeError != null) return (decodeError, null);

            return (null, new UInt8(bytes[0]));
        }
    }
}

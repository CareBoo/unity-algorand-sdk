using System;

namespace Algorand.Unity.Experimental.Abi
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

        public (string decodeError, IAbiValue abiValue) Decode(byte[] bytes)
        {
            var decodeError = this.CheckDecodeLength(bytes);
            if (decodeError != null)
            {
                return (decodeError, null);
            }

            var address = default(Address);
            for (var i = 0; i < address.Length; i++)
            {
                address[i] = bytes[i];
            }

            return (null, new AbiAddress(address));
        }
    }
}

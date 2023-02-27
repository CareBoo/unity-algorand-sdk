using System;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    [Serializable]
    public class VariableArrayType : IAbiType
    {
        [SerializeField, SerializeReference] private IAbiType elementType;

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

        public (string decodeError, IAbiValue abiValue) Decode(byte[] bytes)
        {
            if (bytes == null)
            {
                return ($"Can't decode null to {Name}", null);
            }
            if (bytes.Length < 2)
            {
                return ($"Can't decode {bytes.Length} bytes to {Name}", null);
            }

            var length = (bytes[0] << 8) + bytes[1];
            var fixedArrBytes = new byte[bytes.Length - 2];
            Array.Copy(bytes, 2, fixedArrBytes, 0, fixedArrBytes.Length);

            return AbiType.FixedArray(ElementType, length).Decode(fixedArrBytes);
        }
    }
}

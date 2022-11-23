using System;
using System.Numerics;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    [Serializable]
    public class UIntType : IAbiType
    {
        [SerializeField, ModRange(8, 512, 8)] private int n;

        public UIntType(int n)
        {
            if (n % 8 > 0)
                throw new System.ArgumentException($"{n} is not a multiple of 8", nameof(n));
            if (n < 8 || n > 512)
                throw new System.ArgumentException($"{n} is not in the range [8, 512]", nameof(n));
            this.n = n;
        }

        public string Name => $"uint{n}";

        public AbiValueType ValueType => AbiValueType.UIntN;

        public bool IsStatic => true;

        public int StaticLength => n / 8;

        public int N => n;

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
            if (decodeError != null)
            {
                return (decodeError, null);
            }

            switch (N)
            {
                case 8:
                    var uint8 = bytes[0];
                    return (null, new UInt8(uint8));
                case 16:
                    Endianness.FromBytesBigEndian(bytes, out ushort uint16);
                    return (null, new UInt16(uint16));
                case 32:
                    Endianness.FromBytesBigEndian(bytes, out uint uint32);
                    return (null, new UInt32(uint32));
                case 64:
                    Endianness.FromBytesBigEndian(bytes, out ulong uint64);
                    return (null, new UInt64(uint64));
                default:
                    Endianness.FromBytesBigEndian(bytes, out BigInteger uintN);
                    return (null, new UIntN(uintN));
            }
        }
    }
}

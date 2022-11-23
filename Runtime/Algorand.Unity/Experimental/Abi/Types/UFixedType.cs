using System;
using System.Numerics;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    [Serializable]
    public class UFixedType : IAbiType
    {
        [SerializeField, ModRange(8, 512, 8)] private int n;

        [SerializeField, ModRange(1, 160, 1)] private int m;

        public UFixedType(int n, int m)
        {
            if (n % 8 > 0)
                throw new System.ArgumentException($"{n} is not a multiple of 8", nameof(n));
            if (n < 8 || n > 512)
                throw new System.ArgumentException($"{n} is not in the range [8, 512]", nameof(n));
            if (m < 1 || m > 160)
                throw new System.ArgumentException($"{m} is not in the range (0, 160]", nameof(m));
            this.n = n;
            this.m = m;
        }

        public string Name => $"ufixed{n}x{m}";

        public AbiValueType ValueType => AbiValueType.UFixedNxM;

        public bool IsStatic => true;

        public int StaticLength => n / 8;

        public int N => n;

        public int M => m;

        public IAbiType[] NestedTypes => throw new NotImplementedException();

        public IAbiType ElementType => throw new NotImplementedException();

        public int ArrayLength => throw new NotImplementedException();

        public bool IsFixedArray => throw new NotImplementedException();

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType ReferenceType => default;

        public (string decodeError, IAbiValue abiValue) Decode(byte[] bytes)
        {
            var decodeError = this.CheckDecodeLength(bytes);
            if (decodeError != null)
            {
                return (decodeError, null);
            }

            Endianness.FromBytesBigEndian(bytes, out BigInteger value);
            return (null, new UFixedNxM(new UIntN(value), (byte)M));
        }
    }
}

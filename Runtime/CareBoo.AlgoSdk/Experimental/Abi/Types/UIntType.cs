using System;
using UnityEngine;

namespace AlgoSdk.Experimental.Abi
{
    [Serializable]
    public class UIntType : IAbiType
    {
        [SerializeField, ModRange(8, 512, 8)]
        int n;

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
    }
}

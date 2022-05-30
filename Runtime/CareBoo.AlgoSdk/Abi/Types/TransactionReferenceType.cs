using System;
using UnityEngine;

namespace AlgoSdk.Abi
{
    [Serializable]
    public class TransactionReferenceType : IAbiType
    {
        [SerializeField]
        AbiTransactionType txnType;

        public TransactionReferenceType(AbiTransactionType txnType)
        {
            this.txnType = txnType;
        }

        public string Name => txnType.ToString();

        public AbiValueType ValueType => default;

        public bool IsStatic => true;

        public int StaticLength => 0;

        public int N => throw new NotImplementedException();

        public int M => throw new NotImplementedException();

        public IAbiType[] NestedTypes => throw new NotImplementedException();

        public IAbiType ElementType => throw new NotImplementedException();

        public int ArrayLength => throw new NotImplementedException();

        public bool IsFixedArray => throw new NotImplementedException();

        public AbiTransactionType TransactionType => txnType;

        public AbiReferenceType ReferenceType => default;
    }
}

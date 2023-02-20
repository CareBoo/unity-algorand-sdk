using System;
using Algorand.Unity.Experimental.Abi;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.CallingSmartContractAbi
{
    public abstract partial class ArgField : VisualElement
    {
        public abstract IAbiValue Value { get; }

        public static ArgField Create(string label, IAbiType type) => type switch
        {
            ReferenceType refType when refType.Type == AbiReferenceType.Account => new Account(label),
            VariableArrayType variableArrayType => new Array(label, variableArrayType),
            TupleType tupleType => new Tuple(label, tupleType),
            StringType stringType => new String(label),
            UIntType uintType when uintType.N == 64 => new UInt64(label),
            TransactionReferenceType txnRefType => new Txn(label, txnRefType),
            _ => throw new NotSupportedException()
        };
    }
}
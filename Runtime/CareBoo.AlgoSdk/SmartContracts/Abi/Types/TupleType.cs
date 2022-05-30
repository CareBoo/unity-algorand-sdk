using System;
using UnityEngine;

namespace AlgoSdk.Abi
{
    [Serializable]
    public class TupleType
        : IAbiType
        , ISerializationCallbackReceiver
    {
        [SerializeField, SerializeReference]
        IAbiType[] nestedTypes;

        bool isStatic;

        int staticLength;

        public TupleType(IAbiType[] nestedTypes)
        {
            this.nestedTypes = nestedTypes;
            GetStaticInfo(nestedTypes, out isStatic, out staticLength);
        }

        public string Name => $"({string.Join<IAbiType>(",", nestedTypes)})";

        public AbiValueType ValueType => AbiValueType.Tuple;

        public bool IsStatic => isStatic;

        public int StaticLength => staticLength;

        public int N => throw new NotImplementedException();

        public int M => throw new NotImplementedException();

        public IAbiType[] NestedTypes => nestedTypes;

        public IAbiType ElementType => throw new NotImplementedException();

        public int ArrayLength => throw new NotImplementedException();

        public bool IsFixedArray => throw new NotImplementedException();

        public AbiTransactionType TransactionType => default;

        public AbiReferenceType ReferenceType => default;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            GetStaticInfo(nestedTypes, out isStatic, out staticLength);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        static void GetStaticInfo(IAbiType[] nestedTypes, out bool isStatic, out int staticLength)
        {
            if (nestedTypes == null)
            {
                isStatic = false;
                staticLength = 0;
                return;
            }

            isStatic = true;
            staticLength = 0;
            var boolCount = 0;

            for (var i = 0; i < nestedTypes.Length; i++)
            {
                var type = nestedTypes[i];
                if (!type?.IsStatic ?? true)
                {
                    isStatic = false;
                    staticLength = 0;
                    break;
                }

                boolCount = type.ValueType == AbiValueType.Boolean
                    ? boolCount + 1
                    : 0
                    ;
                if (boolCount % 8 == 0)
                    staticLength += type.StaticLength;
            }
        }
    }
}

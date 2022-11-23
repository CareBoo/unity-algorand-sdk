using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi
{
    [Serializable]
    public class TupleType
        : IAbiType
        , ISerializationCallbackReceiver
    {
        [SerializeField, SerializeReference] private IAbiType[] nestedTypes;

        private bool isStatic;

        private int staticLength;

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

        public (string decodeError, IAbiValue abiValue) Decode(byte[] bytes)
        {
            var decodeError = this.CheckDecodeLength(bytes);
            if (decodeError != null)
            {
                return (decodeError, null);
            }

            var boolCount = 0;
            var offset = 0;
            var dynamicPtrs = new List<(int ptr, int index)>();
            var abiValues = new IAbiValue[NestedTypes.Length];
            for (var i = 0; i < NestedTypes.Length; i++)
            {
                var type = NestedTypes[i];
                if (type.ValueType == AbiValueType.Boolean)
                {
                    boolCount %= 8;
                    if (boolCount > 0)
                    {
                        offset--;
                    }
                    var packedBools = (PackedBooleans)bytes[offset];
                    abiValues[i] = new Boolean(packedBools[boolCount]);
                    boolCount++;
                    offset++;
                }
                else
                {
                    boolCount = 0;
                    if (type.IsStatic)
                    {
                        var subArray = new byte[type.StaticLength];
                        Array.Copy(bytes, offset, subArray, 0, type.StaticLength);
                        (decodeError, abiValues[i]) = type.Decode(subArray);
                        if (decodeError != null) return (decodeError, null);
                        offset += type.StaticLength;
                    }
                    else
                    {
                        if (offset + 2 > bytes.Length)
                        {
                            return ($"Can't decode {bytes.Length} bytes to {Name}", null);
                        }
                        var ptr = bytes[offset] << 8 + bytes[offset + 1];
                        dynamicPtrs.Add((ptr, i));
                        offset += 2;
                    }
                }
            }

            for (var i = 0; i < dynamicPtrs.Count; i++)
            {
                var (start, index) = dynamicPtrs[i];
                var end = i == dynamicPtrs.Count - 1 ? bytes.Length : dynamicPtrs[i + 1].index;
                var count = end - start;
                var subArray = new byte[count];
                Array.Copy(bytes, start, subArray, 0, count);

                (decodeError, abiValues[index]) = NestedTypes[index].Decode(subArray);
                if (decodeError != null)
                {
                    return (decodeError, null);
                }
            }

            return (null, Tuple.Of(Args.Of(abiValues)));
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            GetStaticInfo(nestedTypes, out isStatic, out staticLength);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        private static void GetStaticInfo(IAbiType[] nestedTypes, out bool isStatic, out int staticLength)
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

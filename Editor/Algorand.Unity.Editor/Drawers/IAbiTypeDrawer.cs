using UnityEditor;
using UnityEngine;

namespace Algorand.Unity.Experimental.Abi.Editor
{
    [CustomPropertyDrawer(typeof(IAbiType), true)]
    public class IAbiTypeDrawer : PropertyDrawer
    {
        public const string AssemblyName = "Algorand.Unity ";
        public const string Namespace = "Algorand.Unity.Experimental.Abi.";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                base.OnGUI(position, property, label);
                return;
            }

            var nextAbiType = GetSelectedAbiType(property.managedReferenceFullTypename);
            EditorGUI.BeginChangeCheck();
            nextAbiType = (AbiTypeCode)EditorGUI.EnumPopup(LabelIndent(position), nextAbiType);
            if (EditorGUI.EndChangeCheck())
                property.managedReferenceValue = SelectAbiType(nextAbiType);
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public AbiTypeCode GetSelectedAbiType(string fullTypeName)
        {
            switch (fullTypeName)
            {
                case AssemblyName + Namespace + nameof(ReferenceType):
                    return AbiTypeCode.Reference;
                case AssemblyName + Namespace + nameof(TransactionReferenceType):
                    return AbiTypeCode.Transaction;
                case AssemblyName + Namespace + nameof(AddressType):
                    return AbiTypeCode.Address;
                case AssemblyName + Namespace + nameof(BoolType):
                    return AbiTypeCode.Bool;
                case AssemblyName + Namespace + nameof(ByteType):
                    return AbiTypeCode.Byte;
                case AssemblyName + Namespace + nameof(FixedArrayType):
                    return AbiTypeCode.FixedArray;
                case AssemblyName + Namespace + nameof(VariableArrayType):
                    return AbiTypeCode.VariableArray;
                case AssemblyName + Namespace + nameof(StringType):
                    return AbiTypeCode.String;
                case AssemblyName + Namespace + nameof(TupleType):
                    return AbiTypeCode.Tuple;
                case AssemblyName + Namespace + nameof(UFixedType):
                    return AbiTypeCode.UFixed;
                case AssemblyName + Namespace + nameof(UIntType):
                    return AbiTypeCode.UInt;
                default:
                    return AbiTypeCode.None;
            }
        }

        public IAbiType SelectAbiType(AbiTypeCode option) => option switch
        {
            AbiTypeCode.Reference => new ReferenceType(AbiReferenceType.None),
            AbiTypeCode.Transaction => new TransactionReferenceType(AbiTransactionType.None),
            AbiTypeCode.Address => AbiType.Address,
            AbiTypeCode.Bool => AbiType.Bool,
            AbiTypeCode.Byte => AbiType.Byte,
            AbiTypeCode.FixedArray => new FixedArrayType(null, 0),
            AbiTypeCode.VariableArray => new VariableArrayType(null),
            AbiTypeCode.String => AbiType.String,
            AbiTypeCode.Tuple => new TupleType(new IAbiType[0]),
            AbiTypeCode.UFixed => new UFixedType(8, 1),
            AbiTypeCode.UInt => new UIntType(8),
            _ => null
        };

        private static Rect LabelIndent(Rect position)
        {
            position.xMin += EditorGUIUtility.labelWidth;
            position.height = EditorGUIUtility.singleLineHeight;
            return position;
        }
    }
}

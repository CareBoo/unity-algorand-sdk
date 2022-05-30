using AlgoSdk.Abi;
using UnityEditor;
using UnityEngine;

namespace AlgoSdk.Editor
{
    [CustomPropertyDrawer(typeof(IAbiType), true)]
    public class IAbiTypeDrawer : PropertyDrawer
    {
        public enum AbiTypeOption
        {
            None,
            Reference,
            Transaction,
            Address,
            Bool,
            Byte,
            FixedArray,
            VariableArray,
            String,
            Tuple,
            UFixed,
            UInt,
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                base.OnGUI(position, property, label);
                return;
            }

            var nextAbiType = GetSelectedAbiType(property.managedReferenceValue);
            EditorGUI.BeginChangeCheck();
            nextAbiType = (AbiTypeOption)EditorGUI.EnumPopup(LabelIndent(position), nextAbiType);
            if (EditorGUI.EndChangeCheck())
                property.managedReferenceValue = SelectAbiType(nextAbiType);
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public AbiTypeOption GetSelectedAbiType(object obj) => obj switch
        {
            ReferenceType => AbiTypeOption.Reference,
            TransactionReferenceType => AbiTypeOption.Transaction,
            AddressType => AbiTypeOption.Address,
            BoolType => AbiTypeOption.Bool,
            ByteType => AbiTypeOption.Byte,
            FixedArrayType => AbiTypeOption.FixedArray,
            VariableArrayType => AbiTypeOption.VariableArray,
            StringType => AbiTypeOption.String,
            TupleType => AbiTypeOption.Tuple,
            UFixedType => AbiTypeOption.UFixed,
            UIntType => AbiTypeOption.UInt,
            _ => AbiTypeOption.None
        };

        public IAbiType SelectAbiType(AbiTypeOption option) => option switch
        {
            AbiTypeOption.Reference => new ReferenceType(AbiReferenceType.None),
            AbiTypeOption.Transaction => new TransactionReferenceType(AbiTransactionType.None),
            AbiTypeOption.Address => new AddressType(),
            AbiTypeOption.Bool => new BoolType(),
            AbiTypeOption.Byte => new ByteType(),
            AbiTypeOption.FixedArray => new FixedArrayType(null, 0),
            AbiTypeOption.VariableArray => new VariableArrayType(null),
            AbiTypeOption.String => new StringType(),
            AbiTypeOption.Tuple => new TupleType(new IAbiType[0]),
            AbiTypeOption.UFixed => new UFixedType(8, 1),
            AbiTypeOption.UInt => new UIntType(8),
            _ => null
        };

        static Rect LabelIndent(Rect position)
        {
            position.xMin += EditorGUIUtility.labelWidth;
            position.height = EditorGUIUtility.singleLineHeight;
            return position;
        }
    }
}

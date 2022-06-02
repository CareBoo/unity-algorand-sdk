using AlgoSdk.Abi;
using UnityEditor;
using UnityEngine;

namespace AlgoSdk.Editor
{
    [CustomPropertyDrawer(typeof(IAbiType), true)]
    public class IAbiTypeDrawer : PropertyDrawer
    {
        public const string AssemblyName = "CareBoo.AlgoSdk ";
        public const string Namespace = "AlgoSdk.Abi.";

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

            var nextAbiType = GetSelectedAbiType(property.managedReferenceFullTypename);
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

        public AbiTypeOption GetSelectedAbiType(string fullTypeName)
        {
            switch (fullTypeName)
            {
                case AssemblyName + Namespace + nameof(ReferenceType):
                    return AbiTypeOption.Reference;
                case AssemblyName + Namespace + nameof(TransactionReferenceType):
                    return AbiTypeOption.Transaction;
                case AssemblyName + Namespace + nameof(AddressType):
                    return AbiTypeOption.Address;
                case AssemblyName + Namespace + nameof(BoolType):
                    return AbiTypeOption.Bool;
                case AssemblyName + Namespace + nameof(ByteType):
                    return AbiTypeOption.Byte;
                case AssemblyName + Namespace + nameof(FixedArrayType):
                    return AbiTypeOption.FixedArray;
                case AssemblyName + Namespace + nameof(VariableArrayType):
                    return AbiTypeOption.VariableArray;
                case AssemblyName + Namespace + nameof(StringType):
                    return AbiTypeOption.String;
                case AssemblyName + Namespace + nameof(TupleType):
                    return AbiTypeOption.Tuple;
                case AssemblyName + Namespace + nameof(UFixedType):
                    return AbiTypeOption.UFixed;
                case AssemblyName + Namespace + nameof(UIntType):
                    return AbiTypeOption.UInt;
                default:
                    return AbiTypeOption.None;
            }
        }

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

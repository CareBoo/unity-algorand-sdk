using System;
using AlgoSdk;
using UnityEditor;
using UnityEngine;

namespace CareBoo.AlgoSdk.Editor
{
    [CustomPropertyDrawer(typeof(Address))]
    public class AddressDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var byteProperties = new SerializedBytes(property);
            var address = byteProperties.ToByteArray<Address>();
            position = EditorGUI.PrefixLabel(position, label);
            var addressText = EditorGUI.DelayedTextField(position, address.ToString());
            try
            {
                address = Address.FromString(addressText);
            }
            catch (ArgumentException argException)
            {
                Debug.LogError($"Invalid address at {property.propertyPath}:\n{argException}");
                address = Address.Empty;
            }
            byteProperties.SetBytes(address);
            EditorGUI.EndProperty();
        }
    }
}

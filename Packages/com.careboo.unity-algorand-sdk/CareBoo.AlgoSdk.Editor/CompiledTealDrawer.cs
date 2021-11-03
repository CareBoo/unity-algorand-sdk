using System;
using UnityEditor;
using UnityEngine;

namespace AlgoSdk.Editor
{
    [CustomPropertyDrawer(typeof(CompiledTeal))]
    public class CompiledTealDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.tooltip = "Base 64 encoded teal bytecode";
            position = EditorGUI.PrefixLabel(position, label);
            property = property.FindPropertyRelative(nameof(CompiledTeal.Bytes));
            var text = GetText(property);
            text = EditorGUI.DelayedTextField(position, text);
            try
            {
                SetText(property, text);
            }
            catch (ArgumentException ex)
            {
                Debug.LogError($"Could not decode string into bytes:\n{ex}");
            }
        }

        string GetText(SerializedProperty property)
        {
            if (!property.isArray)
                return string.Empty;

            var bytes = new byte[property.arraySize];
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)property.GetArrayElementAtIndex(i).intValue;

            return System.Convert.ToBase64String(bytes);
        }

        void SetText(SerializedProperty property, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                property.ClearArray();
                return;
            }

            var bytes = System.Convert.FromBase64String(text);
            property.arraySize = bytes.Length;
            for (var i = 0; i < bytes.Length; i++)
                property.GetArrayElementAtIndex(i).intValue = (int)bytes[i];
        }
    }
}

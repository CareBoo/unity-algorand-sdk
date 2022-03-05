using System;
using System.Collections.Generic;
using AlgoSdk.LowLevel;
using UnityEditor;
using UnityEngine;

namespace AlgoSdk.Editor
{
    public abstract class BytesTextDrawer<T> : BytesTextDrawer
        where T : struct, IByteArray
    {
        protected override string GetString(List<byte> bytes)
        {
            T t = default;
            for (var i = 0; i < bytes.Count; i++)
                t[i] = bytes[i];
            return GetString(t);
        }

        protected override List<byte> GetBytes(string s)
        {
            var t = GetByteArray(s);
            var bytes = new List<byte>();
            for (var i = 0; i < t.Length; i++)
                bytes.Add(t[i]);
            return bytes;
        }

        protected abstract string GetString(T bytes);
        protected abstract T GetByteArray(string s);
    }

    public abstract class BytesTextDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position = EditorGUI.PrefixLabel(position, label);
            var byteProperties = new SerializedBytes(property);
            var text = GetString(byteProperties.GetBytes());
            text = EditorGUI.DelayedTextField(position, text);
            try
            {
                byteProperties.SetBytes(GetBytes(text));
            }
            catch (ArgumentException ex)
            {
                Debug.LogError($"Invalid input for property at {property.propertyPath}:\n{ex}");
            }
        }

        protected abstract string GetString(List<byte> bytes);
        protected abstract List<byte> GetBytes(string s);
    }
}

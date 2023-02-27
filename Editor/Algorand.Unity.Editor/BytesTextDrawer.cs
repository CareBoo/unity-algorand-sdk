using System;
using System.Collections.Generic;
using Algorand.Unity.LowLevel;
using UnityEditor;
using UnityEngine;

namespace Algorand.Unity.Editor
{
    public abstract class BytesTextDrawer<TResult> : BytesTextDrawer
        where TResult : struct, IByteArray
    {
        protected override string GetString(List<byte> bytes)
        {
            TResult t = default;
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

        protected abstract string GetString(TResult bytes);
        protected abstract TResult GetByteArray(string s);
    }

    public abstract class FixedBytesTextDrawer<TResult> : BytesTextDrawer<TResult>
        where TResult : struct, IByteArray
    {
        protected override SerializedBytes GetSerializedBytes(SerializedProperty property) => new SerializedFixedBytes(property);
    }

    public abstract class BytesTextDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var byteProperties = GetSerializedBytes(property);
            var text = GetString(byteProperties.GetBytes());
            text = EditorGUI.DelayedTextField(position, label, text);
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
        protected abstract SerializedBytes GetSerializedBytes(SerializedProperty property);
    }

    public abstract class FixedBytesTextDrawer : BytesTextDrawer
    {
        protected override SerializedBytes GetSerializedBytes(SerializedProperty property) => new SerializedFixedBytes(property);
    }
}

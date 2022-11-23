using Unity.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Algorand.Unity.Editor
{
    [CustomPropertyDrawer(typeof(FixedString32Bytes))]
    [CustomPropertyDrawer(typeof(FixedString64Bytes))]
    [CustomPropertyDrawer(typeof(FixedString128Bytes))]
    [CustomPropertyDrawer(typeof(FixedString512Bytes))]
    [CustomPropertyDrawer(typeof(FixedString4096Bytes))]
    public class FixedStringDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var lengthProperty = property.FindPropertyRelative("utf8LengthInBytes");
            var byteProperties = new SerializedFixedBytes(property);
            var bytes = byteProperties.GetBytes();
            var length = math.min(lengthProperty.intValue, bytes.Count);
            var text = new NativeText(length, Allocator.Persistent);
            try
            {
                text.Length = length;
                for (var i = 0; i < length; i++)
                    text[i] = bytes[i];
                var s = EditorGUI.DelayedTextField(position, label, text.ToString());
                text.Clear();
                text.Append(s);
                length = math.min(text.Length, bytes.Count);
                for (var i = 0; i < length; i++)
                    bytes[i] = text[i];
                byteProperties.SetBytes(bytes);
                lengthProperty.intValue = length;
            }
            finally
            {
                text.Dispose();
            }
        }
    }
}

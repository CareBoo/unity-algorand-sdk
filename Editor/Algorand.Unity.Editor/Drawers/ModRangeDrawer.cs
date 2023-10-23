using UnityEditor;
using UnityEngine;

namespace Algorand.Unity.Editor
{
    [CustomPropertyDrawer(typeof(ModRangeAttribute))]
    public class ModRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                base.OnGUI(position, property, label);
                return;
            }

            var modRangeAttr = (ModRangeAttribute)attribute;
            label = EditorGUI.BeginProperty(position, label, property);
            var min = modRangeAttr.Min / modRangeAttr.Mod * modRangeAttr.Mod;
            var max = modRangeAttr.Max / modRangeAttr.Mod * modRangeAttr.Mod;
            var newValue = property.intValue / modRangeAttr.Mod * modRangeAttr.Mod;
            EditorGUI.BeginChangeCheck();
            newValue = EditorGUI.IntSlider(position, label, newValue, min, max);
            if (EditorGUI.EndChangeCheck())
                property.intValue = newValue;
            EditorGUI.EndProperty();
        }
    }
}

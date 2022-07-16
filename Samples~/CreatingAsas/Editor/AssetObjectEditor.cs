using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AssetObject))]
public class AssetObjectEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var assetObject = (AssetObject)serializedObject.targetObject;
        if (assetObject.Index == 0)
        {
            EditorGUILayout.Space();
            if (GUILayout.Button("Create Asset"))
            {
                AssetCreateWindow.Show(assetObject);
            }
        }
    }
}

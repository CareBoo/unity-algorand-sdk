using UnityEditor;
using UnityEngine;

namespace Algorand.Unity.Editor
{
    [CustomEditor(typeof(AlgorandStandardAsset))]
    public class AlgorandStandardAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var assetObject = (AlgorandStandardAsset)serializedObject.targetObject;
            if (assetObject.index == 0)
            {
                EditorGUILayout.Space();
                if (GUILayout.Button("Create Asset"))
                {
                    AssetCreateWindow.Show(assetObject);
                }
            }
        }
    }
}

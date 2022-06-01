using UnityEditor;
using UnityEngine;

namespace AlgoSdk.WalletConnect.Editor
{
    [CustomEditor(typeof(WalletConnectAccountAsset))]
    public class WalletConnectAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Connect Account"))
                WalletConnectEditorWindow.Show((WalletConnectAccountAsset)serializedObject.targetObject);
        }
    }
}

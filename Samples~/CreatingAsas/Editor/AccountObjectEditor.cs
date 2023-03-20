using UnityEditor;
using UnityEngine;

namespace Algorand.Unity.Samples.CreatingAsas.Editor
{
    [CustomEditor(typeof(AccountObject))]
    public class AccountObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "This asset should only be used for sample purposes. Storing private keys in an asset is not safe for production.",
                MessageType.Warning);
            DrawDefaultInspector();
            var accountObject = (AccountObject)serializedObject.targetObject;
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Generate new Account"))
                {
                    accountObject.GenerateNewAccount();
                    serializedObject.Update();
                }

                if (GUILayout.Button("Fund Account"))
                {
                    Application.OpenURL($"https://dispenser.testnet.aws.algodev.network/?account={accountObject.Address}");
                }
            }
        }
    }
}

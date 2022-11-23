using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Editor
{
    [CustomEditor(typeof(WalletConnectAccountObject))]
    public class WalletConnectAccountObjectEditor : UnityEditor.Editor
    {
        private bool checkingWalletStatus;

        private void OnDisable()
        {
            var account = (WalletConnectAccountObject)serializedObject.targetObject;
            account.EndSession();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var account = (WalletConnectAccountObject)serializedObject.targetObject;
            var walletName = string.IsNullOrEmpty(account.SessionData.WalletMeta.Name) ? "None" : account.SessionData.WalletMeta.Name;
            EditorGUILayout.TextField("Connected Account", account.Address == Address.Empty ? "None" : account.Address.ToString());
            EditorGUILayout.TextField("Connected Wallet", walletName);
            var status = account.ConnectionStatus;
            if (status == SessionStatus.None && !checkingWalletStatus)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Connection Status");
                if (GUILayout.Button("Check Wallet Status"))
                    CheckStatus().Forget();
                EditorGUILayout.EndHorizontal();
            }
            else if (checkingWalletStatus)
            {
                EditorGUILayout.TextField("Connection Status", "Checking status...");
            }
            else
            {
                EditorGUILayout.TextField("Connection Status", ObjectNames.NicifyVariableName(status.ToString()));
            }

            EditorGUILayout.Space();

            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier<Object>(serializedObject.targetObject, out var assetGuid, out var _)
                && GUILayout.Button("Reconnect Wallet"))
            {
                WalletConnectAccountEditorWindow.ShowMenu(assetGuid);
            }
        }

        private async UniTaskVoid CheckStatus()
        {
            checkingWalletStatus = true;
            var account = (WalletConnectAccountObject)serializedObject.targetObject;
            await UniTask.SwitchToThreadPool();
            try
            {
                await account.BeginSession();
            }
            finally
            {
                await UniTask.SwitchToMainThread();
                checkingWalletStatus = false;
            }
        }
    }
}

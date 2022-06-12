using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlgoSdk.WalletConnect.Editor
{
    public class WalletConnectAccountEditorWindow : EditorWindow
    {
        static readonly ClientMeta defaultDappMeta = new ClientMeta
        {
            Description = "This is a connection to the Unity Editor, used to interact with the Algorand Blockchain during development time.",
            IconUrls = new[]
            {
                "https://unity3d.com/profiles/unity3d/themes/unity/images/pages/branding_trademarks/unity-tab.png",
                "https://unity3d.com/profiles/unity3d/themes/unity/images/pages/branding_trademarks/unity-tab.png",
                "https://unity3d.com/profiles/unity3d/themes/unity/images/pages/branding_trademarks/unity-tab.png"
            },
            Url = "https://unity.com",
            Name = "Unity Editor"
        };

        [SerializeField]
        VisualTreeAsset visualTree;

        [SerializeField]
        WalletConnectAccountObject asset;

        [SerializeField]
        string statusText;

        WalletConnectAccountObject unsavedAsset;

        VisualElement configureSessionContent;
        VisualElement connectedContent;
        VisualElement requestingHandshakeContent;

        Image qrCodeImage;
        Button startSessionButton;


        [MenuItem("AlgoSdk/Connect Account/WalletConnect")]
        public static void ShowMenu()
        {
            var window = GetWindow<WalletConnectAccountEditorWindow>("Connect Account via WalletConnect");
            var asset = ScriptableObject.CreateInstance<WalletConnectAccountObject>();
            asset.SessionData = SessionData.InitSession(defaultDappMeta, DefaultBridge.MainBridge);
            window.asset = asset;
        }

        public static void ShowMenu(string assetGuid)
        {
            var window = GetWindow<WalletConnectAccountEditorWindow>("Connect Account via WalletConnect");
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var asset = AssetDatabase.LoadAssetAtPath<WalletConnectAccountObject>(assetPath);
            asset.SessionData = asset.SessionData.Reinitialize();
            window.asset = asset;
        }

        void CreateGUI()
        {
            var gui = visualTree.CloneTree();

            connectedContent = gui.Query<VisualElement>("ConnectedContent").First();
            configureSessionContent = gui.Query<VisualElement>("ConfigureSessionContent").First();
            requestingHandshakeContent = gui.Query<VisualElement>("RequestingHandshakeContent").First();

            var statusTextField = gui.Query<TextField>("StatusField").First();

            startSessionButton = configureSessionContent.Query<Button>("StartSessionButton").First();
            startSessionButton.clicked += StartSession;

            var randomizeBridgeUrlButton = configureSessionContent.Query<Button>("RandomizeButton").First();
            randomizeBridgeUrlButton.clicked += SetRandomBridgeUrl;
            randomizeBridgeUrlButton.Add(new Image
            {
                image = EditorGUIUtility.IconContent("Refresh").image
            });

            var cancelHandshakeButton = requestingHandshakeContent.Query<Button>("CancelHandshakeButton").First();
            cancelHandshakeButton.clicked += ResetWalletConnection;

            var saveAssetButton = connectedContent.Query<Button>("SaveAssetButton").First();
            saveAssetButton.clicked += SaveAsset;

            qrCodeImage = new Image();
            requestingHandshakeContent
                .Query<VisualElement>("QrCodeContent")
                .First()
                .Add(qrCodeImage)
                ;

            var serializedObject = new SerializedObject(this);
            gui.Bind(serializedObject);

            var assetField = gui.Query<ObjectField>("AssetField").First();
            assetField.RegisterValueChangedCallback((evt) =>
            {
                configureSessionContent.Unbind();
                if (asset)
                {
                    var isSaved = AssetDatabase.Contains(asset);
                    SetDisplay(saveAssetButton, !isSaved);
                    assetField.SetEnabled(isSaved);
                    configureSessionContent.Bind(new SerializedObject(asset));
                }
            });
            assetField.objectType = typeof(WalletConnectAccountObject);

            rootVisualElement.Add(gui);
        }

        void Update()
        {
            var status = asset ? asset.ConnectionStatus : default;
            statusText = ObjectNames.NicifyVariableName(status.ToString());

            SetDisplay(configureSessionContent, asset && status <= SessionStatus.NoWalletConnected);
            SetDisplay(requestingHandshakeContent, asset && status == SessionStatus.RequestingWalletConnection);
            SetDisplay(connectedContent, asset && status == SessionStatus.WalletConnected);
        }

        void OnDestroy()
        {
            if (asset)
                asset.EndSession();
        }

        void StartSession()
        {
            StartSessionAsync().Forget();
        }

        async UniTaskVoid StartSessionAsync()
        {
            startSessionButton.SetEnabled(false);
            await UniTask.SwitchToThreadPool();
            try
            {
                await asset.BeginSession();
            }
            finally
            {
                await UniTask.SwitchToMainThread();
                startSessionButton.SetEnabled(true);
            }
            switch (asset.ConnectionStatus)
            {
                case SessionStatus.NoWalletConnected:
                case SessionStatus.RequestingWalletConnection:
                    RequestConnection();
                    break;
            }
        }

        void RequestConnection()
        {
            RequestConnectionAsync().Forget();
        }

        async UniTaskVoid RequestConnectionAsync()
        {
            try
            {
                var handshakeUrl = asset.RequestWalletConnection();
                qrCodeImage.image = handshakeUrl.ToQrCodeTexture();
                await UniTask.SwitchToTaskPool();
                await asset.WaitForWalletApproval();
                await UniTask.SwitchToMainThread();
                qrCodeImage.image = null;
            }
            catch (OperationCanceledException)
            {
            }
        }

        void ResetWalletConnection()
        {
            if (!asset)
                return;

            asset.EndSession();
            asset.ResetSessionData();
        }

        void SetRandomBridgeUrl()
        {
            if (!asset)
                return;

            asset.BridgeUrl = DefaultBridge.GetRandomBridgeUrl();
        }

        void SaveAsset()
        {
            if (!asset)
                return;

            var path = EditorUtility.SaveFilePanelInProject("Save WalletConnect Session", "WalletConnectAccount.asset", "asset", "Choose a location to save this connected account.");
            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(asset, path);
                Close();
            }
        }

        void SetDisplay(VisualElement element, bool isDisplayed)
        {
            element.style.display = isDisplayed ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}

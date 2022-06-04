using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlgoSdk.WalletConnect.Editor
{
    public class WalletConnectEditorWindow : EditorWindow
    {
        [SerializeField]
        WalletConnectAccountAsset asset;

        [SerializeField]
        VisualTreeAsset visualTree;

        [SerializeField]
        Texture qrCodeTexture;

        [SerializeField]
        string statusText;

        [SerializeField]
        string requestingConnectionText;

        VisualElement configureSessionContent;
        VisualElement connectedContent;
        VisualElement requestingHandshakeContent;

        public static void Show(string assetGuid)
        {
            var window = GetWindow<WalletConnectEditorWindow>("Connect to wallet");
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            window.asset = AssetDatabase.LoadAssetAtPath<WalletConnectAccountAsset>(assetPath);
        }

        void CreateGUI()
        {
            var serializedObject = new SerializedObject(this);
            var gui = visualTree.CloneTree();

            connectedContent = gui.Query<VisualElement>("ConnectedContent").First();
            configureSessionContent = gui.Query<VisualElement>("ConfigureSessionContent").First();
            requestingHandshakeContent = gui.Query<VisualElement>("RequestingHandshakeContent").First();

            var statusTextField = gui.Query<TextField>("StatusField").First();

            var assetField = gui.Query<ObjectField>("AssetField").First();
            assetField.RegisterValueChangedCallback((evt) =>
            {
                configureSessionContent.Unbind();
                if (asset)
                {
                    configureSessionContent.Bind(new SerializedObject(asset));
                }
            });

            var startSessionButton = configureSessionContent.Query<Button>("StartSessionButton").First();
            startSessionButton.clicked += StartSession;

            var randomizeBridgeUrlButton = configureSessionContent.Query<Button>("RandomizeButton").First();
            randomizeBridgeUrlButton.clicked += SetRandomBridgeUrl;
            randomizeBridgeUrlButton.Add(new Image
            {
                image = EditorGUIUtility.IconContent("Refresh").image
            });

            var connectNewWalletButton = connectedContent.Query<Button>("ConnectNewWalletButton").First();
            connectNewWalletButton.clicked += ResetWalletConnection;

            var cancelHandshakeButton = requestingHandshakeContent.Query<Button>("CancelHandshakeButton").First();
            cancelHandshakeButton.clicked += ResetWalletConnection;

            var qrCodeImage = new Image();
            requestingHandshakeContent
                .Query<VisualElement>("QrCodeContent")
                .First()
                .Add(qrCodeImage)
                ;

            var requestingConnectionLabel = requestingHandshakeContent.Query<Label>("HandshakeStatusLabel").First();

            gui.Bind(serializedObject);
            var qrCodeTextureProp = serializedObject.FindProperty(nameof(qrCodeTexture));
            gui.TrackPropertyValue(qrCodeTextureProp, (prop) => qrCodeImage.image = (Texture)prop.objectReferenceValue);
            var requestingConnectionProp = serializedObject.FindProperty(nameof(requestingConnectionText));
            gui.TrackPropertyValue(requestingConnectionProp, (prop) => requestingConnectionLabel.text = prop.stringValue);
            rootVisualElement.Add(gui);
        }

        void Update()
        {
            var status = asset?.Account.ConnectionStatus ?? default;
            statusText = ObjectNames.NicifyVariableName(status.ToString());
            requestingConnectionText = "Performing Handshake" + LoadingTextAnimationEllipses();

            configureSessionContent.visible = asset && status <= SessionStatus.NoConnection;
            requestingHandshakeContent.visible = asset && status == SessionStatus.RequestingConnection;
            connectedContent.visible = asset && status == SessionStatus.Connected;
        }

        void OnDestroy()
        {
            asset?.Account.EndSession();
        }

        string LoadingTextAnimationEllipses()
        {
            var time = EditorApplication.timeSinceStartup % 1;
            if (time > 0.67)
                return "...";
            else if (time > 0.33)
                return "..";
            else
                return ".";
        }

        void StartSession()
        {
            if (!asset)
                return;

            asset.Account.BeginSession();
            switch (asset.Account.ConnectionStatus)
            {
                case SessionStatus.NoConnection:
                    RequestConnection();
                    break;
                case SessionStatus.RequestingConnection:
                    ContinueHandshake();
                    break;
            }
        }

        void ResetWalletConnection()
        {
            if (!asset)
                return;

            asset.Account.EndSession();
            asset.Account.SessionData = SavedSession.InitSession(asset.Account.SessionData.DappMeta, asset.Account.SessionData.BridgeUrl);
        }

        void SetRandomBridgeUrl()
        {
            if (!asset)
                return;

            asset.Account.SessionData.BridgeUrl = DefaultBridge.GetRandomBridgeUrl();
        }

        void RequestConnection()
        {
            RequestConnectionAsync();
        }

        async void RequestConnectionAsync()
        {
            try
            {
                var handshakeUrl = await asset.Account.StartNewWalletConnection();
                await PerformHandshake(handshakeUrl);
            }
            catch (OperationCanceledException)
            {
            }
        }

        void ContinueHandshake()
        {
            ContinueHandshakeAsync();
        }

        async void ContinueHandshakeAsync()
        {
            var handshakeUrl = asset.Account.ContinueHandshake();
            await PerformHandshake(handshakeUrl);
        }

        async Task PerformHandshake(HandshakeUrl handshakeUrl)
        {
            try
            {
                qrCodeTexture = handshakeUrl.ToQrCodeTexture();
                await asset.Account.WaitForConnectionApproval();
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}

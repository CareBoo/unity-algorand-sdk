using Cysharp.Threading.Tasks;
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
        ClientMeta dappMeta;

        [SerializeField]
        string bridgeUrl;

        [SerializeField]
        Texture qrCodeTexture;

        ConfigureSessionElements configureSessionGUI;
        RequestConnectionElements requestConnectionGUI;

        Label statusLabel;


        public static void Show(WalletConnectAccountAsset asset)
        {
            var window = GetWindow<WalletConnectEditorWindow>("Connect to wallet");
            window.asset = asset;
            var sessionData = window.asset.Account.SessionData;
            window.dappMeta = sessionData.DappMeta;
        }

        void CreateGUI()
        {
            bridgeUrl = DefaultBridge.GetRandomBridgeUrl();
            VisualElement root = rootVisualElement;

            var assetProp = new PropertyField { bindingPath = nameof(asset) };
            assetProp.SetEnabled(false);
            root.Add(assetProp);

            configureSessionGUI = new ConfigureSessionElements(StartSession);
            root.Add(configureSessionGUI);

            requestConnectionGUI = new RequestConnectionElements(RequestConnection, qrCodeTexture);
            requestConnectionGUI.visible = false;
            root.Add(requestConnectionGUI);

            statusLabel = new Label();
            root.Add(statusLabel);

            root.Bind(new SerializedObject(this));
        }

        void Update()
        {
            statusLabel.text = asset?.Account.ConnectionStatus.ToString();
        }

        void OnDestroy()
        {
            asset?.Account.EndSession();
        }

        void StartSession()
        {
            asset.Account.SessionData = SavedSession.InitSession(dappMeta, bridgeUrl);
            asset.Account.BeginSession();
            configureSessionGUI.visible = false;
            requestConnectionGUI.visible = true;
        }

        void RequestConnection()
        {
            RequestConnectionAsync().Forget();
        }

        async UniTaskVoid RequestConnectionAsync()
        {
            var handshakeUrl = await asset.Account.StartNewWalletConnection();
            qrCodeTexture = handshakeUrl.ToQrCodeTexture();
            requestConnectionGUI.QrCode.image = qrCodeTexture;
            requestConnectionGUI.RequestConnectionButton.visible = false;
        }

        class ConfigureSessionElements : VisualElement
        {
            public PropertyField DappMetaField { get; protected set; }
            public PropertyField BridgeUrlField { get; protected set; }
            public Button StartSessionButton { get; protected set; }

            public ConfigureSessionElements(System.Action startSession) : base()
            {
                DappMetaField = new PropertyField { bindingPath = nameof(dappMeta) };
                BridgeUrlField = new PropertyField { bindingPath = nameof(bridgeUrl) };
                StartSessionButton = new Button(startSession) { text = "Start Session" };

                Add(DappMetaField);
                Add(BridgeUrlField);
                Add(StartSessionButton);
            }
        }

        class RequestConnectionElements : VisualElement
        {
            public Image QrCode { get; protected set; }
            public Button RequestConnectionButton { get; protected set; }

            public RequestConnectionElements(System.Action requestConnection, Texture qrCodeTexture) : base()
            {
                QrCode = new Image { image = qrCodeTexture };
                RequestConnectionButton = new Button(requestConnection) { text = "Request Connection" };

                Add(QrCode);
                Add(RequestConnectionButton);
            }
        }
    }
}

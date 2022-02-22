using AlgoSdk.WalletConnect;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WalletConnectManager : MonoBehaviour
{
    public ClientMeta DappMeta;

    public string BridgeUrl;

    Texture2D qrCode;

    AlgorandWalletConnectSession session;

    void Start()
    {
        StartWalletConnect().Forget();
    }

    void OnGUI()
    {
        if (qrCode != null)
        {
            if (GUI.Button(new Rect(300, 100, 256, 256), qrCode, GUIStyle.none))
            {
                Debug.Log($"Connection status: {session?.ConnectionStatus ?? AlgorandWalletConnectSession.Status.Unknown}");
            }
        }
    }

    async UniTaskVoid StartWalletConnect()
    {
        session = new AlgorandWalletConnectSession(DappMeta, BridgeUrl);
        var connecting = session.Connect();
        Debug.Log($"session url: {session.Url}");
        qrCode = QrCode.Generate(session.Url);
        await connecting;
        qrCode = null;
    }
}

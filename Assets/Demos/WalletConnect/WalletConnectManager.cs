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
        var url = await session.StartConnection();
        UnityEngine.Debug.Log(url);
        qrCode = QrCode.Generate(url);
        await session.WaitForConnectionApproval();
    }
}

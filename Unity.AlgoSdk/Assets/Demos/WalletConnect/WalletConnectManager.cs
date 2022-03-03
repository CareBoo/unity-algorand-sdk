using AlgoSdk;
using AlgoSdk.QrCode;
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
                var status = session?.ConnectionStatus ?? AlgorandWalletConnectSession.Status.Unknown;
                Debug.Log($"Connection status: {status}");
                if (status == AlgorandWalletConnectSession.Status.Connected)
                    TestTransaction().Forget();
            }
        }
    }

    async UniTaskVoid StartWalletConnect()
    {
        session = new AlgorandWalletConnectSession(DappMeta, BridgeUrl);
        var url = await session.StartConnection();
        Debug.Log(url);
        qrCode = QrCode.Generate(url);
        await session.WaitForConnectionApproval();
        Debug.Log($"accounts:\n{AlgoApiSerializer.SerializeJson(session.Accounts)}");
    }

    async UniTaskVoid TestTransaction()
    {
        var algod = new AlgodClient(
            "https://testnet-algorand.api.purestake.io/ps2",
            ("x-api-key", "jsxO0QnF9PcPETV0LQ2z2GgRSd18eoM9Q4VwrgLc")
        );
        var (txnParamsErr, txnParams) = await algod.GetSuggestedParams();
        if (txnParamsErr)
        {
            Debug.LogError((string)txnParamsErr);
            return;
        }

        var txn = Transaction.Payment(
            session.Accounts[0],
            txnParams,
            session.Accounts[0],
            0
        );

        var walletTxn = new WalletTransaction
        {
            Txn = AlgoApiSerializer.SerializeMessagePack(txn),
            Message = "This is a test"
        };

        var (err, signedTxns) = await session.SignTransactions(new[] { walletTxn });
        if (err)
        {
            Debug.LogError(err);
            return;
        }
        Debug.Log($"Got signed transactions:\n{AlgoApiSerializer.SerializeJson(signedTxns)}");
    }
}

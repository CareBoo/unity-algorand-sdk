using Algorand.Unity.WalletConnect;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Algorand.Unity.Samples.WalletConnect
{
    public class WalletConnectCanvas : MonoBehaviour
    {
        public Text connectionStatus;
        public Text transactionStatus;
        public Text connectedAccount;
        public Text amount;

        [FormerlySerializedAs("connectingTOWallet")]
        public Text connectingToWallet;

        public Image qrCodeDisplay;

        public GameObject requestingConnectionDisplay, connectedDisplay, notConnectedDisplay;

        public Button sendTestTransactionButton;

        public void SetCanvasDisplay(SessionStatus status)
        {
            string currentStatus = "UNKNOWN";

            requestingConnectionDisplay.SetActive(false);
            connectedDisplay.SetActive(false);
            notConnectedDisplay.SetActive(false);

            switch (status)
            {
                case (SessionStatus.RequestingWalletConnection):
                    requestingConnectionDisplay.SetActive(true);
                    currentStatus = "Requesting Connection";
                    break;
                case (SessionStatus.WalletConnected):
                    connectedDisplay.SetActive(true);
                    currentStatus = "Connected";
                    break;
                case (SessionStatus.NoWalletConnected):
                    notConnectedDisplay.SetActive(true);
                    currentStatus = "Disconnected";
                    break;
            }

            connectionStatus.text = $"Connection Status: {currentStatus}";
        }

        public void SetConnectionStatus(string status)
        {
            connectionStatus.text = $"Connection Status: {status}";
        }

        public void SetQRCode(Texture2D qrCode)
        {
            qrCodeDisplay.sprite = Sprite.Create(qrCode, new Rect(0, 0, qrCode.width, qrCode.height), Vector2.zero);
        }
    }
}
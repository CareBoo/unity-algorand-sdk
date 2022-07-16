using AlgoSdk.QrCode;
using UnityEngine;
using UnityEngine.Networking;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// An HTTP-escaped Url for initiating the walletconnect connection.
    /// </summary>
    /// <remarks>
    /// Can be converted into a QrCode or used to deeplink on mobile to
    /// supported wallets.
    /// </remarks>
    public readonly struct HandshakeUrl
    {
        readonly string url;

        public string Url => url;

        public HandshakeUrl(
            string topic,
            string version,
            string bridgeUrl,
            Hex key
        )
        {
            var escapedBridgeUrl = UnityWebRequest.EscapeURL(bridgeUrl);
            url = $"wc:{topic}@{version}?bridge={escapedBridgeUrl}&key={key}&algorand=true";
        }

        public Texture2D ToQrCodeTexture()
        {
            return QrCodeUtility.GenerateTexture(url);
        }
    }
}

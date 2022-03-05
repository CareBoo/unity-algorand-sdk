using System;
using AlgoSdk.QrCode;
using UnityEngine;
using UnityEngine.Networking;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// An HTTP-escaped Url for initiating the walletconnect connection.
    /// </summary>
    /// <remarks>
    /// Can be converted into a QrCode.
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
            url = $"wc:{topic}@{version}?bridge={UnityWebRequest.EscapeURL(bridgeUrl)}&key={key}";
        }

        public Texture2D ToQrCodeTexture()
        {
            return QrCodeUtility.GenerateTexture(url);
        }

        public static implicit operator string(HandshakeUrl handshakeUrl)
        {
            return handshakeUrl.Url;
        }
    }
}

using UnityEngine;
using Algorand.Unity.WalletConnect.Core;
using System.Collections;
using Algorand.Unity.JsonRpc;
using UnityEngine.UI;
using Algorand.Unity.QrCode;
using Unity.Collections.LowLevel.Unsafe;
using Algorand.Unity.Crypto;
using Crypto = Algorand.Unity.Crypto;
using Algorand.Unity;
using Unity.Collections;
using Algorand.Unity.Collections;
using System;
using Algorand.Unity.LowLevel;

namespace WalletConnectV2
{
    public class WalletConnectClient : MonoBehaviour
    {
        public Image qrCodeImage;

        public string relayUrl = "wss://relay.walletconnect.org";
        public string projectId = "da87b3345726d69030aa66218202ec19";
        public UserAgent userAgent = new UserAgent
        {
            protocol = "wc",
            version = "2",
            sdkVersion = "5.0.0-exp.1"
        };

        public Metadata clientMetadata = new Metadata
        {
            Description = "test app",
            Url = "https://github.com/careboo/unity-algorand-sdk",
            Icons = new[] {
                "https://media.githubusercontent.com/media/CareBoo/unity-algorand-sdk/main/docs/images/logo_256.png",
                "https://media.githubusercontent.com/media/CareBoo/unity-algorand-sdk/main/docs/images/logo_256.png",
                "https://media.githubusercontent.com/media/CareBoo/unity-algorand-sdk/main/docs/images/logo_256.png"
            },
            Name = "Algorand SDK for Unity"
        };

        IEnumerator Start()
        {
            var relayClient = new RelayClient(relayUrl, projectId, userAgent);
            relayClient.Connect();

            var msg = relayClient.Poll();
            while (msg.Type == JsonRpcEventType.Nothing)
            {
                yield return null;
                msg = relayClient.Poll();
            }

            switch (msg.Type)
            {
                case JsonRpcEventType.Open:
                    Debug.Log("Got open");
                    break;
                case JsonRpcEventType.Close:
                    Debug.LogError($"Got close with reason: {msg.Reason}");
                    yield break;
                case JsonRpcEventType.Error:
                    Debug.LogError($"Got error with message: {msg.Error}");
                    yield break;
                default:
                    Debug.LogError($"Got unexpected message type: {msg.Type}");
                    yield break;
            }

            var pairingMethods = new PairingMethods
            {
                signMethods = new[] { "wc_sessionPropose", "algo_signTxn" }
            };
            var pairingUri = new PairingUri(pairingMethods);
            var pairingUriString = pairingUri.ToString();
            Debug.Log(pairingUriString);
            qrCodeImage.sprite = QrCodeUtility.GenerateSprite(pairingUriString);

            var topic = pairingUri.topic;
            var key = UnsafeUtility.As<SymKey, ChaCha20Poly1305.Key>(ref pairingUri.symKey);
            var nonce = Crypto.Random.Bytes<ChaCha20Poly1305.Nonce>();

            var (publicKey, secretKey) = X25519.Keygen();
            Span<char> publicKeyHex = stackalloc char[X25519.PublicKey.Size * 2];
            var hexError = HexConverter.ToHex(publicKey.AsReadOnlySpan(), publicKeyHex, out _);
            if (hexError > 0) throw new Exception($"Failed to convert public key to hex: {hexError}");

            var proposer = new Participant
            {
                PublicKey = publicKeyHex.ToString(),
                Metadata = clientMetadata
            };

            var sessionProposeRequest = new SessionProposeRequestPayload(proposer);

            var request = new JsonRpcRequest<SessionProposeRequestPayload>
            {
                Id = RpcId.GenerateId(),
                JsonRpc = "2.0",
                Method = "wc_sessionPropose",
                Params = sessionProposeRequest
            };

            string message;
            using (var requestJson = AlgoApiSerializer.SerializeJson(request, Allocator.Temp))
            using (var encryptedEnvelope = Envelope.EncryptType0(requestJson.AsArray(), key, nonce, Allocator.Temp))
            {
                message = Convert.ToBase64String(encryptedEnvelope.ToArray());
            }
            ulong ttl = 5 * 60;
            var sessionProposeRelayId = relayClient.Publish(topic, message, 1101, ttl);

            var expiry = (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds() + ttl;

            do
            {
                yield return null;
                msg = relayClient.Poll();
            }
            while (msg.Type == JsonRpcEventType.Nothing);

            switch (msg.Type)
            {
                case JsonRpcEventType.Response when msg.Response.Id == sessionProposeRelayId:
                    Debug.Log($"Got publish response: {AlgoApiSerializer.SerializeJson(msg.Response)}");
                    break;
                case JsonRpcEventType.Response:
                    Debug.LogError($"Got response with unexpected id: {AlgoApiSerializer.SerializeJson(msg.Response)}");
                    yield break;
                default:
                    Debug.LogError($"Got unexpected message type: {msg.Type}");
                    yield break;
            }

            var subscribeId = relayClient.Subscribe(topic);

            do
            {
                yield return null;
                msg = relayClient.Poll();
            }
            while (msg.Type == JsonRpcEventType.Nothing);

            switch (msg.Type)
            {
                case JsonRpcEventType.Response when msg.Response.Id == subscribeId:
                    Debug.Log($"Got subscription response: {AlgoApiSerializer.SerializeJson(msg.Response)}");
                    break;

                default:
                    Debug.LogError($"Got unexpected message type: {msg.Type}");
                    yield break;
            }

            do
            {
                yield return null;
                if ((ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= expiry)
                {
                    Debug.LogError("Session proposal expired");
                    yield break;
                }
                msg = relayClient.Poll();
            }
            while (msg.Type == JsonRpcEventType.Nothing);

            switch (msg.Type)
            {
                case JsonRpcEventType.Request:
                    Debug.Log($"Got request: {AlgoApiSerializer.SerializeJson(msg.Request)}");
                    break;
                case JsonRpcEventType.Response:
                    Debug.Log($"Got response: {AlgoApiSerializer.SerializeJson(msg.Response)}");
                    break;
                default:
                    Debug.LogError($"Got unexpected message type: {msg.Type}");
                    yield break;
            }
        }
    }
}

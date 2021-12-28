using System;
using AlgoSdk.LowLevel;
using Unity.Collections;
using UnityEngine.Events;

namespace AlgoSdk.WalletConnect
{
    public class AlgorandWalletConnectSession : IWalletConnectSession<AlgorandWalletConnectSession>
    {
        Hex key;

        string bridgeUrl;

        string handshakeTopic;

        string clientId;

        long handshakeId;

        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect { get; set; }

        public UnityEvent OnSessionDisconnect { get; set; }

        public UnityEvent<AlgorandWalletConnectSession> OnSend { get; set; }

        public UnityEvent<WalletConnectSessionData> OnSessionUpdate { get; set; }

        public string Url => $"wc:{handshakeTopic}@{Version}?bridge={bridgeUrl}&key={key}";

        public Address[] Accounts => throw new System.NotImplementedException();

        public string Version => "1";

        public bool SessionConnected { get; protected set; }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        void StartSession()
        {
            handshakeTopic = Guid.NewGuid().ToString();
            clientId = Guid.NewGuid().ToString();
            GenerateKey();
        }

        void GenerateKey()
        {
            using var secret = new NativeByteArray(32, Allocator.Temp);
            AlgoSdk.Crypto.Random.Randomize(secret);
            key = secret.Data.ToArray();
        }
    }
}

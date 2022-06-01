using System;
using UnityEngine.Events;

namespace AlgoSdk.WalletConnect
{
    [Serializable]
    public class WalletConnectSessionEvents
    {
        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect = new UnityEvent<AlgorandWalletConnectSession>();
        public UnityEvent<string> OnSessionDisconnect = new UnityEvent<string>();
        public UnityEvent<WalletConnectSessionData> OnSessionUpdate = new UnityEvent<WalletConnectSessionData>();
    }
}

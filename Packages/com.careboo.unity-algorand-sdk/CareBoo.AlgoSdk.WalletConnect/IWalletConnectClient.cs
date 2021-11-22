using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

namespace AlgoSdk.WalletConnect
{
    public interface IWalletConnectSession<TSession> : IDisposable
        where TSession : IWalletConnectSession<TSession>
    {
        UnityEvent<TSession> OnSessionConnect { get; set; }
        UnityEvent OnSessionDisconnect { get; set; }
        UnityEvent<TSession> OnSend { get; set; }
        UnityEvent<WalletConnectSessionData> OnSessionUpdate { get; set; }

        string Uri { get; }
        Address[] Accounts { get; }

        UniTask CreateSession(int chainId);
    }
}

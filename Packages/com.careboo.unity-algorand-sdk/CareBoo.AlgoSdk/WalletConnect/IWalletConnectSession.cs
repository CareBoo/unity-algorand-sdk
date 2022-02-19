using System;
using UnityEngine.Events;

namespace AlgoSdk.WalletConnect
{
    public interface IActiveWalletConnectSession<TSession> : IWalletConnectSessionData, IDisposable
        where TSession : IActiveWalletConnectSession<TSession>
    {
        UnityEvent<TSession> OnSessionConnect { get; set; }
        UnityEvent OnSessionDisconnect { get; set; }
        UnityEvent<WalletConnectSessionData> OnSessionUpdate { get; set; }
    }

    public interface IWalletConnectSessionData
    {
        /// <summary>
        /// The UUID of this client.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// The bridge used to communicate with the session.
        /// </summary>
        string BridgeUrl { get; }

        /// <summary>
        /// The key used to encrypt/decrypt the payloads.
        /// </summary>
        Hex Key { get; }

        /// <summary>
        /// The UUID of the connecting peer.
        /// </summary>
        string PeerId { get; }

        /// <summary>
        /// 
        /// </summary>
        long HandshakeId { get; }

        /// <summary>
        /// The ID of the network connected (test, beta, main, ...)
        /// </summary>
        int NetworkId { get; }

        /// <summary>
        /// The accounts that the Wallet manages.
        /// </summary>
        Address[] Accounts { get; }

        /// <summary>
        /// The Identifier of the blockchain connected.
        /// </summary>
        int ChainId { get; }

        /// <summary>
        /// Meta information about the Dapp connected in this session.
        /// </summary>
        ClientMeta DappMeta { get; }

        /// <summary>
        /// Meta information about the Wallet connected in this session.
        /// </summary>
        ClientMeta WalletMeta { get; }

        /// <summary>
        /// Url that wallets will connect to.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// WalletConnect protocol Version.
        /// </summary>
        string Version { get; }
    }
}

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace AlgoSdk.WalletConnect
{
    [Serializable]
    public struct WalletConnectAccount
        : IAccount
        , IAsyncSigner
    {
        [SerializeField]
        public SavedSession SessionData;

        [SerializeField]
        WalletConnectSessionEvents sessionEvents;

        AlgorandWalletConnectSession session;

        public WalletConnectAccount(SavedSession sessionData, WalletConnectSessionEvents sessionEvents = null)
        {
            this.SessionData = sessionData;
            this.sessionEvents = sessionEvents ?? new WalletConnectSessionEvents();
            session = null;
        }

        public WalletConnectSessionEvents SessionEvents => sessionEvents;

        public Address Address => SessionData.Accounts?[0] ?? Address.Empty;

        public SessionStatus ConnectionStatus => session?.ConnectionStatus ?? default;

        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect => sessionEvents?.OnSessionConnect;

        public UnityEvent<string> OnSessionDisconnect => sessionEvents?.OnSessionDisconnect;

        public UnityEvent<WalletConnectSessionData> OnSessionUpdate => sessionEvents?.OnSessionUpdate;

        public void BeginSession()
        {
            session = new AlgorandWalletConnectSession(SessionData, sessionEvents);
        }

        public void EndSession()
        {
            if (session == null)
            {
                return;
            }
            SessionData = session.Save();
            session.Dispose();
            session = null;
        }

        public async UniTask<HandshakeUrl> StartNewWalletConnection(CancellationToken cancellationToken = default)
        {
            CheckSession();
            return await session.StartConnection(cancellationToken);
        }

        public async UniTask WaitForConnectionApproval(CancellationToken cancellationToken = default)
        {
            CheckSession();
            await session.WaitForConnectionApproval(cancellationToken);
        }

        public void DisconnectWalletConnection(string reason = default)
        {
            CheckSession();
            session.Disconnect(reason);
        }

        public async UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(
            T[] txns,
            TxnIndices txnsToSign,
            CancellationToken cancellationToken = default
            )
            where T : ITransaction, IEquatable<T>
        {
            CheckSession();

            if (txns == null)
                throw new ArgumentNullException(nameof(txns));

            if (txns.Length == 0 || txns.Length > 16)
                throw new ArgumentException("Must have 1-16 transactions", nameof(txns));

            var walletTxns = new WalletTransaction[txns.Length];
            for (var i = 0; i < txns.Length; i++)
            {
                var signers = txnsToSign.ContainsIndex(i)
                    ? new[] { Address }
                    : new Address[0];
                walletTxns[i] = WalletTransaction.New(
                    txns[i],
                    signers: signers
                );
            }

            var (error, signed) = await session.SignTransactions(walletTxns, cancellationToken: cancellationToken);
            if (error)
                throw new Exception(error.ToString());

            var result = new SignedTxn<T>[signed.Length];
            for (var i = 0; i < signed.Length; i++)
                result[i] = AlgoApiSerializer.DeserializeMessagePack<SignedTxn<T>>(signed[i]);
            return result;
        }

        void CheckSession()
        {
            if (session == null)
                throw new NullReferenceException("Session has not been initialized! Please call " + nameof(BeginSession) + " first.");
        }
    }
}

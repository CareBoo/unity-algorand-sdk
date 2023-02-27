using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Algorand.Unity.WalletConnect
{
    [Serializable]
    public class WalletConnectAccount
        : IWalletConnectAccount
    {
        [SerializeField] private SessionData sessionData;

        private AlgorandWalletConnectSession session;

        private CancellationTokenSource sessionCancellation;

        private bool beginningSession;

        /// <inheritdoc />
        public Address Address => sessionData.Accounts?[0] ?? Address.Empty;

        /// <inheritdoc />
        public SessionStatus ConnectionStatus => session?.ConnectionStatus ?? default;

        /// <inheritdoc />
        public SessionData SessionData
        {
            get => sessionData;
            set => sessionData = value;
        }

        /// <inheritdoc />
        public string BridgeUrl
        {
            get => sessionData.BridgeUrl;
            set => sessionData.BridgeUrl = value;
        }

        /// <inheritdoc />
        public ClientMeta DappMeta
        {
            get => sessionData.DappMeta;
            set => sessionData.DappMeta = value;
        }

        ~WalletConnectAccount()
        {
            EndSession();
        }

        /// <inheritdoc />
        public async UniTask BeginSession()
        {
            if (beginningSession)
            {
                while (beginningSession)
                {
                    await UniTask.Yield();
                    await UniTask.NextFrame();
                    return;
                }
            }
            beginningSession = true;
            try
            {
                sessionCancellation = new CancellationTokenSource();
                if (SessionData.Key.Data == null)
                    SessionData = SessionData.InitSession(DappMeta, BridgeUrl);
                session = new AlgorandWalletConnectSession(SessionData);
                session.OnSessionDisconnect += OnSessionDisconnect;
                await session.Connect(sessionCancellation.Token);
                sessionData = session.Save();
            }
            finally
            {
                beginningSession = false;
            }
        }

        /// <inheritdoc />
        public void EndSession()
        {
            if (sessionCancellation != null)
            {
                sessionCancellation.Cancel();
                sessionCancellation = null;
            }
            if (session == null)
            {
                return;
            }
            sessionData = session.Save();
            session.Dispose();
            session = null;
        }

        /// <inheritdoc />
        public void ResetSessionData()
        {
            sessionData = SessionData.InitSession(sessionData.DappMeta, sessionData.BridgeUrl);
        }

        /// <inheritdoc />
        public HandshakeUrl RequestWalletConnection()
        {
            CheckSession();
            var handshake = session.RequestWalletConnection();
            sessionData = session.Save();
            return handshake;
        }

        /// <inheritdoc />
        public async UniTask WaitForWalletApproval()
        {
            CheckSession();
            await session.WaitForWalletApproval(sessionCancellation.Token);
            sessionData = session.Save();
        }

        /// <inheritdoc />
        public void DisconnectWallet(string reason = default)
        {
            CheckSession();
            session.DisconnectWallet(reason);
        }

        /// <inheritdoc />
        public async UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(
            T[] txns,
            TxnIndices txnsToSign,
            CancellationToken cancellationToken = default
            )
            where T : ITransaction, IEquatable<T>
        {
            if (txns == null)
                throw new ArgumentNullException(nameof(txns));

            if (txns.Length == 0 || txns.Length > 16)
                throw new ArgumentException("Must have 1-16 transactions", nameof(txns));

            if (ConnectionStatus == SessionStatus.None)
                await BeginSession();

            var cancellation = CancellationTokenSource.CreateLinkedTokenSource(sessionCancellation.Token, cancellationToken);
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

            var (error, signed) = await session.SignTransactions(walletTxns, cancellationToken: cancellation.Token);
            if (error)
                throw new Exception(error.ToString());

            var result = new SignedTxn<T>[signed.Length];
            for (var i = 0; i < signed.Length; i++)
                result[i] = AlgoApiSerializer.DeserializeMessagePack<SignedTxn<T>>(signed[i]);
            return result;
        }

        private void CheckSession()
        {
            if (session == null)
                throw new NullReferenceException("Session has not been initialized! Please call " + nameof(BeginSession) + " first.");
        }

        private void OnSessionDisconnect(string reason)
        {
            sessionData = session.Save();
        }
    }
}

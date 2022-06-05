using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AlgoSdk.WalletConnect
{
    [Serializable]
    public struct WalletConnectAccount
        : IAccount
        , IAsyncSigner
    {
        [SerializeField]
        public SavedSession SessionData;

        AlgorandWalletConnectSession session;

        CancellationTokenSource sessionCancellation;

        public WalletConnectAccount(SavedSession sessionData)
        {
            this.SessionData = sessionData;
            session = null;
            sessionCancellation = null;
        }

        public Address Address => SessionData.Accounts?[0] ?? Address.Empty;

        public SessionStatus ConnectionStatus => session?.ConnectionStatus ?? default;

        public async UniTask BeginSession()
        {
            session = new AlgorandWalletConnectSession(SessionData);
            sessionCancellation = new CancellationTokenSource();
            await session.Connect(sessionCancellation.Token);
        }

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
            SessionData = session.Save();
            session.Dispose();
            session = null;
        }

        public HandshakeUrl RequestHandshake()
        {
            CheckSession();
            return session.RequestHandshake();
        }

        public async UniTask WaitForWalletConnectionApproval()
        {
            CheckSession();
            await session.WaitForWalletConnectionApproval(sessionCancellation.Token);
            SessionData = session.Save();
        }

        public void DisconnectWalletConnection(string reason = default)
        {
            CheckSession();
            session.DisconnectWalletConnection(reason);
        }

        public async UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(
            T[] txns,
            TxnIndices txnsToSign,
            CancellationToken cancellationToken = default
            )
            where T : ITransaction, IEquatable<T>
        {
            CheckSession();

            var cancellation = CancellationTokenSource.CreateLinkedTokenSource(sessionCancellation.Token, cancellationToken);

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

            var (error, signed) = await session.SignTransactions(walletTxns, cancellationToken: cancellation.Token);
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

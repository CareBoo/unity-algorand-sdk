using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AlgoSdk.WalletConnect
{
    [CreateAssetMenu(fileName = "NewWalletConnectAccount", menuName = "AlgoSdk/Accounts/WalletConnectAccount")]
    public class WalletConnectAccountAsset
        : ScriptableObject
        , IWalletConnectAccount
    {
        [SerializeField]
        WalletConnectAccount account;

        /// <inheritdoc />
        public Address Address => account.Address;

        /// <inheritdoc />
        public SessionData SessionData => account.SessionData;

        /// <inheritdoc />
        public string BridgeUrl
        {
            get => account.BridgeUrl;
            set => account.BridgeUrl = value;
        }

        /// <inheritdoc />
        public ClientMeta DappMeta
        {
            get => account.DappMeta;
            set => account.DappMeta = value;
        }

        /// <inheritdoc />
        public SessionStatus ConnectionStatus => account.ConnectionStatus;

        /// <inheritdoc />
        public UniTask BeginSession() => account.BeginSession();

        /// <inheritdoc />
        public void EndSession() => account.EndSession();

        /// <inheritdoc />
        public void ResetSessionData() => account.ResetSessionData();

        /// <inheritdoc />
        public HandshakeUrl RequestWalletConnection() => account.RequestWalletConnection();

        /// <inheritdoc />
        public UniTask WaitForWalletApproval() => account.WaitForWalletApproval();

        /// <inheritdoc />
        public void DisconnectWallet(string reason = default) => account.DisconnectWallet(reason);

        /// <inheritdoc />
        public UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(
            T[] txns, TxnIndices txnsToSign,
            CancellationToken cancellationToken = default
            )
            where T : ITransaction, IEquatable<T>
        {
            return account.SignTxnsAsync(txns, txnsToSign, cancellationToken);
        }
    }
}

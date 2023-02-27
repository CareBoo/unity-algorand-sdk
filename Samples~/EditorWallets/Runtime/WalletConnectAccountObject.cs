using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Algorand.Unity.WalletConnect
{
    public class WalletConnectAccountObject
        : AsyncSignerAccountObject
        , IWalletConnectAccount
    {
        [SerializeField] private WalletConnectAccount account = new WalletConnectAccount();

        /// <inheritdoc />
        public override Address Address => account.Address;

        /// <inheritdoc />
        public SessionData SessionData
        {
            get => account.SessionData;
            set => account.SessionData = value;
        }

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
        public override UniTask<SignedTxn<T>[]> SignTxnsAsync<T>(
            T[] txns, TxnIndices txnsToSign,
            CancellationToken cancellationToken = default
            )
        {
            return account.SignTxnsAsync(txns, txnsToSign, cancellationToken);
        }
    }
}

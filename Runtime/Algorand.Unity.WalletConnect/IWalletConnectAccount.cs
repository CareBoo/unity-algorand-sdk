using Cysharp.Threading.Tasks;

namespace Algorand.Unity.WalletConnect
{
    public interface IWalletConnectAccount
        : IAsyncAccountSigner
    {
        /// <summary>
		/// The status of the current wallet connected.
		/// </summary>
        SessionStatus ConnectionStatus { get; }

        /// <summary>
		/// Stored data regarding the session.
		/// </summary>
        SessionData SessionData { get; }

        /// <summary>
		/// The WalletConnect bridge url to connect to.
		/// </summary>
        string BridgeUrl { get; set; }

        /// <summary>
		/// The client information provided in this Dapp.
		/// </summary>
        ClientMeta DappMeta { get; set; }

        /// <summary>
		/// Start the WalletConnect session.
		/// </summary>
        UniTask BeginSession();

        /// <summary>
		/// End the current WalletConnect session, closing the websocket.
		/// </summary>
        /// <remarks>
        /// Does not forget the wallet connection, and it can be resumed using <see cref="BeginSession" />.
		/// </remarks>
        void EndSession();

        /// <summary>
		/// Re-initializes session data, keeping the BridgeUrl and the DappMeta, but regenerating other fields.
		/// </summary>
        void ResetSessionData();

        /// <summary>
		/// Requests a handshake to connect to a new Wallet.
		/// </summary>
        HandshakeUrl RequestWalletConnection();

        /// <summary>
		/// Wait for the handshake to be approved.
		/// </summary>
        UniTask WaitForWalletApproval();

        /// <summary>
		/// Disconnect and forget the wallet connection. If you want to close the session without
        /// forgetting the wallet, use <see cref="EndSession"/>.
		/// </summary>
        void DisconnectWallet(string reason = default);
    }
}

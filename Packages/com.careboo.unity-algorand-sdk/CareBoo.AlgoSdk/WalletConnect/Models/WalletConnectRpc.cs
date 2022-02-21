using System;

namespace AlgoSdk.WalletConnect
{
    public static class WalletConnectRpc
    {
        const string jsonRpcVersion = "2.0";

        /// <summary>
        /// Utility function for building a <see cref="JsonRpcRequest"/> used to start a new WalletConnect session.
        /// </summary>
        /// <param name="peerId">The UUID of the client.</param>
        /// <param name="peerMeta">The metadata of the client.</param>
        /// <param name="chainId">The id of the blockchain this request is for.</param>
        /// <returns>A <see cref="JsonRpcRequest"/> that can be used to start a new WalletConnect session.</returns>
        public static JsonRpcRequest SessionRequest(string peerId, ClientMeta peerMeta, int chainId)
        {
            var sessionRequest = new WalletConnectSessionRequest
            {
                PeerId = peerId,
                PeerMeta = peerMeta,
                ChainId = chainId
            };
            return SessionRequest(sessionRequest);
        }

        /// <summary>
        /// Utility function for building a <see cref="JsonRpcRequest"/> used to start a new WalletConnect session.
        /// </summary>
        /// <param name="sessionRequest">Parameters required to create the request.</param>
        /// <returns>A <see cref="JsonRpcRequest"/> that can be used to start a new WalletConnect session.</returns>
        public static JsonRpcRequest SessionRequest(WalletConnectSessionRequest sessionRequest)
        {
            const string method = "wc_sessionRequest";
            var requestParams = new AlgoApiObject[]
            {
                AlgoApiSerializer.SerializeJson(sessionRequest)
            };
            return new JsonRpcRequest
            {
                Id = GetRandomId(),
                JsonRpc = jsonRpcVersion,
                Method = method,
                Params = requestParams
            };
        }

        static ulong GetRandomId() => (ulong)UnityEngine.Random.Range(1, int.MaxValue);

        public static class Algorand
        {
            public const int ChainId = 4160;

            /// <summary>
            /// Builds a <see cref="JsonRpcRequest"/> used for signing transactions.
            /// </summary>
            /// <param name="transactions">
            /// The atomic transaction group of [1,16] transactions. Contains information about how to sign
            /// each transaction, and which ones to sign.
            /// </param>
            /// <param name="options">
            /// Optional options for signing the transactions, e.g. adding a message to the transaction group.
            /// </param>
            /// <returns>A <see cref="JsonRpcRequest"/> used for signing Algorand transactions.</returns>
            public static JsonRpcRequest SignTransactions(WalletTransaction[] transactions, SignTxnsOpts options = default)
            {
                if (transactions == null)
                    throw new ArgumentNullException(nameof(transactions));
                if (transactions.Length < 1 || transactions.Length > 16)
                    throw new ArgumentException($"must have [1,16] transactions, instead it was {transactions.Length}", nameof(transactions));

                const string method = "algo_signTxn";
                AlgoApiObject[] requestParams = options.Equals(default)
                    ? new AlgoApiObject[1] { AlgoApiSerializer.SerializeJson(transactions) }
                    : new AlgoApiObject[2] { AlgoApiSerializer.SerializeJson(transactions), AlgoApiSerializer.SerializeJson(options) }
                    ;

                return new JsonRpcRequest
                {
                    Id = GetRandomId(),
                    JsonRpc = jsonRpcVersion,
                    Method = method,
                    Params = requestParams
                };
            }
        }
    }
}

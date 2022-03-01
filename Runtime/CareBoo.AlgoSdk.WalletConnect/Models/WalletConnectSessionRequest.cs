using System;

namespace AlgoSdk.WalletConnect
{
    [AlgoApiObject]
    public partial struct WalletConnectSessionRequest
        : IEquatable<WalletConnectSessionRequest>
    {
        [AlgoApiField("peerId", null)]
        public string PeerId;

        [AlgoApiField("peerMeta", null)]
        public ClientMeta PeerMeta;

        [AlgoApiField("chainId", null)]
        public Optional<int> ChainId;

        public bool Equals(WalletConnectSessionRequest other)
        {
            return StringComparer.Equals(PeerId, other.PeerId)
                && PeerMeta.Equals(other.PeerMeta)
                && ChainId.Equals(other.ChainId)
                ;
        }
    }
}

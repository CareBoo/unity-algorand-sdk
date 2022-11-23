using System;

namespace Algorand.Unity.WalletConnect
{
    [AlgoApiObject]
    public partial struct WalletConnectSessionRequest
        : IEquatable<WalletConnectSessionRequest>
    {
        [AlgoApiField("peerId")]
        public string PeerId;

        [AlgoApiField("peerMeta")]
        public ClientMeta PeerMeta;

        [AlgoApiField("chainId")]
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

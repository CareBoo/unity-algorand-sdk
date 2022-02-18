using System;

namespace AlgoSdk.WalletConnect
{
    [AlgoApiObject]
    public struct WCSessionRequestParams
        : IEquatable<WCSessionRequestParams>
    {
        [AlgoApiField("peerId", null)]
        public string PeerId;

        [AlgoApiField("peerMeta", null)]
        public ClientMeta PeerMeta;

        [AlgoApiField("chainId", null)]
        public Optional<int> ChainId;

        public bool Equals(WCSessionRequestParams other)
        {
            return StringComparer.Equals(PeerId, other.PeerId)
                && PeerMeta.Equals(other.PeerMeta)
                && ChainId.Equals(other.ChainId)
                ;
        }
    }
}

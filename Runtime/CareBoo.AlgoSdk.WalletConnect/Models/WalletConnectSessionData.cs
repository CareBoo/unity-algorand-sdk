using System;

namespace AlgoSdk.WalletConnect
{
    [AlgoApiObject]
    public partial struct WalletConnectSessionData
        : IEquatable<WalletConnectSessionData>
    {
        [AlgoApiField("peerId", null)]
        public string PeerId;

        [AlgoApiField("peerMeta", null)]
        public ClientMeta PeerMeta;

        [AlgoApiField("approved", null)]
        public bool IsApproved;

        [AlgoApiField("chainId", null)]
        public int ChainId;

        [AlgoApiField("accounts", null)]
        public Address[] Accounts;

        public bool Equals(WalletConnectSessionData other)
        {
            return StringComparer.Equals(PeerId, other.PeerId)
                && PeerMeta.Equals(other.PeerMeta)
                && IsApproved.Equals(other.IsApproved)
                && ChainId.Equals(other.ChainId)
                && ArrayComparer.Equals(Accounts, other.Accounts)
                ;
        }
    }
}

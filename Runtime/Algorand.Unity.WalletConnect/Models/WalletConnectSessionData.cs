using System;

namespace Algorand.Unity.WalletConnect
{
    [AlgoApiObject]
    public partial struct WalletConnectSessionData
        : IEquatable<WalletConnectSessionData>
    {
        [AlgoApiField("peerId")]
        public string PeerId;

        [AlgoApiField("peerMeta")]
        public ClientMeta PeerMeta;

        [AlgoApiField("approved")]
        public bool IsApproved;

        [AlgoApiField("chainId")]
        public int ChainId;

        [AlgoApiField("accounts")]
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

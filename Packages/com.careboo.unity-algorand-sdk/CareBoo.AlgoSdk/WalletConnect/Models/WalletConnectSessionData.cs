namespace AlgoSdk.WalletConnect
{
    public struct WalletConnectSessionData
    {
        public string PeerId;
        public ClientMeta PeerMeta;
        public bool IsApproved;
        public int ChainId;
        public int NetworkId;
        public Address[] Accounts;
    }
}

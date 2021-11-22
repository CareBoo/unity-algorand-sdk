using System;

namespace AlgoSdk.WalletConnect
{
    [Serializable]
    public struct SavedSession
    {
        public string ClientId;
        public string BridgeUrl;
        public string Key;
        public byte[] KeyRaw;
        public string PeerId;
        public int NetworkId;
        public Address[] Accounts;
        public int ChainId;
        public ClientMeta DappMeta;
    }
}

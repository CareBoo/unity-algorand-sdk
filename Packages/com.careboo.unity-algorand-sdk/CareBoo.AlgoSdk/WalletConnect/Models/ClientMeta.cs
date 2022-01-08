using System;

namespace AlgoSdk.WalletConnect
{
    [Serializable]
    [AlgoApiObject]
    public struct ClientMeta
        : IEquatable<ClientMeta>
    {
        [AlgoApiField("description", null)]
        public string Description;

        [AlgoApiField("url", null)]
        public string Url;

        [AlgoApiField("icons", null)]
        public string[] IconUrls;

        [AlgoApiField("name", null)]
        public string Name;

        public bool Equals(ClientMeta other)
        {
            return StringComparer.Equals(Description, other.Description)
                && StringComparer.Equals(Url, other.Url)
                && ArrayComparer.Equals(IconUrls, other.IconUrls)
                && StringComparer.Equals(Name, other.Name)
                ;
        }
    }
}

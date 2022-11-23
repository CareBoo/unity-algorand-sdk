using System;

namespace Algorand.Unity.WalletConnect
{
    [Serializable]
    [AlgoApiObject]
    public partial struct ClientMeta
        : IEquatable<ClientMeta>
    {
        [AlgoApiField("description")]
        public string Description;

        [AlgoApiField("url")]
        public string Url;

        [AlgoApiField("icons")]
        public string[] IconUrls;

        [AlgoApiField("name")]
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

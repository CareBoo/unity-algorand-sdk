using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ListWalletsResponse
        : IEquatable<ListWalletsResponse>
    {
        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        [AlgoApiField("wallets")]
        public Wallet[] Wallets;

        public bool Equals(ListWalletsResponse other)
        {
            return Error.Equals(other.Error)
                && Message.Equals(other.Message)
                && ArrayComparer.Equals(Wallets, other.Wallets)
                ;
        }
    }
}

using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ListWalletsResponse
        : IEquatable<ListWalletsResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        [AlgoApiField("wallets", null)]
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

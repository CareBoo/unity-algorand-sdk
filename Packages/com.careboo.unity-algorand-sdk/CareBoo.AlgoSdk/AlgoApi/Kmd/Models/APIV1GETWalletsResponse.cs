using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct APIV1GETWalletsResponse
        : IEquatable<APIV1GETWalletsResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        [AlgoApiField("wallets", null)]
        public APIV1Wallet[] Wallets;

        public bool Equals(APIV1GETWalletsResponse other)
        {
            return Error.Equals(other.Error)
                && Message.Equals(other.Message)
                && ArrayComparer.Equals(Wallets, other.Wallets)
                ;
        }
    }
}

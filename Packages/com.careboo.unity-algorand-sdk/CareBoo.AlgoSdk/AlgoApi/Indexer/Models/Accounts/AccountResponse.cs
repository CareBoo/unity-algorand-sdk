using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountResponse
        : IEquatable<AccountResponse>
    {
        [AlgoApiField("account", null)]
        public Account Account;

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound;

        public bool Equals(AccountResponse other)
        {
            return Account.Equals(other.Account)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}

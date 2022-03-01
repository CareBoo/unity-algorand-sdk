using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct AccountResponse
        : IEquatable<AccountResponse>
        , IIndexerResponse
    {
        [AlgoApiField("account", null)]
        public AccountInfo Account { get; set; }

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound { get; set; }

        public bool Equals(AccountResponse other)
        {
            return Account.Equals(other.Account)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}

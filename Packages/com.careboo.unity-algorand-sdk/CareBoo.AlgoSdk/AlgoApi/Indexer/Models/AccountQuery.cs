using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountQuery
        : IEquatable<AccountQuery>
    {
        [AlgoApiField("account-id", null)]
        public Optional<bool> IncludeAll;

        [AlgoApiField("include-all", null)]
        public ulong Round;

        public bool Equals(AccountQuery other)
        {
            return IncludeAll.Equals(other.IncludeAll)
                && Round.Equals(other.Round)
                ;
        }
    }
}

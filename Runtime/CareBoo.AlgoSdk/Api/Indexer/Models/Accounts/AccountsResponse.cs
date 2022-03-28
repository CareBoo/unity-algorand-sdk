using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct AccountsResponse
        : IEquatable<AccountsResponse>
        , IPaginatedIndexerResponse<AccountInfo>
    {
        [AlgoApiField("accounts")]
        public AccountInfo[] Accounts { get; set; }

        [AlgoApiField("current-round")]
        public ulong CurrentRound { get; set; }

        [AlgoApiField("next-token")]
        public FixedString128Bytes NextToken { get; set; }

        AccountInfo[] IPaginatedIndexerResponse<AccountInfo>.Results
        {
            get => Accounts;
            set => Accounts = value;
        }

        public bool Equals(AccountsResponse other)
        {
            return ArrayComparer.Equals(Accounts, other.Accounts)
                && CurrentRound.Equals(other.CurrentRound)
                && NextToken.Equals(other.NextToken)
                ;
        }
    }
}

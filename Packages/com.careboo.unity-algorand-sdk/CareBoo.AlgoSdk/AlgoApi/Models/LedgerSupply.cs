using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct LedgerSupply
        : IEquatable<LedgerSupply>
    {
        [AlgoApiKey("current_round", null)]
        public ulong Round;

        [AlgoApiKey("online-money", null)]
        public ulong OnlineMoney;

        [AlgoApiKey("total-money", null)]
        public ulong TotalMoney;

        public bool Equals(LedgerSupply other)
        {
            return Round == other.Round
                && OnlineMoney == other.OnlineMoney
                && TotalMoney == other.TotalMoney
                ;
        }
    }
}

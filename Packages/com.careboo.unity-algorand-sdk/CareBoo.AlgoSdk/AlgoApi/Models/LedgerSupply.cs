using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct LedgerSupply
        : IEquatable<LedgerSupply>
    {
        [AlgoApiKey("current_round")]
        public ulong Round;
        [AlgoApiKey("online-money")]
        public ulong OnlineMoney;
        [AlgoApiKey("total-money")]
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

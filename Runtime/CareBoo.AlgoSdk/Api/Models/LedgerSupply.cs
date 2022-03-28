using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Supply represents the current supply of MicroAlgos in the system.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct LedgerSupply
        : IEquatable<LedgerSupply>
    {
        [AlgoApiField("current_round")]
        public ulong Round;

        [AlgoApiField("online-money")]
        public ulong OnlineMoney;

        [AlgoApiField("total-money")]
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

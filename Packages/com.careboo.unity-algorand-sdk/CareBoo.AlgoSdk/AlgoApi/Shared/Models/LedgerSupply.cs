using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Supply represents the current supply of MicroAlgos in the system.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public struct LedgerSupply
        : IEquatable<LedgerSupply>
    {
        [AlgoApiField("current_round", null)]
        public ulong Round;

        [AlgoApiField("online-money", null)]
        public ulong OnlineMoney;

        [AlgoApiField("total-money", null)]
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

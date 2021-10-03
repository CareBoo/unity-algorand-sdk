using System;

namespace AlgoSdk
{
    public struct TransactionsQuery
        : IEquatable<TransactionsQuery>
    {
        public Address Address;

        public bool Equals(TransactionsQuery other)
        {
            throw new NotImplementedException();
        }
    }
}

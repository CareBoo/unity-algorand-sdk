using System;

namespace AlgoSdk.WalletConnect
{
    [AlgoApiFormatter(typeof(SignTxnParamsFormatter))]
    public struct SignTxnParams
        : IEquatable<SignTxnParams>
    {
        public WalletTransaction[] Transactions;
        public SignTxnOpts Options;

        public bool Equals(SignTxnParams other)
        {
            return ArrayComparer.Equals(Transactions, other.Transactions)
                && Options.Equals(other.Options)
                ;
        }
    }
}

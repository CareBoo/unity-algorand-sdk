using System;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// Parameters used in an <see cref="AlgoSignTxnsRequest"/>
    /// </summary>
    [AlgoApiFormatter(typeof(SignTxnsParamsFormatter))]
    public struct SignTxnsParams
        : IEquatable<SignTxnsParams>
    {
        /// <summary>
        /// A group of 1-16 transactions. Each transaction in the group
        /// (even ones not being signed by the wallet) must be an element in this array.
        /// </summary>
        public WalletTransaction[] Transactions;

        /// <summary>
        /// Optional options for this request.
        /// </summary>
        public SignTxnsOpts Options;

        public bool Equals(SignTxnsParams other)
        {
            return ArrayComparer.Equals(Transactions, other.Transactions)
                && Options.Equals(other.Options)
                ;
        }
    }
}

using System;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// Options for an <see cref="AlgoSignTxnsRequest"/>.
    /// </summary>
    [AlgoApiObject]
    public struct SignTxnsOpts
        : IEquatable<SignTxnsOpts>
    {
        /// <summary>
        /// An optional message describing the group of transactions
        /// in the <see cref="AlgoSignTxnsRequest"/>.
        /// </summary>
        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(SignTxnsOpts other)
        {
            return StringComparer.Equals(Message, other.Message);
        }
    }
}

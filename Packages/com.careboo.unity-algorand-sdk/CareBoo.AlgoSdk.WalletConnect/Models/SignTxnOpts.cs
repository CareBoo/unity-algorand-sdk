using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct SignTxnOpts
        : IEquatable<SignTxnOpts>
    {
        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(SignTxnOpts other)
        {
            return StringComparer.Equals(Message, other.Message);
        }
    }
}

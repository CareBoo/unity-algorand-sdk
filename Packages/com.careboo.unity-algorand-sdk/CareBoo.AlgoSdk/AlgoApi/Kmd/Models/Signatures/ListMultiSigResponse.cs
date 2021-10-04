using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ListMultiSigResponse
        : IEquatable<ListMultiSigResponse>
    {
        [AlgoApiField("addresses", null)]
        public Address[] Addresses;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(ListMultiSigResponse other)
        {
            return ArrayComparer.Equals(Addresses, other.Addresses)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}

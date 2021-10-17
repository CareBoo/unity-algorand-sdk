using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ListMultisigResponse
        : IEquatable<ListMultisigResponse>
    {
        [AlgoApiField("addresses", null)]
        public Address[] Addresses;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(ListMultisigResponse other)
        {
            return ArrayComparer.Equals(Addresses, other.Addresses)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}

using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct ListKeysResponse
        : IEquatable<ListKeysResponse>
    {
        [AlgoApiField("addresses", null)]
        public Address[] Addresses;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(ListKeysResponse other)
        {
            return ArrayComparer.Equals(Addresses, other.Addresses)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}
